using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class ClientViewingsPage : ContentPage
{
    /// <summary>
    /// Display list of client viewings
    /// </summary>
    
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<ClientViewing> ClientViewingsList { get; } = new();

    public ClientViewingsPage(IDreamHomeService dreamHomeService)
	{
		InitializeComponent();
        _dreamHomeService = dreamHomeService;
        BindingContext = this;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadClientViewingsAsync();
    }


    /// <summary>
    /// Load all client viewings
    /// </summary>
    /// <returns></returns>
    private async Task LoadClientViewingsAsync()
    {
        try
        {
            var viewings = await _dreamHomeService.GetClientViewings();
            ClientViewingsList.Clear();

            int index = 0;
            foreach (var viewing in viewings)
            {
                viewing.IsEven = (index % 2 == 0);
                ClientViewingsList.Add(viewing);
                index++;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading viewings", ex.Message, "OK");
        }
    }

}