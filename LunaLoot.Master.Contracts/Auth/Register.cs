using System.Text.Json.Serialization;
using LunaLoot.Master.Application.Auth.Commands.Register;

namespace LunaLoot.Master.Contracts.Auth;



public record RegisterRequest
{
    [JsonPropertyName("firstName")]
    public required  string FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }
    
    [JsonPropertyName("mobilePhoneNumber")]
    public required  string MobilePhoneNumber { get; set; }
    
    [JsonPropertyName("email")]
    public required  string Email { get; set; }
    
    [JsonPropertyName("password")]
    public required string Password { get; set; }


    public static implicit operator RegisterCommand(RegisterRequest request)
    {
        return new RegisterCommand(
                FirstName: request.FirstName,
                LastName: request.LastName,
                Email: request.Email,
                MobilePhoneNumber: request.MobilePhoneNumber,
                Password: request.Password
            );

    }

    
    [JsonConstructor]
    public RegisterRequest(
        string firstName,
        string lastName,
        string mobilePhoneNumber,
        string email,
        string password)
    {
        FirstName = firstName;
        LastName = lastName;
        MobilePhoneNumber = mobilePhoneNumber;
        Email = email;
        Password = password;
    }
};

public record RegisterResponse();