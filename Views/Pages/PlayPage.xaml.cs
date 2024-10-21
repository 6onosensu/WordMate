using WordMate.Data;

namespace WordMate.Views;

public partial class PlayPage : ContentPage
{
	private WordDB _wordDB;

	public PlayPage(WordDB wordDB)
	{
		_wordDB = wordDB;
		InitializeComponent();
	}
}