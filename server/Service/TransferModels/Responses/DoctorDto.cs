using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Specialty { get; set; } = null!;
    public int? YearsExperience { get; set; }
    
    public DoctorDto FromEntity(Doctor doctor)
    {
        return new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Specialty = doctor.Specialty,
            YearsExperience = doctor.YearsExperience
        };
    }
}