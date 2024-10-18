using Microsoft.Maui.Controls;

namespace WordMate.Views;
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
            BackgroundColor = Colors.WhiteSmoke,
            TextColor = Colors.Black
        };

        var playBtn = new Button
        {
            Text = "Play",
            FontSize = 20,
            HeightRequest = 50,
            WidthRequest = 150,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.WhiteSmoke,
            TextColor = Colors.Black
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

        this.Orientation = StackOrientation.Horizontal;
        this.HorizontalOptions = LayoutOptions.FillAndExpand;
        this.VerticalOptions = LayoutOptions.Center;
        this.BackgroundColor = Color.FromHex("ffbd59");
        this.HeightRequest = 60;
        this.Spacing = 10;

        this.Children.Add(spacer);
        this.Children.Add(learnBtn);
        this.Children.Add(playBtn);
        this.Children.Add(spacer1);
    }
}
