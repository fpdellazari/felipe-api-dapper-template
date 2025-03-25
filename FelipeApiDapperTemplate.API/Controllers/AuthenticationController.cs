using Microsoft.AspNetCore.Mvc;
using FelipeApiDapperTemplate.Domain.Services.Authentication;
using FelipeApiDapperTemplate.Domain.Models.DTOs;

namespace FelipeApiDapperTemplate.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public IActionResult Authenticate(AuthenticationDTO authenticationModel)
    {
        var token = _authenticationService.Authenticate(authenticationModel);

        if (token == "") return Unauthorized(new { Message = "Usuário ou senha inválidos." });        

        return Ok(token);        
    }
}

