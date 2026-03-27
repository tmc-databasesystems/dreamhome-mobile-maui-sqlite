using DreamHome_Mobile_SQLite.Contexts;

namespace DreamHome_Mobile_SQLite
{
    public partial class App : Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();
            MainPage = shell;
        }

    }
}