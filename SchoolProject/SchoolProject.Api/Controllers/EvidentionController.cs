using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Commands;
using SchoolProject.Application.services;

namespace SchoolProject.Api.Controllers;

[Route("/api/evidention/")]
public class EvidentionController : Controller
{
    private readonly IEvidentionService _evidentionService;

    public EvidentionController(IEvidentionService evidentionService)
    {
        _evidentionService = evidentionService;
    }
    
    [AllowAnonymous]
    [HttpPost("add-record")]
    public async Task<IActionResult> AddRecord(AddEvidentionRecordCommand command)
    {
        var ipAdress = HttpContext.Connection.RemoteIpAddress.ToString();
        await _evidentionService.AddRecord(ipAdress, command);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("filter")]
    public async Task<IActionResult> Filter(FilterEvidentionCommand command)
    {
        var result = await _evidentionService.Filter(command);
        return Ok(result);
    }
}