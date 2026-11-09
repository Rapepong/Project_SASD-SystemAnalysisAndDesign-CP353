using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("cart_items")]
[Index("UserId", "GameId", Name = "cart_items_user_id_game_id_key", IsUnique = true)]
public partial class CartItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("game_id")]
    public int GameId { get; set; }

    [Column("added_at", TypeName = "timestamp without time zone")]
    public DateTime AddedAt { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("CartItems")]
    public virtual Game Game { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("CartItems")]
    public virtual User User { get; set; } = null!;
}
