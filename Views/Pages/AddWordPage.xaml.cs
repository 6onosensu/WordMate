using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views.Pages;

public partial class AddWordPage : ContentPage
{
    private readonly WordService _wordService;
    private readonly RefreshManager _refreshManager;

    public AddWordPage(WordService wordService, RefreshManager refreshManager)
    {
        InitializeComponent();
        _wordService = wordService;
        _refreshManager = refreshManager;
    }

    private async void OnSaveWordClicked(object sender, EventArgs e)
    {
        string wordText = WordEntry.Text;
        string translationText = TranslationEntry.Text;
        string definitionText = DefinitionEntry.Text;

        var checkWordText = string.IsNullOrWhiteSpace(wordText);
        var checkTranslationText = string.IsNullOrWhiteSpace(translationText);
        var checkDefinitionText = string.IsNullOrWhiteSpace(definitionText);

        if (checkWordText || checkTranslationText || checkDefinitionText)
        {
            string missingFields = "";

            if (checkWordText) missingFields += "Word";
            if (checkTranslationText)
                missingFields += (missingFields.Length > 0 ? ", " : "") + "Translation";
            if (checkDefinitionText)
                missingFields += (missingFields.Length > 0 ? ", " : "") + "Definition";

            await DisplayAlert("Error", $"Please enter the following fields: {missingFields}.", "OK");
            return;
        }

        Word newWord = new Word
        {
            Text = wordText,
            Translation = translationText,
            Definition = definitionText,
        };

        await _wordService.SaveWordAsync(newWord);
        await _refreshManager.RefreshAfterUpdating(1);

        await DisplayAlert("Success", "Word added successfully!", "OK");
        await Navigation.PopAsync();
    }
}