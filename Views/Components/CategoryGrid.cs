using WordMate.Core.Models;
using WordMate.Core.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
//using Microsoft.Maui.Controls.Compatibility;
using Grid = Microsoft.Maui.Controls.Grid;

namespace WordMate.Views.Components;
public class CategoryGrid : ContentView
{
    private readonly CategoryService _categoryService;
    private readonly WordService _wordService;
    private Frame _categoryFrame;
    private Grid _grid;
    //private readonly Dictionary<int, Label> _categoryLabels = new Dictionary<int, Label>();
    private readonly ObservableCollection<Category> _categories = new ObservableCollection<Category>();

    public CategoryGrid(CategoryService categoryService, WordService wordService)
    {
        _categoryService = categoryService;
        _wordService = wordService;


        var listView = new ListView
        {
            ItemsSource = _categories,
            //ItemsSource = new ObservableCollection<Category>(),
            ItemTemplate = new DataTemplate(() =>
            {
                _grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition()
                    },
                };

                /*var number1Label = new Label();
                number1Label.SetBinding(Label.TextProperty, "Name");
                _grid.Add(number1Label, 0, 0);*/

                return new ViewCell { View = _grid };
            })
        };

        Content = listView;


        LoadCategories();
    }

    public void Refresh()
    {
        UpdateCategoryLabels();
    }

    private async Task LoadCategories()
    {
        //List<Category> categories = await _categoryService.GetAllCategoriesAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        foreach (var category in categories)
        {
            _categories.Add(category);
            /*if (!_categoryLabels.ContainsKey(categories[i].Id))
            {
                AddCategoryToGrid(categories[i], i);
            }*/
        }
    }

    private void AddCategoryToGrid(Category category, int columnIndex)
    {
        var categoryLbl = CreateCategoryLabel();
        CreateCategoryFrame(categoryLbl, category);

        _categoryLabels[category.Id] = categoryLbl;

        _grid.Children.Add(_categoryFrame);
        Grid.SetColumn(_categoryFrame, columnIndex);
    }

    private Label CreateCategoryLabel()
    {
        var label = new Label
        {
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
        label.SetBinding(Label.TextProperty, "Name" + "WordsCount");
        return label;
    }

    private void CreateCategoryFrame(Label categoryLbl, Category category)
    {
        _categoryFrame = new Frame
        {
            BorderColor = Color.FromHex("ffbd59"),
            CornerRadius = 0,
            VerticalOptions = LayoutOptions.Center,
            Content = new StackLayout
            {
                Children = { categoryLbl }
            }
        };

        var tapGR = new TapGestureRecognizer();
        tapGR.Tapped += async (s, e) => await OnCategoryTapped(category.Id);

        _categoryFrame.GestureRecognizers.Add(tapGR);
    }

    private async Task OnCategoryTapped(int categoryId)
    {
        await Navigation.PushAsync(new WordListPage(_wordService, categoryId));
    }

    private async void UpdateCategoryLabels()
    {
        await _categoryService.UpdateCountForCategories();
        List<Category> categories = await _categoryService.GetAllCategoriesAsync();

        for (int i = 0; i <= 2; i++)
        {
            var categoryLbl = CreateCategoryLabel();
            _categoryFrame.Content = new StackLayout
            {
                Children = { categoryLbl }
            };

            _categoryLabels[categories[i].Id] = categoryLbl;

            _grid.Children.RemoveAt(i);
            _grid.Children.Add(_categoryFrame);
            Grid.SetColumn(_categoryFrame, i);
        }
    }
}
