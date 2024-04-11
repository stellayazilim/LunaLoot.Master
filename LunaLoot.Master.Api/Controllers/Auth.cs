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
public class Auth(ISender mediator): ApiController
{
    
    
    
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


        RegisterCommand command = body;
        var result = await mediator.Send(command);
        
        return Ok();
    }
}