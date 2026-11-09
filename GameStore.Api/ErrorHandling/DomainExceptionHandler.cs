using GameStore.Api.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GameStore.Api.ErrorHandling;

// แปลง exception ที่ถูก throw ออกมาจาก Domain Layer ให้เป็น HTTP Response Status Code
// และส่ง body กลับเป็น JSON มาตรฐาน "Problem Details" (RFC 9457) เช่น
//   { "status": 409, "title": "Conflict", "detail": "คุณมีเกม 'Neon Rush' ใน Game List แล้ว" }
// Controller จึงไม่ต้องเขียน try/catch ซ้ำ ๆ ทุก method
public class DomainExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService problemDetailsService;
    private readonly ILogger<DomainExceptionHandler> logger;

    public DomainExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<DomainExceptionHandler> logger)
    {
        this.problemDetailsService = problemDetailsService;
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int status, string title, string detail) = exception switch
        {
            DomainException e => (StatusCodeOf(e.Kind), TitleOf(e.Kind), e.Message),

            // unique constraint ใน database ทำงาน (เช่น กดซื้อซ้ำพร้อมกัน 2 ครั้ง)
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.UniqueViolation } } =>
                (StatusCodes.Status409Conflict, "Conflict", "ข้อมูลซ้ำกับที่มีอยู่แล้ว กรุณารีเฟรชแล้วลองใหม่"),

            // ข้อมูลถูก request อื่นแก้/ลบไปก่อน ระหว่างที่ request นี้กำลังทำงาน
            DbUpdateConcurrencyException =>
                (StatusCodes.Status409Conflict, "Conflict", "ข้อมูลถูกเปลี่ยนแปลงระหว่างทำรายการ กรุณารีเฟรชแล้วลองใหม่"),

            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "เกิดข้อผิดพลาดภายใน server"),
        };

        if (status == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Unhandled exception");

        httpContext.Response.StatusCode = status;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails { Status = status, Title = title, Detail = detail },
        });
    }

    private static int StatusCodeOf(DomainErrorKind kind) => kind switch
    {
        DomainErrorKind.Validation => StatusCodes.Status400BadRequest,
        DomainErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
        DomainErrorKind.PaymentDeclined => StatusCodes.Status402PaymentRequired,
        DomainErrorKind.NotFound => StatusCodes.Status404NotFound,
        DomainErrorKind.Conflict => StatusCodes.Status409Conflict,
        DomainErrorKind.ExternalService => StatusCodes.Status502BadGateway,
        _ => StatusCodes.Status500InternalServerError,
    };

    private static string TitleOf(DomainErrorKind kind) => kind switch
    {
        DomainErrorKind.Validation => "Bad Request",
        DomainErrorKind.Unauthorized => "Unauthorized",
        DomainErrorKind.PaymentDeclined => "Payment Required",
        DomainErrorKind.NotFound => "Not Found",
        DomainErrorKind.Conflict => "Conflict",
        DomainErrorKind.ExternalService => "Bad Gateway",
        _ => "Error",
    };
}
