using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class PropertyViewingsPage : ContentPage
{
    /// <summary>
    /// Display details of viewings between a given start and end date
    /// </summary>
    
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<Viewing> PropertyViewingList { get; } = new();

    public PropertyViewingsPage(IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    /// <summary>
    /// Display details of viewings between the start/end date the user has specified
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnRunQueryClicked(object sender, EventArgs e)
    {
        try
        {
            var startDate = StartDatePicker.Date.Date;
            var endDate = EndDatePicker.Date.Date;

            if (startDate > endDate)
            {
                await DisplayAlert("Invalid Dates",
                    "The start date must be earlier than or equal to the end date.",
                    "OK");
                return;
            }

            var viewings = await _dreamHomeService.GetPropertyViewings(startDate, endDate);

            PropertyViewingList.Clear();

            int index = 0;
            foreach (var viewing in viewings)
            {
                viewing.IsEven = (index % 2 == 0);
                PropertyViewingList.Add(viewing);
                index++;
            }
            ViewingCollectionView.IsVisible = true;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading viewings", ex.Message, "OK");
        }

    }
}