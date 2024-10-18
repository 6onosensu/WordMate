using Microsoft.Maui.Controls;
using WordMate.Models;
using WordMate.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordMate.Views;
public class CategoryGrid : Grid
{
    private WordDB _wordDB;
    public CategoryGrid(WordDB wordDB)
    {
        _wordDB = wordDB;

        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition()
        };

        LoadCategories();
    }
    private async void LoadCategories()
    {
        try
        {
            List<Category> categories = await _wordDB.CategoryManager.GetCategories();
            Console.WriteLine($"Categories loaded: {categories.Count}");
            for (int i = 0; i < categories.Count && i < 3; i++)
            {
                AddCategoryToGrid(categories[i], i);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading categories: {ex.Message}");
        }
    }
    private void AddCategoryToGrid(Category category, int columnIndex)
    {
        var categoryFrame = CreateCategoryLbl(category.Name, category.WordsCount);
        Children.Add(categoryFrame);
        Grid.SetColumn(categoryFrame, columnIndex);
    }
    private Frame CreateCategoryLbl(string categoryName, int wordCount)
    {
        return new Frame
        {
            BorderColor = Color.FromHex("ffbd59"),
            CornerRadius = 0,
            VerticalOptions = LayoutOptions.Center,
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = $"{categoryName} ({wordCount})",
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            }
        };
    }
}
