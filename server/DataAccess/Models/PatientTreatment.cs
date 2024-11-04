namespace DataAccess.Models;

public class PatientTreatment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int TreatmentId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Treatment Treatment { get; set; } = null!;
}