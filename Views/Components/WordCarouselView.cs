using Microsoft.Maui.Controls;
using WordMate.Core.Models;
using System.Collections.Generic;

namespace WordMate.Views.Components;

public class WordCarouselView : CarouselView
{
    private Label _emptyStateLabel;
    public WordCarouselView()
    {
        ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
        {
            ItemSpacing = 10 
        };

        ItemTemplate = new DataTemplate(() =>
        {
            var wordLbl = CreateLabel(24, "Text");
            var translationLbl = CreateLabel(16, "Translation");
            var definitionLbl = CreateLabel(14, "Definition");

            var frame = new Frame
            {
                CornerRadius = 10,
                Padding = 10,
                WidthRequest = 300,
                BorderColor = Color.FromHex("ffbd59"),
                BackgroundColor = Colors.White,
                    
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
        });

        IsBounceEnabled = false;

        _emptyStateLabel = new Label
        {
            Text = "No words available",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };
    }

    public void SetWordsSource(IEnumerable<Word> words)
    {
        if (words == null || !HasWords(words))
        {
            ItemsSource = null;
            ShowEmptyState(true);
        }
        else
        {
            ItemsSource = words;
            ShowEmptyState(false);
        }
    }

    private void ShowEmptyState(bool isEmpty)
    {
        if (isEmpty)
        {
            if (Parent is Layout layout)
            {
                layout.Children.Add(_emptyStateLabel);
            }
            _emptyStateLabel.IsVisible = true;
        }
        else
        {
            _emptyStateLabel.IsVisible = false;
        }
    }

    private bool HasWords(IEnumerable<Word> words)
    {
        return words != null && System.Linq.Enumerable.Any(words);
    }

    private Label CreateLabel(double fontSize, string bindingProperty)
    {
        var label = new Label
        {
            FontSize = fontSize,
            Padding = 10
        };
        label.SetBinding(Label.TextProperty, bindingProperty);
        return label;
    }
}
