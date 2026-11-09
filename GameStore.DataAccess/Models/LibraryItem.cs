using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("library_items")]
[Index("OrderItemId", Name = "library_items_order_item_id_key", IsUnique = true)]
[Index("UserId", "GameId", Name = "library_items_user_id_game_id_key", IsUnique = true)]
public partial class LibraryItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("game_id")]
    public int GameId { get; set; }

    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("acquired_at", TypeName = "timestamp without time zone")]
    public DateTime AcquiredAt { get; set; }

    [Column("playtime_minutes")]
    public int PlaytimeMinutes { get; set; }

    [Column("last_played_at", TypeName = "timestamp without time zone")]
    public DateTime? LastPlayedAt { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("LibraryItems")]
    public virtual Game Game { get; set; } = null!;

    [ForeignKey("OrderItemId")]
    [InverseProperty("LibraryItem")]
    public virtual OrderItem OrderItem { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LibraryItems")]
    public virtual User User { get; set; } = null!;
}
