namespace FinanceManager.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // income or expense
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
