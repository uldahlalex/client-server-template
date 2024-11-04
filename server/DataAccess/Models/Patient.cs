namespace DataAccess.Models;

public class Patient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public bool Gender { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<PatientTreatment> PatientTreatments { get; set; } = new List<PatientTreatment>();
}