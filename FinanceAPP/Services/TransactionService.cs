using Blazored.LocalStorage;
using FinanceAPP.Models;

namespace FinanceAPP.Services
{
    public class TransactionService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly CategoryService _categoryService;
        private const string StorageKey = "transactions";

        public List<Transaction> Transactions { get; private set; } = new();

        public TransactionService(ILocalStorageService localStorage, CategoryService categoryService)
        {
            _localStorageService = localStorage;
            _categoryService = categoryService;
        }

        public async Task LoadAsync()
        {
            var transactionList = await _localStorageService.GetItemAsync<List<Transaction>>(StorageKey);
            if (transactionList != null)
            {
                Transactions = transactionList;
            }
        }
        public async Task AddAsync(Transaction transaction)
        {
            Transactions.Add(transaction);
            await SaveAsync();
        }
        public async Task UpdateAsync(Transaction updated)
        {
            var result = Transactions.Find(t => t.Id == updated.Id);
            if (result != null)
            {
                result.CategoryId = updated.CategoryId;
                result.Description = updated.Description;
                result.OperationDate = updated.OperationDate;
                result.Amount = updated.Amount;
                result.Type = updated.Type;

                await SaveAsync();
            }
        }
        public async Task DeleteAsync(Transaction transaction)
        {
            Transactions.Remove(transaction);
            await SaveAsync();
        }
        public string GetCategoryName(Guid categoryId)
        {
            return _categoryService.GetCategoryName(categoryId);
        }
        private async Task SaveAsync()
        {
            await _localStorageService.SetItemAsync(StorageKey, Transactions);
        }
    }
}
