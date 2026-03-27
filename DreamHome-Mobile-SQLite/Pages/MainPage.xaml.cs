using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Pages;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;


namespace DreamHome_Mobile_SQLite
{
    public partial class MainPage : ContentPage
    {
        private readonly IDreamHomeService _dreamHomeService;

        public ObservableCollection<Branch> BranchList { get; } = new();

        public MainPage(IDreamHomeService dreamHomeService)
        {
            InitializeComponent();
            _dreamHomeService = dreamHomeService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadBranchesAsync();
        } 


        private async Task LoadBranchesAsync() 
        {
            try
            {
                var branches = await _dreamHomeService.GetBranches();
                BranchList.Clear();

                int index = 0;
                foreach (var branch in branches)
                {
                    branch.IsEven = (index % 2 == 0);
                    BranchList.Add(branch);
                    index++;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error loading branches", ex.Message, "OK");
            }
        }


        private async void OnBranchSelected(Object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Branch selectedBranch)
            {
                ((CollectionView)sender).SelectedItem = null;

                //await Navigation.PushAsync(new BranchTabbedPage { SelectedBranch = selectedBranch });

                await Shell.Current.GoToAsync(nameof(BranchTabbedPage),
                     new Dictionary<string, object>
                     {
                        { "Branch", selectedBranch }
                     });
            }
        }

    }
}
