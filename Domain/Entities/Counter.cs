using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Key", "Id")]
[Table("Counter", Schema = "HangFire")]
public partial class Counter
{
    [Key]
    [StringLength(100)]
    public string Key { get; set; } = null!;

    public int Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireAt { get; set; }

    [Key]
    public long Id { get; set; }
}
