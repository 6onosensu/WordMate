using SQLite;
using WordMate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordMate.Data;
public class WordDB
{
    private readonly SQLiteAsyncConnection _connection;
    public WordDB(string dbPath)
    {
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Word>().Wait();
        _connection.CreateTableAsync<Category>().Wait();
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

    public async Task<int> SaveWord(Word word)
    {
        var existingWord = await GetWordById(word.Id);
        if (existingWord != null)
        {
            return await _connection.UpdateAsync(word);
        }
        else
        {
            return await _connection.InsertAsync(word);
        }
    }

    public Task<int> DeleteWord(Word word)
    {
        return _connection.DeleteAsync(word);
    }
    public Task<List<Category>> GetCategories()
    {
        return _connection.Table<Category>().ToListAsync();
    }

    public async Task ChangeWordCategory(Guid wordId, int newCategoryId)
    {
        var word = await GetWordById(wordId);
        if (word != null)
        {
            var oldCategoryId = word.CategoryId;
            word.CategoryId = newCategoryId;
            await SaveWord(word);

            await UpdateWordCountForCategory(oldCategoryId);
            await UpdateWordCountForCategory(newCategoryId);
        }
    }

    private async Task UpdateWordCountForCategory(int categoryId)
    {
        var category = await _connection.Table<Category>().Where(c => c.Id == categoryId).FirstOrDefaultAsync();
        if (category != null)
        {
            category.WordsCount = await CountWordsInCategory(categoryId);
            await _connection.UpdateAsync(category); 
        }
    }

    public async Task<bool> UpdateWordProgress(Guid wordId, bool isCorrect)
    {
        var word = await GetWordById(wordId);
        if (word == null) return false;

        if (isCorrect)
        {
            word.SuccessCount++;

            if (word.CategoryId == 1)
            {
                if (word.SuccessCount >= 4)
                {
                    await ChangeWordCategory(wordId, 2);
                    word.SuccessCount = 0;
                }
            }
            else if (word.CategoryId == 2)
            {
                if (word.SuccessCount >= 6)
                {
                    await ChangeWordCategory(wordId, 3);
                    word.SuccessCount = 0;
                }
            }
        }
        await SaveWord(word);
        return true;
    }


    public async Task<int> CountWordsInCategory(int categoryId)
    {
        return await _connection.Table<Word>().Where(w => w.CategoryId == categoryId).CountAsync();
    }

}
