using Moq;
using FelipeApiDapperTemplate.Application.Services.Authentication;
using FelipeApiDapperTemplate.Domain.Services.Authentication;
using Microsoft.Extensions.Configuration;
using FelipeApiDapperTemplate.Domain.Models.DTOs;

namespace FelipeApiDapperTemplate.Tests;

public class AuthenticationServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _tokenServiceMock = new Mock<ITokenService>();

        _configurationMock.Setup(c => c["Jwt:PrivateKey"]).Returns("fake-secret-key");
        _configurationMock.Setup(c => c["Jwt:ExpireMinutes"]).Returns("60");
        _configurationMock.Setup(c => c["Authentication:Username"]).Returns("correct-username");
        _configurationMock.Setup(c => c["Authentication:Password"]).Returns("correct-password");

        _authenticationService = new AuthenticationService(_configurationMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public void Authenticate_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var authenticationModel = new AuthenticationDTO(
            Username: "correct-username",
            Password: "correct-password"
        );

        var expectedToken = "fake-jwt-token";
        _tokenServiceMock.Setup(ts => ts.Generate(It.IsAny<string>())).Returns(expectedToken);

        // Act
        var result = _authenticationService.Authenticate(authenticationModel);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public void Authenticate_InvalidCredentials_ReturnsEmptyString()
    {
        // Arrange
        var authenticationModel = new AuthenticationDTO(
            Username: "wrong-username",
            Password: "wrong-password"
        );

        // Act
        var result = _authenticationService.Authenticate(authenticationModel);

        // Assert
        Assert.Equal("", result);
    }
}
