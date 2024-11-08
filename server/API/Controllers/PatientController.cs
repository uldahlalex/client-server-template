using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PatientController(
    IHospitalService service,
    IOptionsMonitor<AppOptions> options
) : ControllerBase
{
    [HttpPost]
    [Route("")]
    public ActionResult<PatientResponseDto> CreatePatient(CreatePatientDto createPatientDto)
    {
        var patient = service.CreatePatient(createPatientDto);
        return Ok(patient);
    }

    [HttpPut]
    [Route("")]
    public ActionResult<PatientResponseDto> UpdatePatient(UpdatePatientDto updatePatientDto)
    {
        var patient = service.UpdatePatient(updatePatientDto);
        return Ok(patient);
    }

    [HttpGet]
    [Route("")]
    public ActionResult<List<PatientResponseDto>> GetAllPatients(int limit = 10, int startAt = 0)
    {
        var patients = service.GetAllPatients(limit, startAt);
        return Ok(patients);
    }
}