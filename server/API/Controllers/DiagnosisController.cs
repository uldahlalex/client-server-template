using Microsoft.AspNetCore.Mvc;
using Service;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosisController(IHospitalService service) : ControllerBase
{
    [HttpPost]
    public ActionResult<DiagnosisResponseDto> CreateDiagnosis([FromBody] CreateDiagnosisDto dto)
    {
        var result = service.CreateDiagnosis(dto);
        return Ok(result);
    }
}