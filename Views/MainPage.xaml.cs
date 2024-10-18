using Microsoft.Maui.Controls;
using System.Data;
using WordMate.Data;
using WordMate.Models;

namespace WordMate.Views;
public partial class MainPage : ContentPage
{
    private WordDB _wordDB;
    public MainPage()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WordMate.db3");
        _wordDB = new WordDB(dbPath);

        SetupPage();
    }
    public MainPage(WordDB wordDB)
    {
        _wordDB = wordDB;

        SetupPage();
    }
    private void SetupPage()
    {
        var headerView = new HeaderView();

        var categoryGrid = new CategoryGrid(_wordDB);
        var wordsReviewCarousel = new WordsReviewCarousel();
        var allWordsListView = new AllWordsListView();
        var mainContent = new StackLayout
        {
            Children =
            {
                categoryGrid,
                wordsReviewCarousel,
                allWordsListView,
            }
        };
        var scrollView = new ScrollView
        {
            Content = mainContent
        };

        var footerView = new FooterView();

        var grid = new Grid
        {
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto } 
            }
        };
        grid.Add(headerView, 0, 0); 
        grid.Add(scrollView, 0, 1);
        grid.Add(footerView, 0, 2);

        Content = grid;
    }
}