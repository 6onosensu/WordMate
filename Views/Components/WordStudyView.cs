using WordMate.Core.Models;
using WordMate.Core.Services;
using Microsoft.Maui.Controls;
using System;


namespace WordMate.Views.Components
{
    public class WordStudyView : StackLayout
    {
        private Label _wordLbl, _promptLbl, _feedbackLbl;
        private Entry _inputEntry;
        private Button _checkBtn, _nextBtn;
        private Word _currentWord;
        private WordService _wordService;
        private int _currentIndex = 0;
        private List<Word> _wordsList;

        public WordStudyView(WordService wordService, List<Word> wordsList)
        {
            _wordService = wordService;
            _wordsList = wordsList;

            _wordLbl = new Label
            {
                Text = "",
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

            _feedbackLbl = new Label
            {
                Text = "",
                FontSize = 16,
                TextColor = Colors.Green,
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

            _checkBtn = new Button
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
            _checkBtn.Clicked += OnCheckButtonClicked;

            var cardFrame = new Frame
            {
                BorderColor = Color.FromHex("ffbd59"),
                BackgroundColor = Colors.White,
                CornerRadius = 20,
                Padding = 20,
                Content = new StackLayout
                {
                    Children = { _wordLbl, _promptLbl, _inputEntry, _checkBtn },
                    Spacing = 20
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 300
            };

            Children.Add(cardFrame);

            _nextBtn = new Button
            {
                Text = "Next Word",
                FontSize = 20,
                HeightRequest = 60,
                WidthRequest = 300,
                Margin = 20,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("ffde59"),
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,

            };
            _nextBtn.Clicked += OnNextBtnKlicked;

            Children.Add(_feedbackLbl);
            Children.Add(_nextBtn);

            LoadWords();
        }

        public void LoadWords()
        {
            _currentWord = _wordsList[_currentIndex];
            UpdateWordDisplay();
        }

        private void LoadNextWord()
        {
            _currentIndex++;
            if (_currentIndex < _wordsList.Count)
            {
                _currentWord = _wordsList[_currentIndex];
                UpdateWordDisplay();
            }
            else
            {
                _feedbackLbl.Text = "You've completed all words!";
                _inputEntry.IsEnabled = false;
                _checkBtn.IsEnabled = false;
                _nextBtn.IsEnabled = false;
            }
        }

        private void UpdateWordDisplay()
        {
            _feedbackLbl.Text = "";
            _inputEntry.Text = "";

            if (_currentWord.SuccessCount == 0)
            {
                _wordLbl.Text = _currentWord.Text;
                _promptLbl.Text = "Input the translation:";
                _inputEntry.Placeholder = "The translation is...";
            }
            else if (_currentWord.SuccessCount == 1)
            {
                _wordLbl.Text = _currentWord.Translation;
                _promptLbl.Text = "Input the word:";
                _inputEntry.Placeholder = "The word is...";
            }
            else if (_currentWord.SuccessCount >= 2)
            {
                _wordLbl.Text = _currentWord.Definition;
                _promptLbl.Text = "Input the word according to the definition:";
                _inputEntry.Placeholder = "The word is...";
            }
        }

        private Boolean IsCorrect()
        {
            var userInput = _inputEntry.Text?.Trim().ToLower();
            var isCorrect = false;
            var wordSuccess = _currentWord.SuccessCount;

            if (wordSuccess == 0)
            {
                isCorrect = userInput == _currentWord.Translation.ToLower();
            }
            else if (wordSuccess == 1)
            {
                isCorrect = userInput == _currentWord.Text.ToLower();
            }
            else if (wordSuccess == 2)
            {
                isCorrect = userInput == _currentWord.Text.ToLower();
            }

            return isCorrect;
        }

        private async void OnCheckButtonClicked(object sender, EventArgs e)
        {
            /*var userInput = _inputEntry.Text?.Trim().ToLower();
            var isCorrect = false;
            var wordSuccess = _currentWord.SuccessCount;

            if (wordSuccess == 0)
            {
                isCorrect = userInput == _currentWord.Translation.ToLower();
            }
            else if (wordSuccess == 1)
            {
                isCorrect = userInput == _currentWord.Text.ToLower();
            }
            else if (wordSuccess == 2)
            {
                isCorrect = userInput == _currentWord.Text.ToLower();
            }*/
            if (IsCorrect())
            {
                _feedbackLbl.Text = "Correct!";
                _feedbackLbl.TextColor = Colors.Green;
                //await _wordService.UpdateWordProgress(_currentWord.Id, isCorrect);
            }
            else
            {
                _feedbackLbl.Text = "Incorrect!";
                _feedbackLbl.TextColor = Colors.Red;
            }
        }

        private async void OnNextBtnKlicked(object sender, EventArgs e)
        {
            var isCorrect = IsCorrect();
            if (isCorrect)
            {
                await _wordService.UpdateWordProgress(_currentWord.Id, isCorrect);
            }

            LoadNextWord();
        }
    }
}
