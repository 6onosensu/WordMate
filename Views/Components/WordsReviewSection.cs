using WordMate.Core.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordMate.Views.Pages;
using WordMate.Core.Services;

namespace WordMate.Views.Components;
public class WordsReviewSection : StackLayout
{
    private WordCarouselView _wordsCarousel;
    private WordService _wordService;
    public WordsReviewSection(WordService wordService)
    {
        _wordService = wordService;
        InitializeUI();
    }

    private void InitializeUI()
    {
        var wordsOnReview = CreateLabel("Words on Review");
        _wordsCarousel = CreateWordCarousel();
        var recallButton = CreateButton();

        Children.Add(wordsOnReview);
        Children.Add(_wordsCarousel);
        Children.Add(recallButton);

        Spacing = 10;
        Margin = 10;
        HorizontalOptions = LayoutOptions.Center;
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

    private WordCarouselView CreateWordCarousel()
    {
        return new WordCarouselView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
    }

    private Button  CreateButton()
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

    public void SetWords(IEnumerable<Word> words)
    {
        _wordsCarousel.SetWordsSource(words);
    }
}
