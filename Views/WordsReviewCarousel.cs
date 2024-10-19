using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;
using WordMate.Models;
using System.Collections.Generic;
using System.Linq;

namespace WordMate.Views;
public class WordsReviewCarousel : StackLayout
{
    public WordsReviewCarousel()
    {
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

        this.Children.Add(wordsOnReview);
        this.Children.Add(wordsCarousel);
        this.Children.Add(recallBtn);

        this.Spacing = 10;
        this.Margin = 10;
        this.HorizontalOptions = LayoutOptions.Center;
    }

    public void SetWordsSource(IEnumerable<object> words)
    {
        ((CarouselView)this.Children[1]).ItemsSource = words;
    }
}
