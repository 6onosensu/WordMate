using Microsoft.Maui.Controls;

namespace WordMate.Views.Components;
public class FooterView : StackLayout
{
    public FooterView()
    {
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
}
