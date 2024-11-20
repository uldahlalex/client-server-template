using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class DoctorResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Specialty { get; set; } = null!;
    public int? YearsExperience { get; set; }

    public DoctorResponseDto FromEntity(Doctor doctor)
    {
        return new DoctorResponseDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Specialty = doctor.Specialty,
            YearsExperience = doctor.YearsExperience
        };
    }
}