using Microsoft.Maui.Controls;
using System.IO;
using WordMate.Data;
using WordMate.Views.Components;

namespace WordMate.Views;
public partial class MainPage : ContentPage
{
    private WordDB _wordDB;
    private CategoryGrid _categoryGrid;
    private AllWordsListView _allWordsListView;
    private WordsReviewSection _wordsReviewSection;
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeDB();
        await LoadWordsForReview();
        await LoadAllWords();
    }

    private async Task InitializeDB()
    {
        await _wordDB.InitializeDatabase(); 
    }

    private async Task LoadWordsForReview()
    {
        var reviewWords = await _wordDB.WordManager.GetWordsByCategory(2);
        _wordsReviewSection.SetWords(reviewWords);
    }

    private async Task LoadAllWords()
    {
        var allWords = await _wordDB.WordManager.GetWords();

        _allWordsListView = new AllWordsListView(_wordDB, OnWordAdded);
        _allWordsListView.SetWordsSource(allWords);
    }

    private void SetupPage()
    {
        var headerView = new HeaderView();

        _categoryGrid = new CategoryGrid(_wordDB);
        _wordsReviewSection = new WordsReviewSection();
        _allWordsListView = new AllWordsListView(_wordDB, OnWordAdded);
        var mainContent = new StackLayout
        {
            Children =
            {
                _categoryGrid,
                _wordsReviewSection,
                _allWordsListView,
            }
        };
        var scrollView = new ScrollView
        {
            Content = mainContent
        };

        var footerView = new FooterView(_wordDB);

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
    private async void OnWordAdded()
    {
        _categoryGrid.Refresh();

        var allWords = await _wordDB.WordManager.GetWords();
        _allWordsListView.SetWordsSource(allWords);
    }
}