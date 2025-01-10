using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;
using FinanceManager.Service.Extensions;
using FinanceManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinanceManager.Controllers;

/// <summary>
/// UserController
/// </summary>
/// <param name="userService"></param>
/// <param name="jwtToken"></param>
[ApiController, Route("api/users")]
public class UserController(JWTToken jwtToken, IUserService userService) : BaseController(jwtToken)
{
    private readonly JWTToken _jwtToken = jwtToken;
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Cadastrar um usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns>User</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDTO user)
    {
        var result = await _userService.Create(user);
        return result.Match<IActionResult>(onSuccess => CreatedAtAction(nameof(Get), new { id = onSuccess.Data }, onSuccess), BadRequest);
    }

    /// <summary>
    /// Obtém lista de usuários ou 1 específico por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Lista ou apenas 1 usuário</returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int id = 0)
    {
        var result = await _userService.Get(id);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    /// <summary>
    /// Realiza login
    /// </summary>
    /// <param name="login">Email e senha</param>
    /// <returns></returns>
    /// <remarks>
    /// POST /api/auth/login
    ///     {
    ///         "email": "mail@mail.com",
    ///         "password": "password123"
    ///     }
    /// </remarks>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        var result = await _userService.Login(login);
        return result.Match(Ok, ErrorHandle);
    }

    private IActionResult ErrorHandle(Result<string> result)
    {
        if (result.Error?.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
        if (result.Error?.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(result);

        return BadRequest(result);
    }
}
