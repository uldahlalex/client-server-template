using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("diagnoses")]
[Index("DiseaseId", Name = "IX_diagnoses_disease_id")]
[Index("DoctorId", Name = "IX_diagnoses_doctor_id")]
[Index("PatientId", Name = "IX_diagnoses_patient_id")]
public partial class Diagnosis
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [Column("disease_id")]
    public Guid DiseaseId { get; set; }

    [Column("diagnosis_date")]
    public DateTime? DiagnosisDate { get; set; }

    [Column("doctor_id")]
    public Guid DoctorId { get; set; }

    [ForeignKey("DiseaseId")]
    [InverseProperty("Diagnoses")]
    public virtual Disease Disease { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("Diagnoses")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Diagnoses")]
    public virtual Patient Patient { get; set; } = null!;
}
