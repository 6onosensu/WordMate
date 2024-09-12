using Microsoft.Maui.Controls;

namespace WordMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(60) },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = new GridLength(50) }
                }

            };

            var header = new StackLayout
            {
                BackgroundColor = Colors.LightGray,
                Padding = 10,
                Children =
                {
                    new Label
                    {
                        Text = "Header",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };
            grid.Add(header, 0, 0);

            var scrollView = new ScrollView();
            var contentStack = new StackLayout
            {
                Padding = 10,
                Children =
                {
                    new Label { Text = "This is the content area.", FontSize = 20, VerticalOptions = LayoutOptions.Start },
                    new Label { Text = "More content...", FontSize = 20, VerticalOptions = LayoutOptions.Start },
                    new Label { Text = "Even more content...", FontSize = 20, VerticalOptions = LayoutOptions.Start },
                    new Label { Text = "And more content to scroll.", FontSize = 20, VerticalOptions = LayoutOptions.Start },
                    new Label { Text = "Still more content!", FontSize = 20, VerticalOptions = LayoutOptions.Start },
                    new Label { Text = "You get the idea!", FontSize = 20, VerticalOptions = LayoutOptions.Start }
                }
            };
            scrollView.Content = contentStack;
            grid.Add(scrollView, 0, 1);

            // Footer
            var footer = new StackLayout
            {
                BackgroundColor = Colors.LightGray,
                Padding = 10,
                Children =
                {
                    new Label
                    {
                        Text = "Footer",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };
            grid.Add(footer, 0, 2);

            Content = grid;
        }

    }

}
