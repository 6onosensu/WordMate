using WordMate.Core.Models;

namespace WordMate.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task InitializeCategories();
        Task UpdateWordCountForCategories();
    }
}
