using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("Account")]
[Index("Password", Name = "UQ__Account__87909B1589294BF4", IsUnique = true)]
[Index("Email", Name = "UQ__Account__A9D105341980272A", IsUnique = true)]
public partial class Account
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string Password { get; set; } = null!;

    [StringLength(256)]
    public string Status { get; set; } = null!;

    [InverseProperty("Account")]
    public virtual Student? Student { get; set; }
}
