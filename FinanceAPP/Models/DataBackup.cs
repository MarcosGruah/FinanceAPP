namespace FinanceAPP.Models
{
    public class DataBackup
    {
        public List<Transaction> Transactions { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<RecurringTransaction> RecurringTransactions { get; set; } = new();
    }
}
