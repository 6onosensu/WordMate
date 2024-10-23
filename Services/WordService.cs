using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordMate.Core.Models;
using WordMate.Data;

namespace WordMate.Services
{
    public class WordService
    {
        private readonly WordDB _wordDB;
        private readonly WordManagementDB _wordMDB;
        private readonly CategoryManagementDB _categoryMDB;
        public WordService(WordDB wordDB)
        {
            _wordDB = wordDB;
            _wordMDB = _wordDB.WordManager;
            _categoryMDB = _wordDB.CategoryManager;
        }

        public async Task<List<Word>> GetAllWordsAsync()
        {
            return await _wordMDB.GetWords();
        }

        public async Task<List<Word>> GetWordsByCategoryAsync(int categoryId)
        {
            return await _wordMDB.GetWordsByCategory(categoryId);
        }

        public async Task<bool> UpdateWordProgress(Guid wordId, bool isCorrect)
        {
            var word = await _wordMDB.GetWordById(wordId);
            if (word == null) return false;

            if (isCorrect)
            {
                word.SuccessCount++;

                if (word.CategoryId == 1 && word.SuccessCount >= 3)
                {
                    await _categoryMDB.ChangeWordCategory(word.Id, 2);
                    word.SuccessCount = 0;
                }
                else if (word.CategoryId == 2 && word.SuccessCount >= 5)
                {
                    await _categoryMDB.ChangeWordCategory(word.Id, 3); 
                    word.SuccessCount = 0;
                    await _categoryMDB.UpdateWordCountForCategory(word.CategoryId);
                }

            }
            await _wordMDB.SaveWord(word); 
            return true;
        }

        public async Task<bool> DeleteWordAsync(Guid wordId)
        {
            var word = await _wordMDB.GetWordById(wordId);
            if (word == null) return false;

            await _wordMDB.DeleteWord(word);
            await UpdateCategoryAndRefreshUI(word.CategoryId);

            return true;
        }
        private async Task UpdateCategoryAndRefreshUI(int categoryId)
        {//// может быть дело в возращении на страницу обратно (надо узнать как работает возращению на страницу заного ли он отрисовывает)
            await _categoryMDB.UpdateWordCountForCategory(categoryId);

            var refreshManager = new RefreshManager(_wordDB);
            await refreshManager.RefreshPageComponents();
        }

        public async Task AddSampleWords()
        {
            var quantityWordsInLearning = await _wordMDB.CountWordsInCategory(1);
            var quantityWordsInReviewing = await _wordMDB.CountWordsInCategory(2);

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
                    await _wordMDB.SaveWord(word);
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
                    await _wordMDB.SaveWord(word);
                }
            }
        }

    }
}
