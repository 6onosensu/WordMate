using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using WordMate.Data;
using WordMate.Views.Components;

namespace WordMate.Services
{
    public class RefreshManager
    {
        private readonly WordDB _wordDB;
        public RefreshManager(WordDB wordDB)
        {
            _wordDB = wordDB;
        }
        public async Task RefreshPageComponents()
        {
            var mainPage = Application.Current.MainPage as MainPage;
            if (mainPage != null)
            {
                var allWordsListView = mainPage.FindByName<AllWordsListView>("_allWordsListView");
                if (allWordsListView != null)
                {
                    await allWordsListView.LoadWordsAsync();
                }

                var categoryGrid = mainPage.FindByName<CategoryGrid>("_categoryGrid");
                if (categoryGrid != null)
                {
                    categoryGrid.Refresh();
                }
            }
        }
    }
}
