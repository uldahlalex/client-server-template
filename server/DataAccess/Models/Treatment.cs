namespace DataAccess.Models;

public partial class Treatment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public virtual ICollection<PatientTreatment> PatientTreatments { get; set; } = new List<PatientTreatment>();
}
