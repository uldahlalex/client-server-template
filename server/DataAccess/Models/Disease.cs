using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("diseases")]
public partial class Disease
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("severity")]
    public string Severity { get; set; } = null!;

    [InverseProperty("Disease")]
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}
