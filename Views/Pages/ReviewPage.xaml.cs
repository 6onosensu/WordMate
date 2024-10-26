using WordMate.Core.Models;
using WordMate.Core.Services;

namespace WordMate.Views.Pages;

public partial class ReviewPage : ContentPage
{
	private List<Word> _wordList;
	private WordService _wordService;

	public ReviewPage(List<Word> wordList, WordService wordService)
	{
		_wordList = wordList;
		_wordService = wordService;
		Content = new Label
		{
			Text = "Reviewing"
		};
	}
}