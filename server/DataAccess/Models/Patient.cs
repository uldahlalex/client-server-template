using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("patients")]
public partial class Patient
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("birthdate")]
    public DateOnly Birthdate { get; set; }

    [Column("gender")]
    public bool Gender { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    [InverseProperty("Patient")]
    public virtual ICollection<PatientTreatment> PatientTreatments { get; set; } = new List<PatientTreatment>();
}
