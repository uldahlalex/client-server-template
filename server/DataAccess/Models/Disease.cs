namespace DataAccess.Models;

public class Disease
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Severity { get; set; } = null!;

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}