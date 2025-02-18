using FinanceManager.Model.DTO;
using FinanceManager.Service.Extensions;
using FinanceManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinanceManager.Middleware;

namespace FinanceManager.Controllers;

/// <summary>
/// TransactionController
/// </summary>
/// <param name="transactionService"></param>
/// <param name="jwtToken"></param>
[ApiController, Route("api/v1/transactions"), ValidateToken]
public class TransactionController(JWTToken jwtToken, ITransactionService transactionService) : BaseController(jwtToken)
{
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// Cadastrar um usuário
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns>Transaction</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTransactionDTO transaction)
    {
        var result = await _transactionService.Create(transaction);
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
        var result = await _transactionService.Get(id);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
