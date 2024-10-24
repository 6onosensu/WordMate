using SQLite;
using WordMate.Core.Interfaces;
using WordMate.Core.Models;

namespace WordMate.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly ICategoryRepository _categoryRepository;

        public WordRepository(  SQLiteAsyncConnection connection,
                                ICategoryRepository categoryRepository)
        {
            _connection = connection;
            _categoryRepository = categoryRepository;
        }
        public Task<List<Word>> GetWords()
        {
            return _connection.Table<Word>().ToListAsync();
        }

        public Task<Word> GetWordById(Guid id)
        {
            return _connection.Table<Word>().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Word>> GetWordsByCategory(int categoryId)
        {
            return _connection.Table<Word>().Where(w => w.CategoryId == categoryId).ToListAsync();
        }

        public Task<int> GetWordCount()
        {
            return _connection.Table<Word>().CountAsync();
        }

        public async Task<int> SaveWord(Word word)
        {
            var existingWord = await GetWordById(word.Id);
            if (existingWord != null)
            {
                await _connection.UpdateAsync(word);
            }
            else
            {
                await _connection.InsertAsync(word);
            }
            return 1;
        }

        public Task<int> DeleteWord(Word word)
        {
            return _connection.DeleteAsync(word);
        }

        public async Task DeleteWord(Guid id)
        {
            await _connection.DeleteAsync<Word>(id);
        }

        public Task<int> CountWordsInCategory(int categoryId)
        {
            return _connection.Table<Word>().Where(w => w.CategoryId == categoryId).CountAsync();
        }

        public async Task ChangeWordCategory(Guid wordId, int newCategoryId)
        {
            var word = await GetWordById(wordId);

            if (word != null)
            {
                word.CategoryId = newCategoryId;
                await SaveWord(word);
            }
        }
    }
}
