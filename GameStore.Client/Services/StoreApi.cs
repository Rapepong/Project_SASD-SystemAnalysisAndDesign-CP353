using GameStore.Contracts;

namespace GameStore.Client.Services;

// รวมการเรียก REST API ทุก end point ไว้ที่เดียว
// หน้าจอ (Forms) เรียกใช้คลาสนี้ ไม่ต้องรู้จัก URL หรือ HTTP เอง
public static class StoreApi
{
    private static string BaseUri => MyConfig.BaseUri;

    // ---------- Users ----------
    public static Task<UserDto> LoginAsync(string username, string password) =>
        RestUtil.Post<UserDto>(BaseUri, "users:login", new LoginRequest(username, password));

    public static Task<UserDto> RegisterAsync(RegisterRequest request) =>
        RestUtil.Post<UserDto>(BaseUri, "users", request);

    // ---------- Catalog ----------
    public static Task<List<GenreDto>> GetGenresAsync() =>
        RestUtil.Get<List<GenreDto>>(BaseUri, "genres");

    public static Task<List<GameSummaryDto>> SearchGamesAsync(string? search, int? genreId)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(search))
            query.Add("search=" + Uri.EscapeDataString(search.Trim()));
        if (genreId != null)
            query.Add("genreId=" + genreId);

        string path = query.Count == 0 ? "games" : "games?" + string.Join("&", query);
        return RestUtil.Get<List<GameSummaryDto>>(BaseUri, path);
    }

    public static Task<GameDetailDto> GetGameAsync(int gameId) =>
        RestUtil.Get<GameDetailDto>(BaseUri, $"games/{gameId}");

    // ---------- Cart ----------
    public static Task<CartDto> GetCartAsync(int userId) =>
        RestUtil.Get<CartDto>(BaseUri, $"users/{userId}/cart");

    public static Task<CartDto> AddToCartAsync(int userId, int gameId) =>
        RestUtil.Post<CartDto>(BaseUri, $"users/{userId}/cart/items", new AddCartItemRequest(gameId));

    public static Task RemoveFromCartAsync(int userId, int gameId) =>
        RestUtil.Delete(BaseUri, $"users/{userId}/cart/items/{gameId}");

    // ---------- Orders ----------
    public static Task<PurchaseReceiptDto> CheckoutAsync(int userId, string bankAccountNo) =>
        RestUtil.Post<PurchaseReceiptDto>(BaseUri, $"users/{userId}/orders", new CheckoutRequest(bankAccountNo));

    public static Task<List<OrderDto>> GetOrdersAsync(int userId) =>
        RestUtil.Get<List<OrderDto>>(BaseUri, $"users/{userId}/orders");

    // ---------- Library (Game List) ----------
    public static Task<List<LibraryItemDto>> GetLibraryAsync(int userId) =>
        RestUtil.Get<List<LibraryItemDto>>(BaseUri, $"users/{userId}/library");

    public static Task<LibraryItemDto> PlayAsync(int userId, int gameId) =>
        RestUtil.Post<LibraryItemDto>(BaseUri, $"users/{userId}/library/{gameId}:play", null);

    // ---------- Refunds ----------
    public static Task<RefundReceiptDto> RequestRefundAsync(int userId, int orderItemId, string reason) =>
        RestUtil.Post<RefundReceiptDto>(BaseUri, $"users/{userId}/refunds", new RefundRequest(orderItemId, reason));

    public static Task<List<RefundDto>> GetRefundsAsync(int userId) =>
        RestUtil.Get<List<RefundDto>>(BaseUri, $"users/{userId}/refunds");
}
