using Microsoft.Maui.Controls;
using WordMate.Core.Models;

namespace WordMate.Views.Components
{
    public class WordCarouselView : CarouselView
    {
        public WordCarouselView()
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 10 
            };

            ItemTemplate = new DataTemplate(() =>
            {
                var wordLbl = new Label 
                { 
                    FontSize = 24,
                    Padding = 10,
                };
                wordLbl.SetBinding(Label.TextProperty, "Text");

                var translationLbl = new Label 
                { 
                    FontSize = 16,
                    Padding = 10,
                };
                translationLbl.SetBinding(Label.TextProperty, "Translation");

                var definitionLbl = new Label 
                { 
                    FontSize = 14,
                    Padding = 10,
                };
                definitionLbl.SetBinding(Label.TextProperty, "Definition");

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
        }

        public void SetWordsSource(IEnumerable<Word> words)
        {
            ItemsSource = words;
        }
    }
}
