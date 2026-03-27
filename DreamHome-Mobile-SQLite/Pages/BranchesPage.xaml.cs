using DreamHome_Mobile_SQLite.Contexts;
using DreamHome_Mobile_SQLite.Helpers;
using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class BranchesPage : ContentPage
{
    /// <summary>
    /// Display list of branches and allow one to be selected
    /// </summary>
    private readonly IBranchContext _context;
    private readonly IDreamHomeService _dreamHomeService;

    public ObservableCollection<Branch> BranchList { get; } = new();

    public BranchesPage(IBranchContext context, IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
		_context = context;
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Title = "Branches";
        await LoadBranchesAsync();
    }


    /// <summary>
    /// Load all branches
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Handle branch selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnBranchSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Branch selectedBranch)
        {
            ((CollectionView)sender).SelectedItem = null;

            _context.Current = selectedBranch;

            // Auto-navigate to Properties:
            await Shell.Current.GoToAsync("//BranchTabs/PropertiesTab/PropertiesPage");
        }
    }
}