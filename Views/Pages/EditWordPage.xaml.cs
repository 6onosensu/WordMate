using Microsoft.Maui.Controls;
using System;
using WordMate.Data;
using WordMate.Models;

namespace WordMate.Views.Pages
{
    public partial class EditWordPage : ContentPage
    {
        private WordDB _wordDB;
        private Word _word;

        public EditWordPage(WordDB wordDB, Word word)
        {
            InitializeComponent();
            _wordDB = wordDB;
            _word = word;

            WordLabel.Text = _word.Text;
            TranslationLabel.Text = _word.Translation;
            DefinitionLabel.Text = _word.Definition;
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            ActionButtonsContainer.IsVisible = false;

            EditContainer.IsVisible = true;

            EditWordEntry.Text = _word.Text;
            EditTranslationEntry.Text = _word.Translation;
            EditDefinitionEntry.Text = _word.Definition;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _word.Text = EditWordEntry.Text;
            _word.Translation = EditTranslationEntry.Text;
            _word.Definition = EditDefinitionEntry.Text;

            await _wordDB.WordManager.SaveWord(_word);

            WordLabel.Text = _word.Text;
            TranslationLabel.Text = _word.Translation;
            DefinitionLabel.Text = _word.Definition;

            EditContainer.IsVisible = false;

            ActionButtonsContainer.IsVisible = true;

            await DisplayAlert("Success", "Word updated successfully!", "OK");
        }

        private async void OnCancelEditClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Discard changes?", "Yes", "No");
            if (confirm)
            {
                EditContainer.IsVisible = false;
                ActionButtonsContainer.IsVisible = true;
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this word?", "Yes", "No");
            if (confirm)
            {
                await _wordDB.WordManager.DeleteWord(_word.Id);
                await DisplayAlert("Success", "Word deleted successfully!", "OK");

                await Navigation.PopAsync();
            }
        }
    }
}
