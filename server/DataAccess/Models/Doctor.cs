namespace DataAccess.Models;

public class Doctor
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public int? YearsExperience { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}