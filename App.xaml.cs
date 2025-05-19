using TouristApp.Data;

namespace TouristApp
{
    public partial class App : Application
    {
        readonly DatabaseService _db;

        public App(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            _ = _db.InitializeAsync();
        }
    }

}