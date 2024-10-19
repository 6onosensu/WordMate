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

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += async (s, e) =>
        {
            Navigation.PopAsync();
        };
        logo.GestureRecognizers.Add(tapGestureRecognizer);

        this.Children.Add(logo);
        this.VerticalOptions = LayoutOptions.Start;
    }
}
