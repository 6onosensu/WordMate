using Microsoft.Maui.Controls;
using WordMate.Core.Services;
using WordMate.Views.Components;
using WordMate.Data;

namespace WordMate.Views;
public partial class MainPage : ContentPage
{
    private WordService _wordService;
    private CategoryService _categoryService;
    private RefreshManager _refreshManager;
    private CategoryGrid _categoryGrid;
    private AllWordsListView _allWordsListView;
    private WordsReviewSection _wordsReviewSection;

    public MainPage()
    {
        InitializePage();
    }
    private async Task InitializePage()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WordMate.db3");
        var database = new WordMateDatabase(dbPath);

        await database.InitializeDatabase();

        var (wordRepository, categoryRepository) = database.CreateRepositories();

        _wordService = new WordService(wordRepository, categoryRepository, null);
        _categoryService = new CategoryService(categoryRepository);

        _refreshManager = new RefreshManager(_allWordsListView, _categoryGrid, _wordsReviewSection, _wordService, _categoryService);

        SetupPage();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_wordService != null && _categoryService != null)
        {
            await InitializeDB();
            await _refreshManager.RefreshPageComponents();
        }
        else
        {
            await DisplayAlert("Error", "Services are not initialized", "OK");
        }
        //await InitializeDB();
        //await _refreshManager.RefreshPageComponents();
    }

    private async Task InitializeDB()
    {
        await _categoryService.InitializeCategories();
        var allWords = await _wordService.GetAllWordsAsync();
        _allWordsListView.SetWordsSource(allWords);
    }

    private void SetupPage()
    {
        var headerView = new HeaderView();

        _categoryGrid = new CategoryGrid(_categoryService, _wordService);
        _wordsReviewSection = new WordsReviewSection();
        _allWordsListView = new AllWordsListView(_wordService, _refreshManager);


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

        var footerView = new FooterView(_wordService);

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