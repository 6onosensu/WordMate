using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordMate.Models;

namespace WordMate.Data;
public class WordManagementDB
{
    private readonly SQLiteAsyncConnection _connection;

    public WordManagementDB(SQLiteAsyncConnection connection)
    {
        _connection = connection;
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

    public async Task<bool> UpdateWordProgress(Guid wordId, bool isCorrect, CategoryManagementDB categoryManagementDB)
    {
        var word = await GetWordById(wordId);
        if (word == null) return false;

        if (isCorrect)
        {
            word.SuccessCount++;

            if (word.CategoryId == 1 && word.SuccessCount >= 4)
            {
                await categoryManagementDB.ChangeWordCategory(word.Id, 2);
                word.SuccessCount = 0;
            }
            else if (word.CategoryId == 2 && word.SuccessCount >= 6)
            {
                await categoryManagementDB.ChangeWordCategory(word.Id, 3);
                word.SuccessCount = 0;
            }
        }
        await SaveWord(word);
        return true;
    }

    public Task<int> CountWordsInCategory(int categoryId)
    {
        return _connection.Table<Word>().Where(w => w.CategoryId == categoryId).CountAsync();
    }
}
