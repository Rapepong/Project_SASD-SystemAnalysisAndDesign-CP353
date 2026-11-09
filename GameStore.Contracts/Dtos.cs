namespace GameStore.Contracts;

// =====================================================================
//  Data Transfer Objects (DTO)
//  รูปแบบข้อมูล (JSON) ที่ส่งไป-กลับระหว่าง Client กับ REST API
//  ใช้ร่วมกันทั้ง GameStore.Api และ GameStore.Client
//  (ทำหน้าที่เดียวกับคลาสที่ได้จาก "Add Service Reference" ในชีต 12)
//
//  ไม่ส่ง Entity ของ EF ออกไปตรง ๆ เพราะ
//   1. Entity มีข้อมูลที่ไม่ควรหลุดออกไป เช่น password_hash
//   2. Entity มี navigation property วนกันไปมา (Game -> OrderItems -> Game ...)
//   3. ถ้าแก้ table แล้ว API ไม่ควรเปลี่ยนตามโดยไม่ตั้งใจ
// =====================================================================

// ---------- Users ----------
public record RegisterRequest(string Username, string Password, string DisplayName, string Email);

public record LoginRequest(string Username, string Password);

public record UserDto(int Id, string Username, string DisplayName, string Email);

// ---------- Catalog (Games & Genres) ----------
public record GenreDto(int Id, string Name);

public record GameSummaryDto(
    int Id,
    string Title,
    string Developer,
    decimal Price,
    List<string> Genres);

public record GameDetailDto(
    int Id,
    string Title,
    string Developer,
    string Description,
    decimal Price,
    DateOnly ReleaseDate,
    List<string> Genres);

// ---------- Cart ----------
public record AddCartItemRequest(int GameId);

public record CartItemDto(int GameId, string Title, decimal Price, DateTime AddedAt);

public record CartDto(List<CartItemDto> Items, decimal TotalAmount);

// ---------- Orders (การซื้อ) ----------
public record CheckoutRequest(string BankAccountNo);

public record OrderItemDto(int Id, int GameId, string Title, decimal Price, bool IsRefunded);

public record OrderDto(
    int Id,
    DateTime OrderedAt,
    decimal TotalAmount,
    string BankAccountNo,
    string PaymentRef,
    List<OrderItemDto> Items);

// ใบเสร็จหลังซื้อสำเร็จ (BankBalanceAfter = ยอดเงินคงเหลือในบัญชีธนาคารจำลอง เหมือนใน slip)
public record PurchaseReceiptDto(OrderDto Order, decimal BankBalanceAfter);

// ---------- Library (Game List) ----------
public record LibraryItemDto(
    int GameId,
    string Title,
    string Developer,
    DateTime AcquiredAt,
    int PlaytimeMinutes,
    DateTime? LastPlayedAt,
    int OrderItemId,
    decimal PricePaid,
    bool IsRefundable,
    string RefundNote);

// ---------- Refunds (การขอคืนเงิน) ----------
public record RefundRequest(int OrderItemId, string Reason);

public record RefundDto(
    int Id,
    int OrderItemId,
    int GameId,
    string Title,
    decimal Amount,
    string Reason,
    DateTime RefundedAt,
    string PaymentRef);

public record RefundReceiptDto(RefundDto Refund, decimal BankBalanceAfter);
