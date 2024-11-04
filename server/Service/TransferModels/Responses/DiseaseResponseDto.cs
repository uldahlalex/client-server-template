using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class DiseaseResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Severity { get; set; } = null!;

      public virtual ICollection<DiagnosisResponseDto> DiagnosesResponseDtos { get; set; } = new List<DiagnosisResponseDto>();

    public DiseaseResponseDto FromEntity(Disease disease)
    {
        var dto = new DiseaseResponseDto
        {
                     DiagnosesResponseDtos = disease.Diagnoses.Select(diagnosis => new DiagnosisResponseDto().FromEntity(diagnosis)).ToList(),
            Severity = disease.Severity,
            Name = disease.Name,
            Id = disease.Id
        };
        return dto;
    }
}