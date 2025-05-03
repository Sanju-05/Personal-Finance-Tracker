namespace PersonalFinanceTracker.Models
{
    public class TransactionReportViewModel
    {
        public List<TransactionCategoryReportViewModel> IncomeData { get; set; }
        public List<TransactionCategoryReportViewModel> ExpenseData { get; set; }
    }


}
