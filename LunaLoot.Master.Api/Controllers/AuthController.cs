using System.Net;
using ErrorOr;
using LunaLoot.Master.Api.Common;
using LunaLoot.Master.Application.Auth.Commands.Register;
using LunaLoot.Master.Contracts.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LunaLoot.Master.Api.Controllers;


[Route("auth")]
public class AuthController(ISender mediator, ILogger<AuthController> logger): ApiController
{

    private readonly ILogger _logger = logger;
    
    [HttpGet("")]
    public string  AuthTest()
    {
        return "Hello auth";
    }


    
    [HttpPost("login"), Produces("application/json")]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest body)
    {
        

        
        return Ok(new LoginResponse(
            AccessToken: string.Empty,
            RefreshToken: string.Empty
        ));
    }

    [HttpPost("register"), Produces("application/json")]
    [ProducesResponseType<RegisterResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterRequest body)
    {

        _logger.LogInformation("controller");

        if (!ModelState.IsValid)
        {
            _logger.LogInformation("model state is invalid");
            return Problem(statusCode:StatusCodes.Status500InternalServerError);
        }
        RegisterCommand command = body;
        var result = await mediator.Send(command);
        
        _logger.LogInformation(result.IsError.ToString());
        _logger.LogInformation(result.Errors.FirstOrDefault().ToString());
        return Ok();
    }
}