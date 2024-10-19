using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordMate.Models;

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

    public Task<int> GetWordCount()
    {
        return _connection.Table<Word>().CountAsync();
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

    public async Task AddSampleWords()
    {
        var quantityWordsInLearning = await CountWordsInCategory(1);
        var quantityWordsInReviewing = await CountWordsInCategory(2);
        if (quantityWordsInLearning == 0)
        {
            var words = new List<Word>
            {
                new Word("apple", "яблоко", "A common fruit."),
                new Word("run", "бежать", "To move swiftly on foot."),
                new Word("study", "учить", "To engage in learning.")
            };
            foreach (var word in words)
            {
                await SaveWord(word);
            }
        }
        else if (quantityWordsInReviewing == 0)
        {
            var words = new List<Word>
            {
                new Word("repeat", "повторять", "To say or do something again.", categoryId: 2),
                new Word("remember", "запомнить", "To keep in mind.", categoryId: 2)
            };
            foreach (var word in words)
            {
                await SaveWord(word);
            }
        }
        else
        {
            Console.WriteLine("Words already exist in Category 1 or Category 2.");
        }
        
    }
}
