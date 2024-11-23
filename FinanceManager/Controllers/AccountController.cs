using FinanceManager.Model;
using FinanceManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> Get(int id = 0)
        {
            var accounts = await _accountService.Get(id);
            return Ok(id == 0 ? accounts.FirstOrDefault() : accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account accountParam)
        {
            await _accountService.Create(accountParam);
            return CreatedAtAction(nameof(Get), new { id = accountParam.Id }, accountParam);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _accountService.Remove(id);
            return Ok();
        }
    }
}
