using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views;
public partial class WordListPage : ContentPage
{
    private readonly WordService _wordService;
    private readonly int _categoryId;

    public WordListPage(WordService wordService, int categoryId)
    {
        InitializeComponent();
        _wordService = wordService;
        _categoryId = categoryId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadWords();
    }

    private async Task LoadWords()
    {
        List<Word> words = await _wordService.GetWordsByCategoryAsync(_categoryId);
        WordsListView.ItemsSource = words;
    }

    private void OnWordTapped(object sender, EventArgs e)
    {
        var stackLayout = (StackLayout)sender;

        var translationLabel = (Label)stackLayout.FindByName("TranslationLabel");
        var definitionLabel = (Label)stackLayout.FindByName("DefinitionLabel");

        if (translationLabel != null && definitionLabel != null)
        {
            translationLabel.IsVisible = !translationLabel.IsVisible;
            definitionLabel.IsVisible = !definitionLabel.IsVisible;
        }
    }
}