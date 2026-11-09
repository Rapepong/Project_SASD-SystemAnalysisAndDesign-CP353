using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("orders")]
public partial class Order
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("ordered_at", TypeName = "timestamp without time zone")]
    public DateTime OrderedAt { get; set; }

    [Column("total_amount", TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [Column("bank_account_no")]
    [StringLength(20)]
    public string BankAccountNo { get; set; } = null!;

    [Column("payment_ref")]
    [StringLength(40)]
    public string PaymentRef { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
