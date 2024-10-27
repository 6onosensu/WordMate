using WordMate.Core.Models;
using WordMate.Core.Services;
using WordMate.Views.Components;

namespace WordMate.Views;
public partial class LearningPage : ContentPage
{
    private List<Word> _wordsList;
    private WordService _wordService;

    public LearningPage(List<Word> wordList, WordService wordService)
    {
        _wordService = wordService;
        _wordsList = wordList;

        var headerView = new HeaderView();

        var learningView = new WordStudyView(wordService, _wordsList);


        Content = new StackLayout
        {
            headerView, learningView
        };
    }
}