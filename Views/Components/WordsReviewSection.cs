using Microsoft.Maui.Controls;
using System.Collections.Generic;
using WordMate.Data;
using WordMate.Core.Models;

namespace WordMate.Views.Components;
public class WordsReviewSection : StackLayout
{
    private WordCarouselView _wordsCarousel;
    public WordsReviewSection()
    {
        var wordsOnReview = new Label
        {
            Text = "Words on Review",
            FontSize = 22,
            HorizontalOptions = LayoutOptions.Center
        };

        _wordsCarousel = new WordCarouselView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,

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
        recallBtn.Clicked += OnRecallBtnClicked;

        Children.Add(wordsOnReview);
        Children.Add(_wordsCarousel);
        Children.Add(recallBtn);

        Spacing = 10;
        Margin = 10;
        HorizontalOptions = LayoutOptions.Center;
    }

    private async void OnRecallBtnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReviewPage());
    }

    public void SetWords(IEnumerable<Word> words)
    {
        _wordsCarousel.SetWordsSource(words);
    }
}
