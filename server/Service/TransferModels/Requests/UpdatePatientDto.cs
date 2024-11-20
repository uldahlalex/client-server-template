using DataAccess.Models;

namespace Service.TransferModels.Requests;

public class UpdatePatientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public bool Gender { get; set; }

    public string? Address { get; set; }

    public Patient ToPatient()
    {
        return new Patient
        {
            Id = Id,
            Name = Name,
            Birthdate = Birthdate,
            Address = Address,
            Gender = Gender
        };
    }
}