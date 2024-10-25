using SQLite;
using System.Threading.Tasks;
using WordMate.Core.Models;

namespace WordMate.Data
{
    public class WordMateDatabase
    {
        private SQLiteAsyncConnection _connection;
        public WordMateDatabase(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public void InitializeDatabaseAsync()
        {
            _connection.CreateTableAsync<Word>().Wait();
            _connection.CreateTableAsync<Category>().Wait();
        }


        public (WordRepository wordRepository, CategoryRepository categoryRepository) CreateRepositories()
        {
            var categoryRepository = new CategoryRepository(_connection);
            var wordRepository = new WordRepository(_connection, categoryRepository);

            categoryRepository.SetWordRepository(wordRepository);

            return (wordRepository, categoryRepository);
        }
    }
}