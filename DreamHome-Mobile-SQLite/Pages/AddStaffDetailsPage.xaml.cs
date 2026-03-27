using DreamHome_Mobile_SQLite.Models;
using DreamHome_Mobile_SQLite.Services;
using System.Globalization;

namespace DreamHome_Mobile_SQLite.Pages;

public partial class AddStaffDetailsPage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Add/Update staff details
    /// </summary>
    
    private readonly IDreamHomeService _dreamHomeService;
    private Staff? _staff;
    private bool _isEditMode;

    public AddStaffDetailsPage(IDreamHomeService dreamHomeService)
    {
        InitializeComponent();
        _dreamHomeService = dreamHomeService;
    }


    /// <summary>
    /// Get all branches for branch picker
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var branches = await _dreamHomeService.GetBranches();
        BranchPicker.ItemsSource = branches.Select(b => b.BranchNo).ToList();
        if (_staff is not null)
        {
            BranchPicker.SelectedIndex = branches.FindIndex(b => b.BranchNo == _staff.BranchNo);
        }
    }


    /// <summary>
    /// Check if staff has been passed in (if so, staff record is being updated otherwise a staff record is being created)
    /// </summary>
    /// <param name="query"></param>
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("SelectedStaff", out var value) && value is Staff staff)
        {
            _staff = staff;
            await LoadStaffForEditAsync(staff);
        }
        else
        {
            _staff = null;
            PrepareForAddMode();
        }
    }


    /// <summary>
    /// New staff record is being added
    /// </summary>
    private void PrepareForAddMode()
    {
        _isEditMode = false;
        Title = "Add Staff";
        StaffNoEntry.IsReadOnly = false;
    }


    /// <summary>
    /// Staff record is being updated so get the staff record for update and display details
    /// </summary>
    /// <param name="staff">Staff record to be updated</param>
    /// <returns></returns>
    private async Task LoadStaffForEditAsync(Staff staff)
    {
        _isEditMode = true;
        _staff = staff;

        Title = "Update Staff";
        StaffNoEntry.IsReadOnly = true;

        if (_staff == null)
        {
            await DisplayAlert("Error", "Staff member not found.", "OK");
            return;
        }

        StaffNoEntry.Text = _staff.StaffNo;
        FNameEntry.Text = _staff.FName;
        LNameEntry.Text = _staff.LName;
        PositionPicker.SelectedItem = _staff.Position;
        DobEntry.Text = _staff.Dob.ToString("yyyy-MM-dd");
        SalaryEntry.Text = _staff.Salary.ToString();
    }


    /// <summary>
    /// Validate data entry fields and then add new member of staff to database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            var staffNo = StaffNoEntry.Text?.Trim();
            var fName = FNameEntry.Text?.Trim();
            var lName = LNameEntry.Text?.Trim();
            var position = PositionPicker.SelectedItem?.ToString();
            var dobText = DobEntry.Text?.Trim();
            var salaryText = SalaryEntry.Text?.Trim();
            var branchNo = BranchPicker.SelectedItem?.ToString();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(staffNo))
            {
                await DisplayAlert("Validation Error", "Please enter a staff number.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(fName))
            {
                await DisplayAlert("Validation Error", "Please enter a first name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(lName))
            {
                await DisplayAlert("Validation Error", "Please enter a last name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                await DisplayAlert("Validation Error", "Please select a position.", "OK");
                return;
            }

            if (!DateOnly.TryParseExact(
                    dobText,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dob))
            {
                await DisplayAlert("Validation Error", "Please enter Date of Birth in the format yyyy-MM-dd.", "OK");
                return;
            }

            if (dob >= DateOnly.FromDateTime(DateTime.Today))
            {
                await DisplayAlert("Validation Error", "Date of Birth must be in the past.", "OK");
                return;
            }

            if (!decimal.TryParse(salaryText, out var salary))
            {
                await DisplayAlert("Validation Error", "Please enter a valid salary.", "OK");
                return;
            }

            if (salary < 0)
            {
                await DisplayAlert("Validation Error", "Salary cannot be negative.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(branchNo))
            {
                await DisplayAlert("Validation Error", "Please select a branch number.", "OK");
                return;
            }

            // Create entity object here and save to database
            var staff = new Staff
            {
                StaffNo = staffNo,
                FName = fName,
                LName = lName,
                Position = position,
                Dob = dob.ToDateTime(TimeOnly.MinValue),
                Salary = salary,
                BranchNo = branchNo
            };

            await _dreamHomeService.AddOrUpdateStaffAsync(staff);


            await DisplayAlert("Success", "Staff member added/updated successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error add/update member of staff", ex.Message, "OK");
        }
    }
}