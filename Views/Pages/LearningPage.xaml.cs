using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views;
public partial class LearningPage : ContentPage
{
    private List<Word> _wordsList;
    private WordService _wordService;
    public LearningPage(List<Word> wordList, WordService wordService)
    {
        _wordService = wordService;
        _wordsList = wordList;

        Content = new Label
        {
            Text = "Learning"
        };
    }

}