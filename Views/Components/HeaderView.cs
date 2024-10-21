using Microsoft.Maui.Controls;

namespace WordMate.Views.Components;
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
            await Navigation.PopToRootAsync();
        };
        logo.GestureRecognizers.Add(tapGestureRecognizer);

        Children.Add(logo);
        VerticalOptions = LayoutOptions.Start;
    }
}
