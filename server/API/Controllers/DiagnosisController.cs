using DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.TransferModels.Requests;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DiagnosisController(IHospitalService service) : ControllerBase
{
    [HttpPost]
    public ActionResult<Diagnosis> CreateDiagnosis([FromBody]CreateDiagnosisDto dto)
    {
        var result = service.CreateDiagnosis(dto);
        return Ok(result);
    }
    
}