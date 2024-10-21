using WordMate.Data;
using WordMate.Views.Components;
using WordMate.Models;
using WordMate.Services;

namespace WordMate.Views;
public partial class LearningPage : ContentPage
{
	private WordStudyView _cards;
	private WordDB _wordDB;
    private List<Word> _words;
    private List<Word> _wordsToLearn;
    private Word _currentWord;
	private WordService _wordService;

    public LearningPage(WordDB wordDB)
	{
		_wordDB = wordDB;
        GetWords();

        _cards = new WordStudyView(_currentWord, _wordDB);
        Content = _cards;
    }
    private async Task GetWords()
    {
        List<Word> _words = await _wordDB.WordManager.GetWordsByCategory(1);
    }

    private void Get10Words(List<Word> wordsToLearn) 
    { 
        for (int i = 0; i < 10; i++)
        {
            _wordsToLearn.Add(wordsToLearn[i]);
        }
        return;
    }
}