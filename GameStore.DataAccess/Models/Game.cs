using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("games")]
public partial class Game
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(150)]
    public string Title { get; set; } = null!;

    [Column("developer")]
    [StringLength(100)]
    public string Developer { get; set; } = null!;

    [Column("description")]
    [StringLength(2000)]
    public string Description { get; set; } = null!;

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }

    [Column("release_date")]
    public DateOnly ReleaseDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [InverseProperty("Game")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [InverseProperty("Game")]
    public virtual ICollection<LibraryItem> LibraryItems { get; set; } = new List<LibraryItem>();

    [InverseProperty("Game")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("GameId")]
    [InverseProperty("Games")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
