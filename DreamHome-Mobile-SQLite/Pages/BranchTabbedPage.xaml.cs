using DreamHome_Mobile_SQLite.Models;


namespace DreamHome_Mobile_SQLite.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SelectedBranch), "Branch")]
    public partial class BranchTabbedPage : TabbedPage
    {
        public Branch SelectedBranch { get; set; }

        public BranchTabbedPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (SelectedBranch != null)
            {
                // You now have the full Branch object
                Title = $"Branch: {SelectedBranch.BranchNo}";
            }
        }
    }
}