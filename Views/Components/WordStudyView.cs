using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordMate.Data;
using WordMate.Models;

namespace WordMate.Views.Components
{
    public class WordStudyView : StackLayout
    {
        private Label _wordLbl, _promptLbl;
        private Entry _inputEntry;
        private Button _nextBtn;
        private Word _currentWord;
        private WordDB _wordDB;
        private int _currentStage = 0;

        public WordStudyView(Word currentWord, WordDB wordDB)
        {
            _wordDB = wordDB;
            _currentWord = currentWord;

            var headerView = new HeaderView();

            _wordLbl = new Label
            {
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _promptLbl = new Label
            {
                Text = "The word is...",
                FontSize = 18,
                TextColor = Color.FromHex("ffbd59"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _inputEntry = new Entry
            {
                Placeholder = "Input the word",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 200
            };

            _nextBtn = new Button
            {
                Text = "Check",
                FontSize = 18,
                BackgroundColor = Color.FromHex("ffbd59"),
                TextColor = Colors.White,
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 100
            };
            _nextBtn.Clicked += OnNextButtonClicked;

            var cardFrame = new Frame
            {
                BorderColor = Color.FromHex("ffbd59"),
                BackgroundColor = Colors.White,
                CornerRadius = 20,
                Padding = 20,
                Content = new StackLayout
                {
                    Children = { _wordLbl, _promptLbl, _inputEntry, _nextBtn },
                    Spacing = 20
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 300
            };

            Children.Add(headerView);
            Children.Add(cardFrame);

            UpdateWordDisplay();
        }

        private void UpdateWordDisplay()
        {
            switch (_currentStage)
            {
                case 1:
                    _wordLbl.Text = _currentWord.Translation;
                    _promptLbl.Text = "Input the word:";
                    _inputEntry.Placeholder = "The word is...";
                    break;
                case 2:
                    _wordLbl.Text = _currentWord.Text;
                    _promptLbl.Text = "Input the translation:";
                    _inputEntry.Placeholder = "The translation is...";
                    break;
                case 3:
                    _wordLbl.Text = _currentWord.Definition;
                    _promptLbl.Text = "Input the word according to the definition:";
                    _inputEntry.Placeholder = "The word is...";
                    break;
            }
            _inputEntry.Text = "";
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            _currentStage = (_currentStage + 1) % 3;
            UpdateWordDisplay();
        }
    }
}
