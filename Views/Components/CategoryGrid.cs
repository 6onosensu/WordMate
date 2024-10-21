using Microsoft.Maui.Controls;
using WordMate.Models;
using WordMate.Data;
using System.Collections.Generic;

namespace WordMate.Views.Components;
public class CategoryGrid : Grid
{
    private WordDB _wordDB;
    private Dictionary<int, Label> _categoryLabels;
    public CategoryGrid(WordDB wordDB)
    {
        _wordDB = wordDB;
        _categoryLabels = new Dictionary<int, Label>();

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
        List<Category> categories = await _wordDB.CategoryManager.GetCategories();

        for (int i = 0; i < 3; i++)
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
        var categoryLbl = new Label
        {
            Text = $"{category.Name} ({category.WordsCount})",
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        _categoryLabels[category.Id] = categoryLbl;

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
        tapGR.Tapped += (s, e) =>
        {
            Navigation.PushAsync(new WordListPage(_wordDB, category.Id));
        };
        categoryFrame.GestureRecognizers.Add(tapGR);

        Children.Add(categoryFrame);
        Grid.SetColumn(categoryFrame, columnIndex);
    }
    private void UpdateCategoryLabel(Category category)
    {
        if (_categoryLabels.TryGetValue(category.Id, out var categoryLabel))
        {
            categoryLabel.Text = $"{category.Name} ({category.WordsCount})";
        }
    }
}
