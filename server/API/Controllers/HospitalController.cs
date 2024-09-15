using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.TransferModels.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalController(IHospitalService service) : ControllerBase
{
    
    [HttpPost]
    [Route(nameof(Patient))]
    public ActionResult CreatePatient(CreatePatientDto createPatientDto)
    {
        var patient = service.CreatePatient(createPatientDto);
        return Ok(patient);
    }
    
    [HttpGet]
    [Route(nameof(Patient))]
    public ActionResult GetAllPatients()
    {
        //var patients = service.GetAllPatients();
        return Ok();
    }
    
}