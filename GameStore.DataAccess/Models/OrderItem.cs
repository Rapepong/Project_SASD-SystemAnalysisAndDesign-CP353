using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("order_items")]
public partial class OrderItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("game_id")]
    public int GameId { get; set; }

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }

    [Column("is_refunded")]
    public bool IsRefunded { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("OrderItems")]
    public virtual Game Game { get; set; } = null!;

    [InverseProperty("OrderItem")]
    public virtual LibraryItem? LibraryItem { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;

    [InverseProperty("OrderItem")]
    public virtual Refund? Refund { get; set; }
}
