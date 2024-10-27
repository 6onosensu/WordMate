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

        public RefreshManager(  AllWordsListView allWordsListView, 
                                CategoryGrid categoryGrid, 
                                WordsReviewSection wordsReviewSection)
        {
            _allWordsListView = allWordsListView;
            _categoryGrid = categoryGrid;
            _wordsReviewSection = wordsReviewSection;
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