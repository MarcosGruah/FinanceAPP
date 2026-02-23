using Blazored.LocalStorage;
using FinanceAPP.Models;

namespace FinanceAPP.Services
{
    public class DataBackupService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly CategoryService _categoryService;
        private readonly TransactionService _transactionService;

        public DataBackupService(
            ILocalStorageService localStorage,
            CategoryService categoryService,
            TransactionService transactionService)
        {
            _localStorage = localStorage;
            _categoryService = categoryService;
            _transactionService = transactionService;
        }

        public DataBackup CreateBackup()
        {
            return new DataBackup
            {
                Categories = _categoryService.Categories,
                Transactions = _transactionService.Transactions
            };
        }

        public async Task RestoreBackup(DataBackup backup)
        {
            // Direct LocalStorage write (fast)
            await _localStorage.SetItemAsync("categories", backup.Categories);
            await _localStorage.SetItemAsync("transactions", backup.Transactions);

            // Reload services so they have the new data
            await _categoryService.LoadAsync();
            await _transactionService.LoadAsync();
        }
    }
}