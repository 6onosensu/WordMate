using Microsoft.Maui.Controls;

namespace WordMate.Views;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        var logo = new Image { Source = "logo.png", HeightRequest = 100, VerticalOptions = LayoutOptions.Start };

        var categoryGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            }
        };
        categoryGrid.Add(CreateCategoryLbl("Learning", 10), 0, 0);
        categoryGrid.Add(CreateCategoryLbl("Review", 5), 1, 0);
        categoryGrid.Add(CreateCategoryLbl("Learned", 20), 2, 0);

        var wordsOnReview = new Label
        {
            Text = "Words on Review",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        var wordsCarousel = new CarouselView
        {
            ItemTemplate = new DataTemplate(() =>
            {

                var wordLbl = new Label { FontSize = 22 };
                wordLbl.SetBinding(Label.TextProperty, "Text");

                var translationLbl = new Label { FontSize = 16 };
                translationLbl.SetBinding(Label.TextProperty, "Translation");

                var definitionLbl = new Label { FontSize = 14 };
                definitionLbl.SetBinding(Label.TextProperty, "Definition");

                var frame = new Frame
                {
                    CornerRadius = 10,
                    Padding = 10,
                    Margin = 10,
                    BorderColor = Colors.Black,
                    BackgroundColor = Colors.WhiteSmoke,
                    Content = new StackLayout
                    {
                        Children = { wordLbl, translationLbl, definitionLbl }
                    }
                };

                return new StackLayout
                {
                    Children = { frame },
                    Spacing = 10,
                };
            })
        };

        var recallBtn = new Button
        {
            Text = "Recall",
            FontSize = 20,
            HeightRequest = 60,
            WidthRequest = 300,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Color.FromHex("ffde59"),
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        };

        var carouselStack = new StackLayout
        {
            Children = { wordsOnReview, wordsCarousel, recallBtn },
            Spacing = 10,
            Margin = 10,
            HorizontalOptions= LayoutOptions.Center,
        };

        var searchEntry = new Entry {
            Placeholder = "Search word...",
            BackgroundColor = Colors.WhiteSmoke,
            TextColor = Colors.DarkGoldenrod,
        };

        var allWordsLbl = new Label 
        { 
            FontSize = 20,
        };

        var wordsListView = new ListView
        {
            ItemsSource = new List<string> { "Word1 - Translation1", "Word2 - Translation2" },
            ItemTemplate = new DataTemplate(() =>
            {
                var wordLbl = new Label();
                wordLbl.SetBinding(Label.TextProperty, ".");

                var editBtn = new Button { Text = "Edit" };

                var stack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { wordLbl, editBtn }
                };

                return new ViewCell { View = stack };
            })
        };

        var learnBtn = new Button 
        { 
            Text = "Learn",
            FontSize = 20,
            HeightRequest = 40,
            WidthRequest = 200,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.WhiteSmoke,
        };
        var playBtn = new Button { 
            Text = "Play",
            FontSize = 20,
            HeightRequest = 40,
            WidthRequest = 200,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.WhiteSmoke,
        };

        var footerStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.FromHex("#ffea94"),
            Spacing = 10,
            Children = { learnBtn, playBtn }
        };

        var mainStack = new StackLayout
        {
            Children =
                {
                    logo,
                    categoryGrid,
                    carouselStack,
                    searchEntry,
                    allWordsLbl,
                    wordsListView,
                    footerStack
                }
        };

        Content = new ScrollView { Content = mainStack };
    }
    private Frame CreateCategoryLbl(string category, int wordCount)
    {
        return new Frame
        {
            BorderColor = Color.FromHex("ffbd59"),
            CornerRadius = 0,
            Content = new StackLayout
            {
                Children = 
                {
                    new Label
                    {
                        Text = $"{category} ({wordCount})",
                        FontSize = 14,
                        
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    },
                }
            },
        };
    }
}