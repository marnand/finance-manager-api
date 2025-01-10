using FinanceManager.Model.DTO;
using FinanceManager.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers;

/// <summary>
/// Encapsula recursos a ser compartilhados entre todas as Controllers
/// </summary>
public class BaseController(JWTToken tokenService) : ControllerBase
{
    private readonly JWTToken _jwtToken = tokenService;

    /// <summary>
    /// Dados dos usuário logado
    /// </summary>
    protected new UserDTO User
    {
        get
        {
            var token = HttpContext.Request.Headers.Authorization;
            var result = _jwtToken.GetUserFromToken(token[0]?.Split(" ")[1] ?? string.Empty);
            
            return result.Match(
                onSuccess => new UserDTO(int.Parse(onSuccess.Data.UserId), onSuccess.Data.UserEmail),
                onError => new UserDTO(0, string.Empty)
            );
        }
    }
}
