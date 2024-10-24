using System.Threading.Tasks;
using WordMate.Core.Models;

namespace WordMate.Core.Interfaces
{
    public interface IWordRepository
    {
        Task<List<Word>> GetWords();
        Task<Word> GetWordById(Guid id);
        Task<List<Word>> GetWordsByCategory(int categoryId);
        Task<int> SaveWord(Word word);
        Task<int> DeleteWord(Word word);
        Task DeleteWord(Guid id);
        Task ChangeWordCategory(Guid wordId, int newCategoryId);
        Task<int> CountWordsInCategory(int categoryId);
        Task<int> GetWordCount();
    }
}
