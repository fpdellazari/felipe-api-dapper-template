using Microsoft.Extensions.Configuration;
using FelipeApiDapperTemplate.Domain.Services.Authentication;
using FelipeApiDapperTemplate.Domain.Models.DTOs;

namespace FelipeApiDapperTemplate.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AuthenticationService(IConfiguration configuration, ITokenService tokenService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public string Authenticate(AuthenticationDTO authentication)
    {
        if (authentication.Username != _configuration["Authentication:Username"] || authentication.Password != _configuration["Authentication:Password"]) return "";
        var token = _tokenService.Generate(authentication.Username);

        return token;
    }
}

