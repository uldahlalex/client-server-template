namespace DataAccess.Models;

public class Diagnosis
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DiseaseId { get; set; }

    public DateTime? DiagnosisDate { get; set; }

    public int DoctorId { get; set; }

    public virtual Disease Disease { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}