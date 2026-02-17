using Blazored.LocalStorage;
using FinanceAPP.Models;

namespace FinanceAPP.Services
{
    public class CategoryService
    {
        private readonly ILocalStorageService _localStorageService;
        private const string StorageKey = "categories";

        public List<Category> Categories { get; private set; } = new();

        public CategoryService(ILocalStorageService localStorage)
        {
            _localStorageService = localStorage;
        }

        public async Task LoadAsync()
        {
            var categoryList = await _localStorageService.GetItemAsync<List<Category>>(StorageKey);

            if (categoryList != null)
            {
                Categories = categoryList;
            }
        }

        public async Task AddAsync(Category category)
        {
            Categories.Add(category);
            await SaveAsync();
        }

        public async Task UpdateAsync(Category updated)
        {
            var result = Categories.Find(c => c.Id == updated.Id);

            if (result != null)
            {
                result.Name = updated.Name;
                result.Description = updated.Description;
                await SaveAsync();
            }

        }

        public async Task DeleteAsync(Category category)
        {
            Categories.Remove(category);
            await SaveAsync();
        }

        public string GetCategoryName(Guid categoryId)
        {
            return Categories.FirstOrDefault(c => c.Id == categoryId)?.Name ?? "Unknown";

        }

        private async Task SaveAsync()
        {
            await _localStorageService.SetItemAsync(StorageKey, Categories);
        }


    }
}
