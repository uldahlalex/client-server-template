using DataAccess.Models;

namespace Service.TransferModels.Requests;

public class CreateDiagnosisDto
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public Guid DiseaseId { get; set; }

    public Diagnosis ToDiagnosis()
    {
        return new Diagnosis
        {
            DiseaseId = DiseaseId,
            DoctorId = DoctorId,
            PatientId = PatientId
        };
    }
}