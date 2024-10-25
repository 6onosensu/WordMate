using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views.Components;
public class CategoryGrid : Grid
{
    private readonly CategoryService _categoryService;
    private readonly WordService _wordService;
    private readonly Dictionary<int, Label> _categoryLabels = new Dictionary<int, Label>();

    public CategoryGrid(CategoryService categoryService, WordService wordService)
    {
        _categoryService = categoryService;
        _wordService = wordService;

        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition()
        };

        LoadCategories();
    }

    public async void Refresh()
    {
        await LoadCategories();
    }

    private async Task LoadCategories()
    {
        List<Category> categories = await _categoryService.GetAllCategoriesAsync();

        for (int i = 0; i < categories.Count; i++)
        {
            if (_categoryLabels.ContainsKey(categories[i].Id))
            {
                UpdateCategoryLabel(categories[i]);
            }
            else
            {
                AddCategoryToGrid(categories[i], i);
            }
        }
    }

    private void AddCategoryToGrid(Category category, int columnIndex)
    {
        var categoryLbl = CreateCategoryLabel(category);
        var categoryFrame = CreateCategoryFrame(categoryLbl, category);

        _categoryLabels[category.Id] = categoryLbl;

        Children.Add(categoryFrame);
        Grid.SetColumn(categoryFrame, columnIndex);
    }

    private Label CreateCategoryLabel(Category category)
    {
        return new Label
        {
            Text = $"{category.Name} ({category.WordsCount})",
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }

    private Frame CreateCategoryFrame(Label categoryLbl, Category category)
    {
        var categoryFrame = new Frame
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

        categoryFrame.GestureRecognizers.Add(tapGR);
        return categoryFrame;
    }

    private async Task OnCategoryTapped(int categoryId)
    {
        await Navigation.PushAsync(new WordListPage(_wordService, categoryId));
    }

    private void UpdateCategoryLabel(Category category)
    {
        _categoryService.UpdateCountForCategory(category.Id);

        if (_categoryLabels.ContainsKey(category.Id))
        {
            var categoryLabel = _categoryLabels[category.Id];
            categoryLabel.Text = $"{category.Name} ({category.WordsCount})";
        }
    }
}
