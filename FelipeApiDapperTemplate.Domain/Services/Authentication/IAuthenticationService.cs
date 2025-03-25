using FelipeApiDapperTemplate.Domain.Models.DTOs;

namespace FelipeApiDapperTemplate.Domain.Services.Authentication;

public interface IAuthenticationService
{
    string Authenticate(AuthenticationDTO authentication);
}

