using SQLite;
using WordMate.Core.Interfaces;
using WordMate.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordMate.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        private IWordRepository _wordRepository;

        public CategoryRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public Task<List<Category>> GetCategories()
        {
            return _connection.Table<Category>().ToListAsync();
        }

        public async Task InitializeCategories()
        {
            var categories = await GetCategories();

            if (categories.Count == 0)
            {
                var defaultCategories = new List<Category>
                {
                    new Category { Id = 1, Name = "Learning", WordsCount = 0 },
                    new Category { Id = 2, Name = "Review", WordsCount = 0 },
                    new Category { Id = 3, Name = "Learned", WordsCount = 0 },
                };

                foreach (var category in defaultCategories)
                {
                    await _connection.InsertOrReplaceAsync(category);
                }
            }
        }

        public async Task UpdateWordCountForCategory(int categoryId)
        {
            var category = await _connection.Table<Category>()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            if (category != null)
            {
                category.WordsCount = await _wordRepository.CountWordsInCategory(categoryId);

                await _connection.UpdateAsync(category);
            }
        }

        public void SetWordRepository(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
    }
}
