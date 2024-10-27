using WordMate.Core.Interfaces;
using WordMate.Core.Models;

namespace WordMate.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<List<Category>> GetAllCategoriesAsync() => _categoryRepository.GetCategories();

        public Task InitializeCategories() => _categoryRepository.InitializeCategories();

        public Task UpdateCountForCategories() => _categoryRepository.UpdateWordCountForCategories();
    }
}
