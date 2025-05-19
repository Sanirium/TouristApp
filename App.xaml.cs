using TouristApp.Data;

namespace TouristApp
{
    public partial class App : Application
    {
        public App(DatabaseService db)
        {
            InitializeComponent();

            db.InitializeAsync().Wait();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}