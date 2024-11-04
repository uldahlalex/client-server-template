using DataAccess.Models;

namespace Service.TransferModels.Responses;

public class DiagnosisResponseDto
{
    public int Id { get; set; }

    public DateTime? DiagnosisDate { get; set; }

    public virtual DiseaseResponseDto DiseaseResponseDto { get; set; } = null!;

    public virtual DoctorResponseDto DoctorResponseDto { get; set; } = null!;


    public DiagnosisResponseDto FromEntity(Diagnosis diagnosis)
    {
        var dto = new DiagnosisResponseDto
        {
            DiseaseResponseDto = new DiseaseResponseDto().FromEntity(diagnosis.Disease),
            DoctorResponseDto = new DoctorResponseDto().FromEntity(diagnosis.Doctor),
            Id = diagnosis.Id,
            DiagnosisDate = diagnosis.DiagnosisDate
        };
        return dto;
    }
}