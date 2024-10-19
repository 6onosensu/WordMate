using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordMate.Models;

namespace WordMate.Data;
public class CategoryManagementDB
{
    private readonly SQLiteAsyncConnection _connection;

    public CategoryManagementDB(SQLiteAsyncConnection connection)
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
            await _connection.InsertOrReplaceAsync(new Category { Id = 1, Name = "Learning", WordsCount = 0 });
            await _connection.InsertOrReplaceAsync(new Category { Id = 2, Name = "Review", WordsCount = 0 });
            await _connection.InsertOrReplaceAsync(new Category { Id = 3, Name = "Learned", WordsCount = 0 });
        }
    }

    public async Task ChangeWordCategory(Guid wordId, int newCategoryId)
    {
        var wordManagementDB = new WordManagementDB(_connection, this);
        var word = await wordManagementDB.GetWordById(wordId);

        if (word != null)
        {
            var oldCategoryId = word.CategoryId;
            word.CategoryId = newCategoryId;
            await wordManagementDB.SaveWord(word);

            await UpdateWordCountForCategory(oldCategoryId);
            await UpdateWordCountForCategory(newCategoryId);
        }
    }

    public async Task UpdateWordCountForCategory(int categoryId)
    {
        var category = await _connection.Table<Category>()
            .Where(c => c.Id == categoryId)
            .FirstOrDefaultAsync();

        if (category != null)
        {
            var wordManagementDB = new WordManagementDB(_connection, this);
            category.WordsCount = await wordManagementDB.CountWordsInCategory(categoryId);
            await _connection.UpdateAsync(category);
        }
    }
}
