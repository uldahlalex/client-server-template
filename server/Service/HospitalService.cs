using DataAccess;
using DataAccess.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;

namespace Service;

public interface IHospitalService
{
    public PatientResponseDto CreatePatient(CreatePatientDto createPatientDto);
    public PatientResponseDto UpdatePatient(UpdatePatientDto updatePatientDto);
    public List<PatientResponseDto> GetAllPatients(int limit, int startAt);
    public DiagnosisResponseDto CreateDiagnosis(CreateDiagnosisDto dto);
}

public class HospitalService(
    ILogger<HospitalService> logger,
    IHospitalRepository hospitalRepository,
    IValidator<CreatePatientDto> createPatientValidator,
    IValidator<UpdatePatientDto> updatePatientValidator,
    HospitalContext context
) : IHospitalService
{
    /// <summary>
    ///     This one relies on the repository to "Create patient"
    /// </summary>
    /// <param name="createPatientDto"></param>
    /// <returns></returns>
    public PatientResponseDto CreatePatient(CreatePatientDto createPatientDto)
    {
        createPatientValidator.ValidateAndThrow(createPatientDto);
        var patient = createPatientDto.ToPatient();
        var newPatient = hospitalRepository.InsertPatient(patient);
        return new PatientResponseDto().FromEntity(newPatient);
    }

    /// <summary>
    ///     This one deliberately does not use the repository, it uses the context directly (for demonstration purposes)
    /// </summary>
    /// <param name="updatePatientDto"></param>
    /// <returns></returns>
    public PatientResponseDto UpdatePatient(UpdatePatientDto updatePatientDto)
    {
        updatePatientValidator.ValidateAndThrow(updatePatientDto);
        var patient = updatePatientDto.ToPatient();
        context.Patients.Update(patient);
        return new PatientResponseDto().FromEntity(patient);
    }

    public List<PatientResponseDto> GetAllPatients(int limit, int startAt)
    {
        var entities = context.Patients.OrderBy(p => p.Id).Skip(startAt).Take(limit).ToList();
        var dtos = entities.Select(patient => new PatientResponseDto().FromEntity(patient));
        return dtos.ToList();
    }

    public DiagnosisResponseDto CreateDiagnosis(CreateDiagnosisDto dto)
    {
        var diagnosis = dto.ToDiagnosis();
        context.Diagnoses.Add(diagnosis);
        context.SaveChanges();
        var result = context.Diagnoses.First(d => d.Id == diagnosis.Id) ??
                     throw new KeyNotFoundException("Could not find");
        return new DiagnosisResponseDto().FromEntity(result);
    }
}