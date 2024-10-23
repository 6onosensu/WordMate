using Microsoft.Maui.Controls;
using WordMate.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordMate.Core.Models;

namespace WordMate.Views;

public partial class WordListPage : ContentPage
{
    private WordDB _wordDB;
    private int _categoryId;
    public WordListPage(WordDB wordDB, int categoryId)
	{
        InitializeComponent();
        _wordDB = wordDB;
        _categoryId = categoryId;

        LoadWords();
    }
    private async Task LoadWords()
    {
        List<Word> words = await _wordDB.WordManager.GetWordsByCategory(_categoryId);
        WordsListView.ItemsSource = words;
    }

    private void OnWordTapped(object sender, EventArgs e)
    {
        var stackLayout = (StackLayout)sender;

        var translationLabel = (Label)stackLayout.FindByName("TranslationLabel");
        var definitionLabel = (Label)stackLayout.FindByName("DefinitionLabel");

        if (translationLabel.IsVisible)
        {
            translationLabel.IsVisible = false;
            definitionLabel.IsVisible = true;
        }
        else
        {
            translationLabel.IsVisible = true;
            definitionLabel.IsVisible = false;
        }
    }
}