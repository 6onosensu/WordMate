using Microsoft.Maui.Controls;
using System;
using WordMate.Core.Services;
using WordMate.Views.Pages;

namespace WordMate.Views.Components;
public class FooterView : StackLayout
{
    private readonly WordService _wordService;
    public FooterView(WordService wordService)
    {
        _wordService = wordService;

        var learnBtn = new Button
        {
            Text = "Learn",
            FontSize = 20,
            HeightRequest = 50,
            WidthRequest = 150,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.White,
            TextColor = Color.FromHex("ffde59")
        };
        learnBtn.Clicked += OnLearnBtnClicked;

        var playBtn = new Button
        {
            Text = "Play",
            FontSize = 20,
            HeightRequest = 50,
            WidthRequest = 150,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.White,
            TextColor = Color.FromHex("ffde59")
        };
        playBtn.Clicked += OnPlayBtnClicked;

        var spacer = new BoxView
        {
            WidthRequest = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        var spacer1 = new BoxView
        {
            WidthRequest = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        Orientation = StackOrientation.Horizontal;
        HorizontalOptions = LayoutOptions.FillAndExpand;
        VerticalOptions = LayoutOptions.Center;
        BackgroundColor = Color.FromHex("ffde59");
        HeightRequest = 60;
        Spacing = 10;

        Children.Add(spacer);
        Children.Add(learnBtn);
        Children.Add(playBtn);
        Children.Add(spacer1);
    }

    private async void OnPlayBtnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlayPage());
    }

    private async void OnLearnBtnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new CategoryWordsPage(1, _wordService));
    }
}
