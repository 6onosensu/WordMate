using Microsoft.Maui.Controls;
using System.Collections.Generic;
using WordMate.Models;

namespace WordMate.Views;
public class AllWordsListView : StackLayout
{
    private Entry _searchEntry;
    private Label _allWordsLbl;
    private ListView _wordsListView;

    public AllWordsListView()
    {
        _searchEntry = new Entry
        {
            Placeholder = "Search word...",
            BackgroundColor = Colors.WhiteSmoke,
        };

        _allWordsLbl = new Label
        {
            Text = "All my words (0): ",
            FontSize = 20
        };

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
        this.Children.Add(_allWordsLbl);
        this.Children.Add(_wordsListView);

        this.Spacing = 10;
    }

    public void SetWordsSource(IEnumerable<Word> words)
    {
        _wordsListView.ItemsSource = words;

        int wordCount = words != null ? words.Count() : 0;
        _allWordsLbl.Text = $"All my words ({wordCount})";
    }
}
