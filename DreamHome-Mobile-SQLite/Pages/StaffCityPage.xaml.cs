using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class StaffCityPage : ContentPage
{
    /// <summary>
    /// Display list of staff in a branches at the given city
    /// </summary>
    private readonly IDreamHomeService _dreamHomeService;

    public ObservableCollection<CityStaff> StaffCityList { get; } = new();

    public StaffCityPage(IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    /// <summary>
    /// Get staff for given city
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnRunQueryClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(CityEntry.Text) )
            {
                await DisplayAlert("Invalid Input", "Please enter a city.", "OK");
                return;
            }

            var staff = await _dreamHomeService.GetCityStaff(CityEntry.Text);

            StaffCityList.Clear();

            int index = 0;
            foreach (var member in staff)
            {
                member.IsEven = (index % 2 == 0);
                StaffCityList.Add(member);
                index++;
            }
            StaffCollectionView.IsVisible = true;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading staff", ex.Message, "OK");
        }

    }
}