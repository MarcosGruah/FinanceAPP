using Blazored.LocalStorage;
using FinanceAPP.Models;

namespace FinanceAPP.Services
{
    public class RecurringTransactionService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "recurringTransactions";

        public List<RecurringTransaction> RecurringTransactions { get; private set; } = new();

        public RecurringTransactionService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task LoadAsync()
        {
            var list = await _localStorage.GetItemAsync<List<RecurringTransaction>>(StorageKey);
            if (list != null)
            {
                RecurringTransactions = list;
            }
        }

        public async Task AddAsync(RecurringTransaction recurring)
        {
            RecurringTransactions.Add(recurring);
            await SaveAsync();
        }

        public async Task UpdateAsync(RecurringTransaction updated)
        {
            var existing = RecurringTransactions.Find(r => r.Id == updated.Id);
            if (existing != null)
            {
                existing.CategoryId = updated.CategoryId;
                existing.Description = updated.Description;
                existing.Amount = updated.Amount;
                existing.Type = updated.Type;
                existing.RecurrenceType = updated.RecurrenceType;
                existing.DayOfMonth = updated.DayOfMonth;
                existing.IntervalDays = updated.IntervalDays;
                existing.StartDate = updated.StartDate;
                existing.TotalOccurrences = updated.TotalOccurrences;
                await SaveAsync();
            }
        }

        public async Task DeleteAsync(RecurringTransaction recurring)
        {
            RecurringTransactions.Remove(recurring);
            await SaveAsync();
        }

        /// <summary>
        /// Gera todas as transações baseadas no template recorrente
        /// </summary>
        public List<Transaction> GenerateTransactions(RecurringTransaction template)
        {
            var transactions = new List<Transaction>();
            DateOnly currentDate = template.StartDate;

            for (int i = 0; i < template.TotalOccurrences; i++)
            {
                // Calcula a próxima data baseado no tipo de recorrência
                if (i > 0) // Pula na primeira iteração (usa StartDate)
                {
                    if (template.RecurrenceType == RecurrenceType.DayOfMonth)
                    {
                        // Adiciona um mês e ajusta para o dia específico
                        currentDate = currentDate.AddMonths(1);

                        // Se o dia do mês especificado não existe (ex: 31 em fevereiro),
                        // usa o último dia do mês
                        int targetDay = template.DayOfMonth ?? currentDate.Day;
                        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                        int actualDay = Math.Min(targetDay, daysInMonth);

                        currentDate = new DateOnly(currentDate.Year, currentDate.Month, actualDay);
                    }
                    else if (template.RecurrenceType == RecurrenceType.EveryXDays)
                    {
                        // Adiciona X dias
                        currentDate = currentDate.AddDays(template.IntervalDays ?? 1);
                    }
                }

                // Cria a transação
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    CategoryId = template.CategoryId,
                    Description = template.Description,
                    Amount = template.Amount,
                    Type = template.Type,
                    OperationDate = currentDate,
                    Date = DateTime.UtcNow,
                    RecurringTransactionId = template.Id
                };

                transactions.Add(transaction);
            }

            return transactions;
        }

        private async Task SaveAsync()
        {
            await _localStorage.SetItemAsync(StorageKey, RecurringTransactions);
        }
    }
}