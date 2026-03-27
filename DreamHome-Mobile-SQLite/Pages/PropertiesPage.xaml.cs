using DreamHome_Mobile_SQLite.Contexts;
using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Collections.ObjectModel;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class PropertiesPage : ContentPage
{
    /// <summary>
    /// Display list of properties for the branch given by the context
    /// </summary>
    
    private readonly IBranchContext _context;
    private readonly IDreamHomeService _dreamHomeService;
    public ObservableCollection<PropertyForRent> PropertyList { get; } = new();

    public PropertiesPage(IBranchContext context, IDreamHomeService dreamHomeService)
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
            Title = $"Properties at branch {b.BranchNo}";
            await LoadPropertiesAsync(b.BranchNo);
        }
    }


    /// <summary>
    /// Load properties for given branch
    /// </summary>
    /// <returns></returns>
    private async Task LoadPropertiesAsync(string branchNo)
    {
        try
        {
            var properties = await _dreamHomeService.GetPropertiesForBranch(branchNo);
            PropertyList.Clear();

            int index = 0;
            foreach (var property in properties)
            {
                property.IsEven = (index % 2 == 0);
                PropertyList.Add(property);
                index++;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error loading properties", ex.Message, "OK");
        }
    }
}