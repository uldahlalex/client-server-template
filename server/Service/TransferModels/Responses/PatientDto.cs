using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class PatientDto
{
    public string? Address { get; set; }

    public bool Gender { get; set; }

    public int Id { get; set; }

    public ICollection<Diagnosis> Diagnoses { get; set; }

    public DateOnly Birthdate { get; set; }

    public string Name { get; set; }

    public PatientDto FromEntity(Patient patient)
    {
        return new PatientDto
        {
            Name = patient.Name,
            Birthdate = patient.Birthdate,
            Address = patient.Address,
            Gender = patient.Gender,
            Id = patient.Id,
            Diagnoses = patient.Diagnoses
        };
    }
}