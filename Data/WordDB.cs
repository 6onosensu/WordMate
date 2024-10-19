using SQLite;
using System.Threading.Tasks;
using WordMate.Models;

namespace WordMate.Data
{
    public class WordDB
    {
        private readonly SQLiteAsyncConnection _connection;
        public WordManagementDB WordManager { get; private set; }
        public CategoryManagementDB CategoryManager { get; private set; }

        public WordDB(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Word>().Wait();
            _connection.CreateTableAsync<Category>().Wait();

            CategoryManager = new CategoryManagementDB(_connection);
            WordManager = new WordManagementDB(_connection, CategoryManager);
        }

        public async Task InitializeDatabase()
        {
            await CategoryManager.InitializeCategories();
            await WordManager.AddSampleWords();
        }
    }
}