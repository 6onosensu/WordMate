using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMate.Views.Pages;
using WordMate.Core.Services;
using WordMate.Core.Models;

namespace WordMate.Views.Components;
public class WordsReviewSection : StackLayout
{
    private WordService _wordService;
    private CarouselView _carouselView;
    private Label _emptyStateLabel;
    public WordsReviewSection(WordService wordService)
    {
        _wordService = wordService;
        InitializeUI();
    }

    private async void InitializeUI()
    {
        var wordsOnReviewLabel = CreateLabel("Words on Review");
        _carouselView = await CreateCarousel();
        var recallButton = CreateRecallButton();

        Children.Add(wordsOnReviewLabel);
        Children.Add(_carouselView);
        Children.Add(recallButton);

        Spacing = 15;
        Margin = 15;
        HorizontalOptions = LayoutOptions.Center;
    }

    public void SetCarouselSource(IEnumerable<Word> words)
    {
        _carouselView.ItemsSource = words;
    }

    public async void RefreshCarousel()
    {
        var updatedWords = await _wordService.GetWordsByCategoryAsync(2);
        SetCarouselSource(updatedWords);
    }

    private Label CreateLabel(string text)
    {
        return new Label
        {
            Text = text,
            FontSize = 22,
            HorizontalOptions = LayoutOptions.Center
        };
    }

    private async Task<CarouselView> CreateCarousel()
    {
        var carouselView = new CarouselView
        {
            WidthRequest = 400,
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 10,
                SnapPointsType = SnapPointsType.MandatorySingle,
                SnapPointsAlignment = SnapPointsAlignment.Center,
            },
            ItemTemplate = new DataTemplate(() =>
            {
                var wordLbl = CreateCarouselLbl(24, LineBreakMode.WordWrap, "Text");
                var translationLbl = CreateCarouselLbl(16, LineBreakMode.WordWrap, "Translation");
                var definitionLbl = CreateCarouselLbl(14, LineBreakMode.WordWrap, "Definition");

                var frame = new Frame
                {
                    CornerRadius = 10,
                    Padding = 10,
                    WidthRequest = 280,
                    BorderColor = Color.FromHex("ffbd59"),
                    BackgroundColor = Colors.White,

                    Content = new StackLayout
                    {
                        Children = { wordLbl, translationLbl, definitionLbl },
                        Spacing = 5,

                    }
                };

                return frame;
            }),
            //PeekAreaInsets = new Thickness(20, 0),
        };

        return carouselView;
    }

    private Label CreateCarouselLbl(double fontSize, LineBreakMode lineBreakMode, string bindingProperty = null)
    {
        Label label = new Label
        {
            FontSize = fontSize,
            Padding = 10,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            LineBreakMode = lineBreakMode,
        };

        if (bindingProperty == null) 
        {
            label.SetBinding(Label.TextProperty, "No words available");
        }
        else
        {
            label.SetBinding(Label.TextProperty, bindingProperty);
        }
        
        return label;
    }

    private Button CreateRecallButton()
    {
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
        recallBtn.Clicked += OnRecallBtnClicked;

        return recallBtn;
    }


    private async void OnRecallBtnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new CategoryWordsPage(2, _wordService));
    }
}
