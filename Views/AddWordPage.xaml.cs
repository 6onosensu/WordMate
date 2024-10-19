using Microsoft.Maui.Controls;
using System;
using WordMate.Data;
using WordMate.Models;

namespace WordMate.Views;

public partial class AddWordPage : ContentPage
{
    private readonly WordDB _wordDB;
    private readonly Action _onWordAdded;

    public AddWordPage(WordDB wordDB, Action onWordAdded)
    {
        InitializeComponent();
        _wordDB = wordDB;
        _onWordAdded = onWordAdded;
    }
    private async void OnSaveWordClicked(object sender, EventArgs e)
    {
        string wordText = WordEntry.Text;
        string translationText = TranslationEntry.Text;
        string definitionText = DefinitionEntry.Text;

        if (string.IsNullOrWhiteSpace(wordText) || string.IsNullOrWhiteSpace(translationText))
        {
            await DisplayAlert("Error", "Please enter both the word and the translation.", "OK");
            return;
        }

        Word newWord = new Word
        {
            Text = wordText,
            Translation = translationText,
            Definition = definitionText,
        };

        await _wordDB.WordManager.SaveWord(newWord);
        _onWordAdded?.Invoke();
        await DisplayAlert("Success", "Word added successfully!", "OK");
        await Navigation.PopAsync();
    }
}