using DataAccess.Models;

namespace Service.TransferModels.Requests;

public class CreateDiagnosisDto
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int DiseaseId { get; set; }

    public Diagnosis ToDiagnosis()
    {
        return new Diagnosis()
        {
            DiseaseId = DiseaseId,
            DoctorId = DoctorId,
            PatientId = PatientId
        };
    }
}