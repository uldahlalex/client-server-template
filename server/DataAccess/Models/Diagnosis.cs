namespace DataAccess.Models;

public class Diagnosis
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid DiseaseId { get; set; }

    public DateTime? DiagnosisDate { get; set; }

    public Guid DoctorId { get; set; }

    public virtual Disease Disease { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}