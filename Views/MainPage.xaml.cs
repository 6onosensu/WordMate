using Microsoft.Maui.Controls;

namespace WordMate.Views;
public partial class MainPage : ContentPage
{
    Grid grid, gr;
    ScrollView scrollView;
    Image logo, profile;

    public MainPage()
    {
        grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(70) },
                new RowDefinition { Height = new GridLength(50) },
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = new GridLength(50) } 
            }
        };

        logo = new Image
        {
            Source = "wordmatemain.png",
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center           
        };
        grid.Add(logo, 0, 0);

        gr = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
            },
        };
            
        profile = new Image
        {
            Source = "profile.png",
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        gr.Add(profile, 0, 0);
        //gr.Add(CurrentLevel, 1, 0);
        grid.Add(gr, 0, 1);

        scrollView = new ScrollView();
        StackLayout contentStack = new StackLayout
        {
            Padding = 10,
            Children =
            {
                new Label 
                {
                    Text = "fnsdjnj",
                }
            }
        };
        scrollView.Content = contentStack;
        grid.Add(scrollView, 0, 2);

        StackLayout footer = new StackLayout
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
        grid.Add(footer, 0, 3);
        Image lessons = new Image
        {
            Source = "lessons.png",
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        Content = grid;
    }
}