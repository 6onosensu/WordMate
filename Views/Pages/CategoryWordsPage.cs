using Microsoft.Maui.Controls;
using WordMate.Core.Models;
using WordMate.Core.Services;
using WordMate.Views.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordMate.Views.Pages;

public class CategoryWordsPage : ContentPage
{
    private readonly WordService _wordService;
    private readonly int _categoryId;
    private List<Word> _wordsList;

    public CategoryWordsPage(int categoryId, WordService wordService)
	{
        _categoryId = categoryId;
        _wordService = wordService;

        InitializePage();
    }

    private async void InitializePage()
    {
        var header = new HeaderView();
        var headerLayout = new StackLayout
        {
            Children = { header },
            HorizontalOptions = LayoutOptions.Center
        };

        await LoadWords();

        var wordsListView = CreateWordsList();

        var learnButton = new Button
        {
            Text = "Let's Do It!",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.End
        };
        learnButton.Clicked += OnButtonClicked;

        Content = new StackLayout
        {
            Children = { headerLayout, wordsListView, learnButton },
            Padding = new Thickness(10),
            VerticalOptions = LayoutOptions.FillAndExpand
        };
    }

    private async Task LoadWords()
    {
        _wordsList = await _wordService.GetWordsByCategoryAsync(_categoryId);
        if (_wordsList.Count > 10)
            _wordsList = _wordsList.GetRange(0, 10);
    }

    private ListView CreateWordsList()
    {
        var wordsListView = new ListView
        {
            ItemsSource = _wordsList,
            ItemTemplate = new DataTemplate(() =>
            {
                var wordLbl = new Label { IsVisible = true, FontSize = 18,};
                wordLbl.SetBinding(Label.TextProperty, "Text");

                var definitionLbl = new Label { IsVisible = false, FontSize = 14, };
                definitionLbl.SetBinding(Label.TextProperty, "Definition");

                var wordCell = new ViewCell
                {
                    View = new StackLayout
                    {
                        Children = { wordLbl, definitionLbl },
                        Padding = new Thickness(10),
                        BackgroundColor = Colors.White,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    }
                };

                wordCell.Tapped += async (sender, e) =>
                {
                    wordLbl.IsVisible = !wordLbl.IsVisible;
                    definitionLbl.IsVisible = !definitionLbl.IsVisible;
                };

                return wordCell;
            }),
            SeparatorVisibility = SeparatorVisibility.Default,
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        return wordsListView;
    }

    private async void  OnButtonClicked(object sender, EventArgs e)
    {
        if (_categoryId == 1)
        {
            await Navigation.PushAsync(new LearningPage(_wordsList, _wordService));
        }
        else if (_categoryId == 2)
        {

            await Navigation.PushAsync(new ReviewPage(_wordsList, _wordService));
        }
        else { return; }
    }
}