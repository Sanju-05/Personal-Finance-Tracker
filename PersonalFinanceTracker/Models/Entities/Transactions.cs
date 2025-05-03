namespace PersonalFinanceTracker.Models.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public int UserId { get; set; } //foreign key
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? TransactionType { get; set; }
        public DateTime Date { get; set; }

        //foreign key navigation property
        public User User { get; set; }
    }
}
