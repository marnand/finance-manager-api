namespace FinanceManager.Model.DTO;

public record class CreateTransactionDTO(string Type, string Description, string Category, decimal Amount, DateTime Date);
