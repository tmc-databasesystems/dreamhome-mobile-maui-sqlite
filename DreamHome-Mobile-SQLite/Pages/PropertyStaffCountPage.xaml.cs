using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class PropertyStaffCountPage : ContentPage
{
    /// <summary>
    /// Display count of staff at each branch
    /// </summary>
    
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<BranchStaffCount> BranchStaffList { get; } = new();

    public PropertyStaffCountPage(IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadBranchStaffCountsAsync();
    }


    /// <summary>
    /// Load all branch/staff counts
    /// </summary>
    /// <returns></returns>
    private async Task LoadBranchStaffCountsAsync()
    {
        try
        {
            var counts = await _dreamHomeService.GetBranchStaffCount();
            BranchStaffList.Clear();

            int index = 0;
            foreach (var count in counts)
            {
                count.IsEven = (index % 2 == 0);
                BranchStaffList.Add(count);
                index++;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading branch/staff counts", ex.Message, "OK");
        }
    }

}