namespace FinanceAPP.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// The amount spent in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Tracks when the transaction was added to the system
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Tracks when the transaction actually occurred in real life
        /// </summary>
        public DateOnly OperationDate { get; set; }

        // Parameterless constructor for deserialization
        public Transaction()
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Category = string.Empty;
            Description = string.Empty;
        }

        // Constructor for manual creation
        public Transaction(string category, string description, decimal amount, DateOnly operationDate)
            : this() // Call parameterless constructor first
        {
            Category = category;
            Description = description;
            Amount = amount;
            OperationDate = operationDate;
        }
    }
}