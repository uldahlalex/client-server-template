using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("treatments")]
public partial class Treatment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("cost")]
    public double Cost { get; set; }

    [InverseProperty("Treatment")]
    public virtual ICollection<PatientTreatment> PatientTreatments { get; set; } = new List<PatientTreatment>();
}
