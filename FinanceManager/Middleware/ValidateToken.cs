using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FinanceManager.Middleware;

/// <summary>
/// Validar token JWT nas controllers
/// </summary>
public class ValidateToken : AuthorizeAttribute, IAuthorizationFilter
{
    /// <summary>
    /// Método para validar token
    /// </summary>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Verifica se o usuário está autenticado
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Verifica se o token está expirado
        var tokenExpirationClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
        if (tokenExpirationClaim == null)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Token inválido: sem informação de expiração." });
            return;
        }

        var tokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(tokenExpirationClaim.Value)).DateTime;
        if (tokenExpirationDate < DateTime.UtcNow)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Token expirado. Por favor, renove o token." });
            return;
        }
    }
}
