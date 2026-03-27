
using DreamHome_Mobile_SQLite.Services;

namespace DreamHome_Mobile_SQLite.Pages
{
    public partial class QueriesPage : ContentPage
    {
        /// <summary>
        /// Display list of queries and navigate to the appropriate page based on user selection
        /// </summary>
        
        private readonly IDreamHomeService _dreamHomeService;

        public QueriesPage(IDreamHomeService dreamHomeService)
        {
            InitializeComponent();
            _dreamHomeService = dreamHomeService;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


        private async void HighEarnersClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(HighEarnersPage));
        }


        private async void PropertyViewingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PropertyViewingsPage));
        }


        private async void ClientViewingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ClientViewingsPage));
        }


        private async void BranchStaffClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PropertyStaffCountPage));
        }


        private async void StaffCityClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StaffCityPage));
        }
    }
}