using Microsoft.Maui.Controls;
using WordMate.Models;
using WordMate.Data;
using WordMate.Views.Pages;

namespace WordMate.Views.Components;
public class AllWordsListView : StackLayout
{
    private Entry _searchEntry;
    private Label _allWordsLbl, _wordLbl;
    private ListView _wordsListView;
    private List<Word> _allWords;
    private Button _addWordButton;
    private WordDB _wordDB;
    private readonly Action _onWordAdded;
    //private WordsRefreshViewList _wordsListView;
    public AllWordsListView(WordDB wordDB, Action onWordAdded)
    {
        _wordDB = wordDB;
        _onWordAdded = onWordAdded;

        _searchEntry = new Entry
        {
            Placeholder = "Search word...",
            BackgroundColor = Colors.WhiteSmoke,
        };
        _searchEntry.TextChanged += OnSearchTextChanged;

        _allWordsLbl = new Label { FontSize = 22 };

        _addWordButton = new Button
        {
            Text = "Add New Word",
            FontSize = 14,
            HeightRequest = 38,
            HorizontalOptions = LayoutOptions.End,
            BackgroundColor = Color.FromHex("ffbd59"),
            TextColor = Colors.White
        };
        _addWordButton.Clicked += OnAddWordClicked;

        var headerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };
        headerGrid.Add(_allWordsLbl, 0, 0);
        headerGrid.Add(_addWordButton, 1, 0);

        //_wordsListView = new WordsRefreshViewList(_wordDB);

        _wordsListView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                _wordLbl = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    FontSize = 18
                };
                _wordLbl.SetBinding(Label.TextProperty, "Text");

                var definitionLbl = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    FontSize = 18,
                    IsVisible = false
                };
                definitionLbl.SetBinding(Label.TextProperty, "Definition");

                var editBtn = new Button
                {
                    Text = "Edit",
                    FontSize = 14,
                    HeightRequest = 38,
                    TextColor = Colors.Black,
                    BackgroundColor = Color.FromHex("ffea94"),
                    HorizontalOptions = LayoutOptions.End
                };
                editBtn.SetBinding(Button.BindingContextProperty, ".");
                editBtn.Clicked += OnEditBtnClicked;

                var grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                grid.Add(_wordLbl, 0, 0);
                grid.Add(definitionLbl, 0, 0);
                grid.Add(editBtn, 1, 0);

                var viewCell = new ViewCell { View = grid };
                viewCell.Tapped += (s, e) =>
                {
                    _wordLbl.IsVisible = !_wordLbl.IsVisible;
                    definitionLbl.IsVisible = !definitionLbl.IsVisible;
                };

                return viewCell;
            })
        };

        Children.Add(_searchEntry);
        Children.Add(headerGrid);
        Children.Add(_wordsListView);
        Padding = 15;
        Spacing = 10;

        LoadWordsAsync();
    }

    private async void OnEditBtnClicked(object? sender, EventArgs e)
    {
        if (sender is Button editButton && editButton.BindingContext is Word selectedWord)
        {
            await Navigation.PushAsync(new EditWordPage(_wordDB, selectedWord));
        }
    }
    public void SetWordsSource(IEnumerable<Word> words)
    {
        _allWords = words.ToList();
        UpdateWordsList(_allWords);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string filter = e.NewTextValue ?? "";
        FilterWords(filter);
    }
    private void FilterWords(string filter)
    {
        var filteredWords = _allWords
            .Where(word => word.Text.Contains(filter, StringComparison.OrdinalIgnoreCase))
            .ToList();

        UpdateWordsList(filteredWords);
    }
    private void UpdateWordsList(IEnumerable<Word> words)
    {
        _wordsListView.ItemsSource = words;
        int wordCount = words != null ? words.Count() : 0;
        _allWordsLbl.Text = $"All my words ({wordCount})";
    }
    private async void OnAddWordClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordPage(_wordDB, _onWordAdded));
    }
    public async Task LoadWordsAsync()
    {
        var words = await _wordDB.WordManager.GetWords();
        SetWordsSource(words);
    }
}
