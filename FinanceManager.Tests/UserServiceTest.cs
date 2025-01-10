using FinanceManager.Model;
using FinanceManager.Service;
using FinanceManager.Service.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Net;

namespace FinanceManager.Tests;

public class UserServiceTest
{
    private JWTToken _jwtTokenService;

    public UserServiceTest()
    {
        var jwtSettings = Options.Create(new JWTSettings
        {
            Secret = "test173secret496749agvelk5y1se8s56s8w8", // Deve ser suficientemente longo para HmacSha256
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 30
        });

        // Instancia o JwtTokenService com as configurações mock
        _jwtTokenService = new JWTToken(jwtSettings);
    }

    [Theory]
    [InlineData(09, "Izi", "valid@example.com", "Valid123!")]
    [InlineData(34, "Valid User", "valid@example.com", "Valid123!")]
    [InlineData(56, "Valid User Jhon", "valid_jhon@example.com", "tEste854_@!")]
    [InlineData(72, "Valid User Marie", "marie_valid@example.com", "@56rtd9jJhs15")]
    public void GenerateHashPassAndValidatonTokenValid(int userId, string name, string email, string pass)
    {
        // Arrange
        var resultUser = User.Create(name, email, pass);
        var passHash = PasswordHasher.HashPassword(pass);
        resultUser.Data!.SetPasswordHash(passHash);

        // Act
        var (isValid, message) = PasswordHasher.VerifyPassword(pass, resultUser.Data.Password);

        var token = _jwtTokenService.GenerateToken(userId.ToString(), resultUser.Data.Email);
        var resultToken = _jwtTokenService.GetUserFromToken(token);

        // Assert
        isValid.Should().BeTrue();
        message.Should().Be("Sucesso!");

        resultToken.Data.UserId.Should().Be(userId.ToString());
        resultToken.Data.UserEmail.Should().Be(email);
    }

    [Theory]
    [InlineData("", "valid@mail.com", "Valid123!")]
    [InlineData("T", "valid@mail.com", "Valid123!")]
    [InlineData("an", "valid@mail.com", "Valid123!")]
    public void GenerateHashPassAndValidatonTokenInvalidName(string name, string email, string pass)
    {
        // act
        var resultUser = User.Create(name, email, pass);

        // Assert
        resultUser.Error!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        resultUser.Error.Message.Should().Be("O nome deve conter pelo menos 3 caracteres!");
    }

    [Theory]
    [InlineData("Invalid User Clare", "invalid@example", "Valid123!")]
    [InlineData("Invalid User Jack", "invalid@.com", "Valid123!")]
    [InlineData("Invalid User Hurley", "invalid.mail.com", "Valid123!")]
    [InlineData("Invalid User Said", "invalid.com", "Valid123!")]
    public void GenerateHashPassAndValidatonTokenInvalidEmail(string name, string email, string pass)
    {
        // Act
        var result = User.Create(name, email, pass);

        // Assert
        result.Error!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Error.Message.Should().Be("Email inválido!");
    }

    [Theory]
    [InlineData("Valid User", "valid@example.com", "invalid123!")]
    [InlineData("Invalid User Jhon", "valid@example.com", "teste")]
    [InlineData("Invalid User Marie", "valid@example.com", "123456789")]
    public void GenerateHashPassAndValidatonTokenInvalidPassword(string name, string email, string pass)
    {
        // Act
        var result = User.Create(name, email, pass);

        // Assert
        result.Error!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Error.Message.Should().Be("A senha deve conter pelo menos 8 caracteres, incluindo números, letras e caracteres especiais!");
    }
}