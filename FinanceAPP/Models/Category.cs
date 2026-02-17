namespace FinanceAPP.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Tracks when the transaction was added to the system
        /// </summary>
        public DateTime Date { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Name = string.Empty;
            Description = string.Empty;
        }

        public Category(string name, string description) : this()
        {

            Name = name;
            Description = description;

        }

    }
}
