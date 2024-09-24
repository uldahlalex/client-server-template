using DataAccess.Models;
using DataAccess.TransferModels.Request;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interfaces;
using Service.TransferModels.Responses;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaperController(IPaperService paperService) : ControllerBase
{
    [HttpPost]
    [Route("CreatePaper")]
    public ActionResult<PaperDto> CreatePaper([FromBody] CreatePaperDto createPaperDto)
    {
        var paper = paperService.CreatePaper(createPaperDto);
        return Ok(paper);
    }
}