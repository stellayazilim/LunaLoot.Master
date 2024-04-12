using System.Text.Json.Serialization;
using LunaLoot.Master.Application.Auth.Commands.Login;

namespace LunaLoot.Master.Contracts.Auth;

public record LoginRequest
{
    
    [JsonPropertyName("email")] 
    public required string Email { get; set; }
    
    [JsonPropertyName("password")]
    public required string Password { get; set; }


    public static implicit operator LoginCommand(LoginRequest request)
    {
        return new LoginCommand()
        {
            Email = request.Email,
            Password = request.Password
        };
    }
}


public record LoginResponse(
        string AccessToken,
        string RefreshToken
    );