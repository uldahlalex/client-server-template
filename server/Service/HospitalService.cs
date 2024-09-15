using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.TransferModels.Requests;

namespace Service;

public interface IHospitalService
{
    public Patient CreatePatient(CreatePatientDto createPatientDto);
}
public class HospitalService(
    ILogger<HospitalService> logger, 
    IRepository repository, 
    IValidator<CreatePatientDto> createPatientValidator,
    IValidator<UpdatePatientDto> updatePatientValidator
    ) : IHospitalService
{

    public Patient CreatePatient(CreatePatientDto createPatientDto)
    {
        createPatientValidator.ValidateAndThrow(createPatientDto);
        var patient = createPatientDto.ToPatient();
        Patient newPatient = repository.CreatePatient(patient);
        return newPatient;
    }
    
    public Patient UpdatePatient(UpdatePatientDto updatePatientDto)
    {
        updatePatientValidator.ValidateAndThrow(updatePatientDto);
        throw new NotImplementedException();
    }
}