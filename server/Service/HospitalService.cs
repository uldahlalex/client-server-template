using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;

namespace Service;

public interface IHospitalService
{
    public PatientDto CreatePatient(CreatePatientDto createPatientDto);
    public PatientDto UpdatePatient(UpdatePatientDto updatePatientDto);
    public List<Patient> GetAllPatients(int limit, int startAt);
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
    /// This one relies on the repository to "Create patient"
    /// </summary>
    /// <param name="createPatientDto"></param>
    /// <returns></returns>
    public PatientDto CreatePatient(CreatePatientDto createPatientDto)
    {
        createPatientValidator.ValidateAndThrow(createPatientDto);
        var patient = createPatientDto.ToPatient();
        Patient newPatient = hospitalRepository.InsertPatient(patient);
        return new PatientDto().FromEntity(newPatient);
    }
    
    /// <summary>
    /// This one deliberately does not use the repository, it uses the context directly (for demonstration purposes)
    /// </summary>
    /// <param name="updatePatientDto"></param>
    /// <returns></returns>
    public PatientDto UpdatePatient(UpdatePatientDto updatePatientDto)
    {
        updatePatientValidator.ValidateAndThrow(updatePatientDto);
        var patient = updatePatientDto.ToPatient();
        context.Patients.Update(patient);
        return new PatientDto().FromEntity(patient);
    }

    public List<Patient> GetAllPatients(int limit, int startAt)
    {
        return context.Patients.OrderBy(p => p.Id).Skip(startAt).Take(limit).ToList();
    }
}