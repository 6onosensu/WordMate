using Microsoft.Maui.Controls;

namespace WordMate.Views;
public class HeaderView : StackLayout
{
    public HeaderView()
    {
        var logo = new Image
        {
            Source = "logo.png",
            HeightRequest = 100,
            VerticalOptions = LayoutOptions.Start
        };

        this.Children.Add(logo);
        this.VerticalOptions = LayoutOptions.Start;
    }
}
