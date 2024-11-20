using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class DiseaseResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Severity { get; set; } = null!;


    public DiseaseResponseDto FromEntity(Disease disease)
    {
        var dto = new DiseaseResponseDto
        {
            Severity = disease.Severity,
            Name = disease.Name,
            Id = disease.Id
        };
        return dto;
    }
}