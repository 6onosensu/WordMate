using WordMate.Data;

namespace WordMate
{
    public partial class App : Application
    {
        static WordMateDatabase database;
        public static WordMateDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WordMate.db3");
                    database = new WordMateDatabase(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
