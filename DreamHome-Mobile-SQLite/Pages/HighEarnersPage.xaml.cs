using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class HighEarnersPage : ContentPage
{
    /// <summary>
    /// Display list of staff with a salary above a given threshold
    /// </summary>
    
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<Staff> HighEarnersList { get; } = new();

    public HighEarnersPage(IDreamHomeService dreamHomeService)
    {
        InitializeComponent();
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    /// <summary>
    /// Get high earners for given threshold
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnRunQueryClicked(object sender, EventArgs e)
    {
        try
        {
            if (!decimal.TryParse(SalaryEntry.Text, out var threshold))
            {
                await DisplayAlert("Invalid Input", "Please enter a valid salary value.", "OK");
                return;
            }

            var staff = await _dreamHomeService.GetHighEarners(threshold);

            HighEarnersList.Clear();

            int index = 0;
            foreach (var member in staff)
            {
                member.IsEven = (index % 2 == 0);
                HighEarnersList.Add(member);
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