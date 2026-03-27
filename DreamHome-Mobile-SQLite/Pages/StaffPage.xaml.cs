using DreamHome_Mobile_SQLite.Contexts;
using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class StaffPage : ContentPage
{
    /// <summary>
    /// Display list of staff at the branch indicated by the context
    /// </summary>
    
    private readonly IBranchContext _context;
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<Staff> StaffList { get; } = new();

    public StaffPage(IBranchContext context, IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
		_context = context;
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_context.Current is Branch b)
        {
            Title = $"Staff at branch {b.BranchNo}";
            await LoadStaffAsync(b.BranchNo);

        }
    }


    /// <summary>
    /// Load staff for given branch
    /// </summary>
    /// <returns></returns>
    private async Task LoadStaffAsync(string branchNo)
    {
        try
        {
            var staff = await _dreamHomeService.GetStaffForBranch(branchNo);
            StaffList.Clear();

            int index = 0;
            foreach (var member in staff)
            {
                member.IsEven = (index % 2 == 0);
                StaffList.Add(member);
                index++;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading staff", ex.Message, "OK");
        }
    }

    /// <summary>
    /// Add staff button clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnAddStaffClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddStaffDetailsPage));
    }


    /// <summary>
    /// Staff row has been clicked to update record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnStaffSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedStaff = e.CurrentSelection.FirstOrDefault() as Staff;
        if (selectedStaff == null)
            return;

        await Shell.Current.GoToAsync(
          nameof(AddStaffDetailsPage),
          new ShellNavigationQueryParameters
          {
              ["SelectedStaff"] = selectedStaff // pass staff to be updated through the next page
          });

        StaffCollectionView.SelectedItem = null;
    }
}