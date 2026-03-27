using DreamHome_Mobile_SQLite.Contexts;

namespace DreamHome_Mobile_SQLite
{
    public partial class App : Application
    {
 
        private readonly AppShell _appShell;

        public App(AppShell appShell)
        {
            InitializeComponent();
            _appShell = appShell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_appShell);
        }

    }
}