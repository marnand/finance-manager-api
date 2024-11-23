using FinanceManager.Model;
using FinanceManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Create(User userParam)
    {
        var id = await _userService.Create(userParam);
        userParam.Id = id;
        return CreatedAtAction(nameof(Get), new { id }, userParam);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id = 0)
    {
        var users = await _userService.Get(id);
        return Ok(id > 0 ? users.FirstOrDefault() : users);
    }
}
