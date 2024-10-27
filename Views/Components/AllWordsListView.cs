using WordMate.Core.Models;
using WordMate.Core.Services;
using WordMate.Views.Pages;

namespace WordMate.Views.Components;
public class AllWordsListView : StackLayout
{
    private Entry _searchEntry;
    private Label _allWordsLbl;
    private ListView _wordsListView;
    private List<Word> _allWords;
    private Button _addWordButton;
    private readonly WordService _wordService;

    public AllWordsListView(WordService wordService)
    {
        _wordService = wordService;

        InitializeComponents();
        LoadWordsAsync();
    }

    private void InitializeComponents()
    {
        _searchEntry = new Entry
        {
            Placeholder = "Search word...",
            BackgroundColor = Colors.WhiteSmoke
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

        _wordsListView = CreateListView();

        Children.Add(_searchEntry);
        Children.Add(headerGrid);
        Children.Add(_wordsListView);
        Padding = 15;
        Spacing = 10;
    }

    private ListView CreateListView()
    {
        return new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var wordLbl = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    FontSize = 18
                };
                wordLbl.SetBinding(Label.TextProperty, "Text");

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

                grid.Add(wordLbl, 0, 0);
                grid.Add(definitionLbl, 0, 0);
                grid.Add(editBtn, 1, 0);

                var viewCell = new ViewCell { View = grid };
                viewCell.Tapped += (s, e) =>
                {
                    wordLbl.IsVisible = !wordLbl.IsVisible;
                    definitionLbl.IsVisible = !definitionLbl.IsVisible;
                };

                return viewCell;
            })
        };
    }

    private async void OnEditBtnClicked(object? sender, EventArgs e)
    {
        var editButton = (Button)sender;
        var selectedWord = (Word)editButton.BindingContext;
        await Navigation.PushAsync(new EditWordPage(_wordService, selectedWord));
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchQuery;
        if (e.NewTextValue != null)
        {
            searchQuery = e.NewTextValue;
        }
        else
        {
            searchQuery = "";
        }

        FilterWords(searchQuery);
    }

    private void FilterWords(string searchQuery)
    {
        var filteredWords = _allWords
            .Where(word => word.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        UpdateWordsList(filteredWords);
    }

    private void UpdateWordsList(IEnumerable<Word> words)
    {
        _wordsListView.ItemsSource = words;
        int wordCount;
        if (words != null)
        {
            wordCount = words.Count();
        }
        else
        {
            wordCount = 0;
        }
        _allWordsLbl.Text = $"My words ({wordCount})";
    }

    private async void OnAddWordClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordPage(_wordService));
    }

    public async Task LoadWordsAsync()
    {
        var words = await _wordService.GetAllWordsAsync();
        SetWordsSource(words);
    }
    public void SetWordsSource(IEnumerable<Word> words)
    {
        _allWords = words.ToList();
        UpdateWordsList(_allWords);
    }
}
