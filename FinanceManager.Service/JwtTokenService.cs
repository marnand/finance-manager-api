using FinanceManager.Model.Control;
using FinanceManager.Service.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace FinanceManager.Service;

public class JwtTokenService(IOptions<JwtSettings> jwtSettings)
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(string userId, string email)
    {
        var claims = new[]
        {
            new Claim("user_id", userId),
            new Claim("user_email", email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    /// <summary>
    /// Decodifica e valida o token JWT e retorna o ID e o Nome do usuário.
    /// </summary>
    /// <param name="token">Token JWT.</param>
    /// <returns>Um objeto contendo o ID e o Nome do usuário.</returns>
    /// <exception cref="SecurityTokenException">Se o token for inválido ou não puder ser decodificado.</exception>
    public Result<(string UserId, string UserEmail)> GetUserFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            // Valida o token
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            // Extrai as claims do token
            var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            var userEmail = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "user_email")?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userEmail))
            {
                var errorMessage = "As informações do usuário não foram encontradas no token.";
                return Result<(string UserId, string UserEmail)>.ResultError(errorMessage, HttpStatusCode.Unauthorized);
            }

            return Result<(string UserId, string UserEmail)>.Success((UserId: userId, UserEmail: userEmail));
        }
        catch
        {
            var errorMessage = "O token é inválido ou não pôde ser decodificado.";
            return Result<(string UserId, string UserEmail)>.ResultError(errorMessage, HttpStatusCode.Unauthorized);
        }
    }
}

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; }
}
