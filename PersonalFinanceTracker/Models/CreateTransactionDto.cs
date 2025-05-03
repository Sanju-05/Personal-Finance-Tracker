namespace PersonalFinanceTracker.Models
{
    public class CreateTransactionDto
    {
        public int UserId { get; set; } //foreign key
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
