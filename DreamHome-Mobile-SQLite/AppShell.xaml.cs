using DreamHome_Mobile_SQLite.Contexts;
using DreamHome_Mobile_SQLite.Pages;

namespace DreamHome_Mobile_SQLite
{
    public partial class AppShell : Shell
    {
        private readonly IBranchContext _context;

        public AppShell(IBranchContext context)
        {
            InitializeComponent();
            _context = context;

            BindingContext = _context;

            Navigating += OnShellNavigating;    // safety guard

            Routing.RegisterRoute(nameof(HighEarnersPage), typeof(HighEarnersPage));
            Routing.RegisterRoute(nameof(PropertyViewingsPage), typeof(PropertyViewingsPage));
            Routing.RegisterRoute(nameof(ClientViewingsPage), typeof(ClientViewingsPage));
            Routing.RegisterRoute(nameof(PropertyStaffCountPage), typeof(PropertyStaffCountPage));
            Routing.RegisterRoute(nameof(StaffCityPage), typeof(StaffCityPage));
            Routing.RegisterRoute(nameof(AddStaffDetailsPage), typeof(AddStaffDetailsPage));
        }


        private void OnShellNavigating(object? sender, ShellNavigatingEventArgs e)
        {
            if (_context.HasBranch) return;

            var target = e.Target?.Location?.OriginalString ?? string.Empty;

            // Block navigating to Properties/Staff when no branch yet
            if (target.Contains("PropertiesTab", StringComparison.OrdinalIgnoreCase) ||
                target.Contains("PropertiesPage", StringComparison.OrdinalIgnoreCase) ||
                target.Contains("StaffTab", StringComparison.OrdinalIgnoreCase) ||
                target.Contains("StaffPage", StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel();
                MainThread.BeginInvokeOnMainThread(async () =>
                    await DisplayAlert("Select a Branch",
                                       "Please select a branch first.",
                                       "OK"));
            }
        }
    }
}
