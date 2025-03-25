namespace FelipeApiDapperTemplate.Domain.Services.Authentication;

public interface ITokenService
{
    string Generate(string userName);
}
