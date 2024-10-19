using Microsoft.Maui.Controls;
using System.Collections.Generic;
using WordMate.Models;
using System.Linq;
using WordMate.Data;

namespace WordMate.Views;
public class AllWordsListView : StackLayout
{
    private Entry _searchEntry;
    private Label _allWordsLbl;
    private ListView _wordsListView;
    private List<Word> _allWords;
    private Button _addWordButton;
    private WordDB _wordDB;
    private readonly Action _onWordAdded;
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

        _allWordsLbl = new Label
        {
            Text = "All my words (0): ",
            FontSize = 20
        };

        _addWordButton = new Button
        {
            Text = "Add New Word",
            FontSize = 16,
            HeightRequest = 40,
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

        _wordsListView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var wordAndTranslationLbl = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    FontSize = 16
                };
                wordAndTranslationLbl.SetBinding(Label.TextProperty, new Binding("Text"));

                var definitionLbl = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    FontSize = 16,
                    IsVisible = false
                };
                definitionLbl.SetBinding(Label.TextProperty, "Definition");

                var editBtn = new Button
                {
                    Text = "Edit",
                    FontSize = 16,
                    HeightRequest = 40,
                    TextColor = Colors.WhiteSmoke,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.FromHex("ffea94"),
                    HorizontalOptions = LayoutOptions.End
                };

                var tapGR = new TapGestureRecognizer();
                tapGR.Tapped += (s, e) =>
                {
                    definitionLbl.IsVisible = !definitionLbl.IsVisible;
                };

                wordAndTranslationLbl.GestureRecognizers.Add(tapGR);
                definitionLbl.GestureRecognizers.Add(tapGR);

                var grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                grid.Add(wordAndTranslationLbl, 0, 0);
                grid.Add(definitionLbl, 0, 0);
                grid.Add(editBtn, 1, 0);

                return new ViewCell { View = grid };
            })
        };

        this.Children.Add(_searchEntry);
        this.Children.Add(headerGrid);
        this.Children.Add(_wordsListView);
        this.Padding = 15;
        this.Spacing = 10;
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
}
