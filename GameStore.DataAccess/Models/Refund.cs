using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("refunds")]
[Index("OrderItemId", Name = "refunds_order_item_id_key", IsUnique = true)]
public partial class Refund
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("amount", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("reason")]
    [StringLength(500)]
    public string Reason { get; set; } = null!;

    [Column("refunded_at", TypeName = "timestamp without time zone")]
    public DateTime RefundedAt { get; set; }

    [Column("payment_ref")]
    [StringLength(40)]
    public string PaymentRef { get; set; } = null!;

    [ForeignKey("OrderItemId")]
    [InverseProperty("Refund")]
    public virtual OrderItem OrderItem { get; set; } = null!;
}
