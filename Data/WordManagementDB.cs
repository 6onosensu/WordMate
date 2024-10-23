using SQLite;
using WordMate.Core.Models;

namespace WordMate.Data;
public class WordManagementDB
{
    private readonly SQLiteAsyncConnection _connection;
    private readonly CategoryManagementDB _categoryManagementDB;

    public WordManagementDB(SQLiteAsyncConnection connection, CategoryManagementDB categoryManagementDB)
    {
        _connection = connection;
        _categoryManagementDB = categoryManagementDB;
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

        await _categoryManagementDB.UpdateWordCountForCategory(word.CategoryId);
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
}
