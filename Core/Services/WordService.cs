using SQLitePCL;
using WordMate.Core.Interfaces;
using WordMate.Core.Models;

namespace WordMate.Core.Services;
public class WordService
{
    private readonly IWordRepository _wordRepository;
    private readonly ICategoryRepository _categoryRepository;
    private IRefreshManager _refreshManager;

    public WordService(IWordRepository wordRepository, ICategoryRepository categoryRepository)
    {
        _wordRepository = wordRepository;
        _categoryRepository = categoryRepository;
    }

    private void UpdateAll()
    {
        _refreshManager.RefreshPageComponents();
    }

    public async Task SaveWordAsync(Word word) 
    { 
        await _wordRepository.SaveWord(word);
        UpdateAll();
    }

    public async Task<List<Word>> GetAllWordsAsync() => await _wordRepository.GetWords();

    public async Task<List<Word>> GetWordsByCategoryAsync(int categoryId) => await _wordRepository.GetWordsByCategory(categoryId);

    public async Task<bool> UpdateWordProgress(Guid wordId, bool isCorrect)
    {
        var word = await _wordRepository.GetWordById(wordId);
        if (word == null) return false;

        if (isCorrect)
        {
            word.SuccessCount++;

            if (word.CategoryId == 1 && word.SuccessCount <= 2)
            {
                await ChangeWordCategory(word, 2);
            }
            else if (word.CategoryId == 3 && word.SuccessCount >= 5)
            {
                await ChangeWordCategory(word, 3);
            }
        }

        await _wordRepository.SaveWord(word);
        UpdateAll();
        return true;
    }

    private async Task ChangeWordCategory(Word word, int newCategoryId)
    {
        var oldCategoryId = word.CategoryId;
        await _wordRepository.ChangeWordCategoryAsync(word.Id, newCategoryId);
    }

    public async Task<bool> DeleteWordAsync(Guid wordId)
    {
        var word = await _wordRepository.GetWordById(wordId);
        if (word == null) return false;
        var categoryIndex = word.CategoryId;
        await _wordRepository.DeleteWord(word);

        UpdateAll();

        return true;
    }

    private async Task AddWordsAsync(List<Word> words)
    {
        foreach (var word in words)
        {
            await SaveWordAsync(word);
        }

        UpdateAll();
    }

    public async Task AddSampleWords()
    {
        if (await _wordRepository.CountWordsInCategory(1) == 0)
        {
            await AddWordsAsync(new List<Word>
            {
                new Word("apple", "яблоко", "A common fruit."),
                new Word("run", "бежать", "To move swiftly on foot."),
                new Word("study", "учить", "To engage in learning.")
            });
        }

        if (await _wordRepository.CountWordsInCategory(2) == 0)
        {
            await AddWordsAsync(new List<Word>
            {
                new Word("repeat", "повторять", "To say or do something again.", categoryId: 2),
                new Word("remember", "запомнить", "To keep in mind.", categoryId: 2)
            });
        }

        if (await _wordRepository.CountWordsInCategory(3) == 0)
        {
            await AddWordsAsync(new List<Word>
            {
                new Word("final", "финал", "The end or last part of something.", categoryId: 3)
            });
        }
    }

    public async Task<int> CountWordsInCategory(int categoryId)
    {
        return await _wordRepository.CountWordsInCategory(categoryId);
    }

    public void SetRefreshManager(RefreshManager refreshManager)
    {
        _refreshManager = refreshManager;
    }
}
