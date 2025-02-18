using FinanceManager.Model.Control;
using System.Net;

namespace FinanceManager.Model;

public class Transaction
{
    public int Id { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Result<Transaction> Create(string type, string description, string category, decimal amount, DateTime date)
    {
        if (amount < 0.01M)
        {
            return Result<Transaction>.ResultError(
                "O valor não pode ser inferior a 0,01!",
                HttpStatusCode.UnprocessableEntity
            );
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Transaction>.ResultError(
                "A descrição não pode ser vazia!",
                HttpStatusCode.UnprocessableEntity
            );
        }

        return Result<Transaction>.Success(new Transaction()
        {
            Type = type,
            Description = description,
            Category = category,
            Amount = amount,
            Date = date,
            CreatedAt = DateTime.Now
        });
    }
}

public enum Type
{
    Income,
    Expense
}
