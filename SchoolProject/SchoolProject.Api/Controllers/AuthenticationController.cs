using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Commands;
using SchoolProject.Application.services;

namespace SchoolProject.Api.Controllers;

[Route("api/authentication/")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        // login in authentication service
        var result = _authenticationService.Login(loginCommand);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterationCommand registerationCommand)
    {
        // register in authentication service
        var result = _authenticationService.Register(registerationCommand);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("get-classroom")]
    public async Task<IActionResult> GetClassroom()
    {
        var result = _authenticationService.GetClassrooms();
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("set-classroom-to-user")]
    public async Task<IActionResult> SetClassroomToUser(SetClassroomToUserCommand command)
    {
        await _authenticationService.SetClassroomsToUser(command);
        return Ok();
    }
}