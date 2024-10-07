using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMate
{
    public class WordsCarousel
    {
        public WordsCarousel() 
        {
            IndicatorView indicatorView = new IndicatorView
            {
                SelectedIndicatorColor = Colors.LightGray,
                HorizontalOptions = LayoutOptions.Center,
                IndicatorColor = Colors.Transparent,
                Margin = new Thickness(10),
                IndicatorTemplate = new DataTemplate(() =>
                {
                    Label label = new Label
                    {
                        Text = "\uf30c",
                        FontSize = 30,
                    };
                    return label;
                }),
            };

            CarouselView carouselView = new CarouselView
            {
                VerticalOptions = LayoutOptions.Center,
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal),
                IndicatorView = indicatorView,
            };

            carouselView.ItemsSource = new List<Word>
            {
                new Word {Id = new Guid(), EngWord = "Application", RusWord = "Приложение", Meaning = "An Icon on the main screen of your phone or menu is an application",},
                new Word {Id = new Guid(), EngWord = "Words", RusWord = "Слова", Meaning = "...",},
                new Word {Id = new Guid(), EngWord = "An Apple", RusWord = "Яблоко", Meaning = "fruit",},
                new Word {Id = new Guid(), EngWord = "To see", RusWord = "Видеть", Meaning = "action",},
            };

            carouselView.ItemTemplate = new DataTemplate(() =>
            {
                Image img = new Image
                {
                    WidthRequest = 150,
                    HeightRequest = 150,
                };
                img.SetBinding(Image.SourceProperty, "Image");

                Label eng = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 22,
                };
                eng.SetBinding(Label.TextProperty, "EngWord");
                
                Label description = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                description.SetBinding(Label.TextProperty, "Meaning");
                
                VerticalStackLayout vsl = new VerticalStackLayout() { img, eng, description};

                Frame frame = new Frame
                {
                    WidthRequest = 250,
                    HeightRequest = 300,
                };
                frame.Content = vsl;
                return frame;
            });
        }
    }
}
