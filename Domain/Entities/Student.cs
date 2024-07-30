using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("Student")]
[Index("AccountId", Name = "FK_Student_Account", IsUnique = true)]
public partial class Student
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    public Guid AccountId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Student")]
    public virtual Account Account { get; set; } = null!;
}
