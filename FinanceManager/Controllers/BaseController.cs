using FinanceManager.Model;
using FinanceManager.Model.DTO;
using FinanceManager.Service;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    /// <summary>
    /// Encapsula recursos a ser compartilhados entre todas as Controllers
    /// </summary>
    public class BaseController(JwtTokenService tokenService) : ControllerBase
    {
        private readonly JwtTokenService _jwtToken = tokenService;

        /// <summary>
        /// Dados dos usuário logado
        /// </summary>
        protected new UserDTO User
        {
            get
            {
                var token = HttpContext.Request.Headers.Authorization;

                var result = _jwtToken.GetUserFromToken(token[0].Split(" ")[1]);
                
                return result.Match(
                    onSuccess => new UserDTO(int.Parse(onSuccess.UserId), onSuccess.UserEmail),
                    onError => new UserDTO(0, string.Empty)
                );
            }
        }
    }
}
