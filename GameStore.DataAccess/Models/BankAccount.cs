using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Models;

[Table("bank_accounts")]
[Index("AccountNo", Name = "bank_accounts_account_no_key", IsUnique = true)]
public partial class BankAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("account_no")]
    [StringLength(20)]
    public string AccountNo { get; set; } = null!;

    [Column("owner_name")]
    [StringLength(100)]
    public string OwnerName { get; set; } = null!;

    [Column("balance", TypeName = "money")]
    public decimal Balance { get; set; }
}
