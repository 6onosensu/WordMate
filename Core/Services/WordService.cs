using WordMate.Core.Interfaces;
using WordMate.Core.Models;

namespace WordMate.Core.Services;
public class WordService
{
    private readonly IWordRepository _wordRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRefreshManager _refreshManager;

    public WordService( IWordRepository wordRepository, ICategoryRepository categoryRepository, IRefreshManager refreshManager )
    {
        _wordRepository = wordRepository;
        _categoryRepository = categoryRepository;
        _refreshManager = refreshManager;
    }

    public async Task SaveWordAsync(Word word)
    {
        await _wordRepository.SaveWord(word);
    }

    private async Task UpdateCategoryWordCountAsync(int categoryId)
    {
        await _categoryRepository.UpdateWordCountForCategory(categoryId);
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

            if (word.CategoryId == 1 && word.SuccessCount >= 3)
            {
                await ChangeWordCategory(word, 2);
            }
            else if (word.CategoryId == 2 && word.SuccessCount >= 5)
            {
                await ChangeWordCategory(word, 3);
            }
        }
        await _wordRepository.SaveWord(word);
        await UpdateCategoryWordCounts();
        return true;
    }

    private async Task ChangeWordCategory(Word word, int newCategoryId)
    {
        var oldCategoryId = word.CategoryId;
        await _wordRepository.ChangeWordCategory(word.Id, newCategoryId);
        word.SuccessCount = 0;
    }

    private async Task UpdateCategoryWordCounts()
    {
        for (int i = 1; i <= 3; i++)
        {
            await _categoryRepository.UpdateWordCountForCategory(i);
        }
    }

    public async Task<bool> DeleteWordAsync(Guid wordId)
    {
        var word = await _wordRepository.GetWordById(wordId);
        if (word == null) return false;

        await _wordRepository.DeleteWord(word);
        await UpdateCategoryAndRefreshUI(word.CategoryId);

        return true;
    }
    private async Task UpdateCategoryAndRefreshUI(int categoryId)
    {
        await _categoryRepository.UpdateWordCountForCategory(categoryId);
        await _refreshManager.RefreshPageComponents();
    }

    private async Task AddWordsAsync(List<Word> words)
    {
        foreach (var word in words)
        {
            await SaveWordAsync(word);
        }
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
}
