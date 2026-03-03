namespace FinanceAPP.Models
{
    public class RecurringTransaction
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Transaction.TransactionType Type { get; set; }
        public int? DayOfMonth { get; set; }
        public int? IntervalDays { get; set; }
        public DateOnly StartDate { get; set; }
        public int TotalOccurrences { get; set; }
        public RecurrenceType RecurrenceType { get; set; }

        public RecurringTransaction()
        {
            Id = Guid.NewGuid();
            CategoryId = Guid.Empty;
            Description = string.Empty;
        }
    }
    public enum RecurrenceType
    {
        DayOfMonth,   // Repeat on 8th of each month
        EveryXDays    // Repeat every 7 days
    }
}
