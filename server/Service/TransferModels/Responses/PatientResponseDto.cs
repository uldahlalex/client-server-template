using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class PatientResponseDto
{
    public string? Address { get; set; }

    public bool Gender { get; set; }

    public int Id { get; set; }

    public ICollection<DiagnosisResponseDto> DiagnosisResponseDtos { get; set; }

    public DateOnly Birthdate { get; set; }

    public string Name { get; set; }


    public PatientResponseDto FromEntity(Patient patient)
    {
        return new PatientResponseDto
        {
            Name = patient.Name,
            Birthdate = patient.Birthdate,
            Address = patient.Address,
            Gender = patient.Gender,
            Id = patient.Id,
            DiagnosisResponseDtos = patient.Diagnoses.Select(d => new DiagnosisResponseDto().FromEntity(d)).ToList()
        };
    }
}