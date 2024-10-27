using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views.Pages
{
    public partial class EditWordPage : ContentPage
    {
        private readonly WordService _wordService;
        private readonly RefreshManager _refreshManager;
        private Word _word;

        public EditWordPage(WordService wordService, Word word)
        {
            _wordService = wordService;
            _word = word;

            InitializeComponent();
            UpdateWordDisplay();
        }

        private void UpdateWordDisplay()
        {
            WordLabel.Text = _word.Text;
            TranslationLabel.Text = _word.Translation;
            DefinitionLabel.Text = _word.Definition;
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            ToggleEditMode(true);
        }

        private void ToggleEditMode(bool isEditing)
        {
            ActionButtonsContainer.IsVisible = !isEditing;
            EditContainer.IsVisible = isEditing;

            if (isEditing)
            {
                EditWordEntry.Text = _word.Text;
                EditTranslationEntry.Text = _word.Translation;
                EditDefinitionEntry.Text = _word.Definition;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            UpdateWord();

            await _wordService.SaveWordAsync(_word);

            UpdateWordDisplay();
            ToggleEditMode(false);

            await DisplayAlert("Success", "Word updated successfully!", "OK");
        }

        private void UpdateWord()
        {
            _word.Text = EditWordEntry.Text;
            _word.Translation = EditTranslationEntry.Text;
            _word.Definition = EditDefinitionEntry.Text;
        }

        private async void OnCancelEditClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Discard changes?", "Yes", "No");
            if (confirm)
            {
                ToggleEditMode(false);
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this word?", "Yes", "No");
            if (confirm)
            {
                await _wordService.DeleteWordAsync(_word.Id);
                await DisplayAlert("Success", "Word deleted successfully!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
