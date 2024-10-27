using WordMate.Core.Models;
using WordMate.Core.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace WordMate.Views.Components;
public class CategoryGrid : ContentView
{
    private readonly CategoryService _categoryService;
    private readonly WordService _wordService;
    private Frame _categoryFrame;
    private CollectionView _listView;
    private List<Category> _categories;

    public CategoryGrid(CategoryService categoryService, WordService wordService)
    {
        _categoryService = categoryService;
        _wordService = wordService;

        BuildList();
        Content = _listView;

        LoadCategories();
    }

    private void BuildList()
    {
        _categories = new List<Category>();

        _listView = new CollectionView
        {
            ItemTemplate = new DataTemplate(() =>
            {

                var nameLabel = new Label
                {
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");
                var numberLabel = new Label
                {
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                numberLabel.SetBinding(Label.TextProperty, "WordsCount");

                var spaceLabel = new Label { Text = ": " };

                var stack = new HorizontalStackLayout
                {
                    Children = { nameLabel, spaceLabel, numberLabel },
                    HorizontalOptions = LayoutOptions.Center,
                };

                var frame = new Frame
                {
                    Content = stack,
                    BorderColor = Color.FromHex("ffbd59"),
                    Padding = new Thickness( 30, 10),
                    HorizontalOptions = LayoutOptions.Fill
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    var tappedFrame = (Frame)s;
                    var bindingContext = tappedFrame.BindingContext;
                    var category = (Category)bindingContext;
                    await OnCategoryTapped(category.Id);
                };
                frame.GestureRecognizers.Add(tapGestureRecognizer);

                return frame;
            }),
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal),
            BackgroundColor = Colors.Transparent
        };
    }

    public async void Refresh()
    {
        _categoryService.UpdateCountForCategories();

        var newCategories = await _categoryService.GetAllCategoriesAsync();
        SetListItemSource(newCategories);
    }

    public void SetListItemSource(List<Category> newSource)
    {
        _listView.ItemsSource = newSource;
    }

    private async void LoadCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        foreach (var category in categories)
        {
            _categories.Add(category);
        }
    }

    private async Task OnCategoryTapped(int categoryId)
    {
        await Navigation.PushAsync(new WordListPage(_wordService, categoryId));
    }
}
