using Microsoft.Maui.Controls;
using WordMate.Core.Interfaces;
using WordMate.Views.Components;

namespace WordMate.Core.Services
{
    public class RefreshManager : IRefreshManager
    {
        private readonly AllWordsListView _allWordsListView;
        private readonly CategoryGrid _categoryGrid;
        private readonly WordsReviewSection _wordsReviewSection;
        private readonly WordService _wordService;
        private readonly CategoryService _categoryService;

        public RefreshManager(  AllWordsListView allWordsListView, 
                                CategoryGrid categoryGrid, 
                                WordsReviewSection wordsReviewSection, 
                                WordService wordService,
                                CategoryService categoryService)
        {
            _allWordsListView = allWordsListView;
            _categoryGrid = categoryGrid;
            _wordsReviewSection = wordsReviewSection;
            _wordService = wordService;
            _categoryService = categoryService;
        }

        public async Task RefreshAfterUpdating(int categoryId)
        {
            await _categoryService.UpdateCountForCategory(categoryId);
            await RefreshPageComponents();
        }

        public async Task RefreshPageComponents()
        {
            if (_allWordsListView != null)
            {
                await _allWordsListView.LoadWordsAsync();
            }

            _categoryGrid.Refresh();

            _wordsReviewSection.RefreshCarousel();
        }
    }
}