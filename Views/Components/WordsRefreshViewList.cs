using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordMate.Data;
using WordMate.Views.Pages;
using System.Windows.Input;
using WordMate.Core.Models;

namespace WordMate.Views.Components
{
    public class WordsRefreshViewList : RefreshView
    {
        ListView _listView;
        Label _wordLbl, _definitionLbl;
        WordDB _wordDB;

        
        public WordsRefreshViewList(WordDB wordDB)
        {
            _wordDB = wordDB;

            _listView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    _wordLbl = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        FontSize = 18
                    };
                    _wordLbl.SetBinding(Label.TextProperty, "Text");

                    _definitionLbl = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        FontSize = 18,
                        IsVisible = false
                    };
                    _definitionLbl.SetBinding(Label.TextProperty, "Definition");

                    var editBtn = new Button
                    {
                        Text = "Edit",
                        FontSize = 14,
                        HeightRequest = 38,
                        TextColor = Colors.Black,
                        BackgroundColor = Color.FromHex("ffea94"),
                        HorizontalOptions = LayoutOptions.End
                    };
                    editBtn.SetBinding(Button.BindingContextProperty, ".");
                    editBtn.Clicked += OnEditBtnClicked;

                    var grid = new Grid
                    {
                        ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                    };

                    grid.Add(_wordLbl, 0, 0);
                    grid.Add(_definitionLbl, 0, 0);
                    grid.Add(editBtn, 1, 0);

                    var viewCell = new ViewCell { View = grid };
                    viewCell.Tapped += OnViewCellTapped;

                    return viewCell;
                })
            };

            ICommand refreshCommand = new Command(() =>
            {
                this.IsRefreshing = false;
            });
            this.Command = refreshCommand;


            Content = _listView;
        }

        private async void OnViewCellTapped(object? sender, EventArgs e)
        {
            _wordLbl.IsVisible = !_wordLbl.IsVisible;
            _definitionLbl.IsVisible = !_definitionLbl.IsVisible;
        }
        private async void OnEditBtnClicked(object? sender, EventArgs e)
        {
            if (sender is Button editBtn && editBtn.BindingContext is Word selectedWord)
            {
                await Navigation.PushAsync(new EditWordPage(_wordDB, selectedWord));
            }
        }
    }
}
