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

        var tapGR = new TapGestureRecognizer();
        tapGR.Tapped += TapGRTapped;

        logo.GestureRecognizers.Add(tapGR);

        Children.Add(logo);
        VerticalOptions = LayoutOptions.Start;
    }

    private async void TapGRTapped(object? sender, TappedEventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
