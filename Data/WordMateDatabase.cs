using SQLite;
using System.Threading.Tasks;
using WordMate.Core.Interfaces;
using WordMate.Core.Models;

namespace WordMate.Data
{
    public class WordMateDatabase
    {
        private readonly SQLiteAsyncConnection _connection;
        public WordMateDatabase(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeDatabase()
        {
            await _connection.CreateTableAsync<Word>();
            await _connection.CreateTableAsync<Category>();
        }

        public (WordRepository wordRepository, CategoryRepository categoryRepository) CreateRepositories()
        {
            var categoryRepository = new CategoryRepository(_connection);
            var wordRepository = new WordRepository(_connection, categoryRepository);

            categoryRepository.SetWordRepository(wordRepository);

            return (wordRepository, categoryRepository);
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _connection;
        }
    }
}