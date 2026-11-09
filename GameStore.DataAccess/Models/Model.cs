namespace GameStore.DataAccess.Models;

// เพิ่ม constructor ที่จำเป็นให้กับคลาสต่าง ๆ ที่ generate ขึ้นจากการ scaffold (แบบในชีต 11 หน้า 40)
// จะถูกใช้กรณีต้องการ Modify (Add, Remove, Update) Database
//
// หมายเหตุ: ชื่อ parameter ต้องตรงกับชื่อ property (แบบ camelCase)
// เพราะ EF จะใช้ constructor นี้ตอนอ่านข้อมูลจาก DB ด้วย (constructor binding)

public partial class User
{
    public User(string username, string passwordHash, string displayName, string email, DateTime createdAt)
    {
        Username = username;
        PasswordHash = passwordHash;
        DisplayName = displayName;
        Email = email;
        CreatedAt = createdAt;
    }
}

public partial class CartItem
{
    public CartItem(int userId, int gameId, DateTime addedAt)
    {
        UserId = userId;
        GameId = gameId;
        AddedAt = addedAt;
    }
}

public partial class Order
{
    public Order(int userId, DateTime orderedAt, decimal totalAmount, string bankAccountNo, string paymentRef)
    {
        UserId = userId;
        OrderedAt = orderedAt;
        TotalAmount = totalAmount;
        BankAccountNo = bankAccountNo;
        PaymentRef = paymentRef;
    }
}

public partial class OrderItem
{
    // OrderId จะถูกเติมให้อัตโนมัติ เมื่อเพิ่มเข้าไปใน order.OrderItems
    public OrderItem(int gameId, decimal price)
    {
        GameId = gameId;
        Price = price;
        IsRefunded = false;
    }
}

public partial class LibraryItem
{
    // OrderItemId จะถูกเติมให้อัตโนมัติ เมื่อกำหนด property OrderItem
    public LibraryItem(int userId, int gameId, DateTime acquiredAt)
    {
        UserId = userId;
        GameId = gameId;
        AcquiredAt = acquiredAt;
        PlaytimeMinutes = 0;
    }
}

public partial class Refund
{
    public Refund(int orderItemId, decimal amount, string reason, DateTime refundedAt, string paymentRef)
    {
        OrderItemId = orderItemId;
        Amount = amount;
        Reason = reason;
        RefundedAt = refundedAt;
        PaymentRef = paymentRef;
    }
}
