using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// Main view model that manages application navigation, sidebar state,
/// and coordinates the dashboard and loans view models.
/// </summary>
/// <seealso cref="ViewModelBase" />
public partial class MainViewModel : ViewModelBase
{
    private const double SidebarCollapsedWidth = 60;

    private const double SidebarExpandedWidth = 240;

    /// <summary>
    /// Gets or sets a value indicating whether the sidebar toggle button is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool CanToggleSidebar { get; set; } = true;

    /// <summary>
    /// Gets or sets the subtitle displayed in the current page header.
    /// </summary>
    [ObservableProperty]
    public partial string CurrentPageSubtitle { get; set; }

    /// <summary>
    /// Gets or sets the title displayed in the current page header.
    /// </summary>
    [ObservableProperty]
    public partial string CurrentPageTitle { get; set; }

    /// <summary>
    /// Gets or sets the view model currently displayed in the content area.
    /// </summary>
    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; set; }

    /// <summary>
    /// Gets the dashboard view model.
    /// </summary>
    public DashboardViewModel Dashboard { get; }

    /// <summary>
    /// Gets a value indicating whether the dashboard view is currently active.
    /// </summary>
    public bool IsDashboardActive => this.CurrentView == this.Dashboard;

    /// <summary>
    /// Gets a value indicating whether the loans view is currently active.
    /// </summary>
    public bool IsLoansActive => this.CurrentView == this.Loans;

    /// <summary>
    /// Gets or sets a value indicating whether the sidebar is expanded.
    /// </summary>
    [ObservableProperty]
    public partial bool IsSidebarExpanded { get; set; } = true;

    /// <summary>
    /// Gets the loans view model.
    /// </summary>
    public LoansViewModel Loans { get; }

    /// <summary>
    /// Gets the current sidebar width based on the expanded or collapsed state.
    /// </summary>
    public double SidebarWidth => this.IsSidebarExpanded
        ? SidebarExpandedWidth
        : SidebarCollapsedWidth;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class
    /// using default view models (design-time support).
    /// </summary>
    // Parameterless constructor for design-time support
    public MainViewModel() : this(new DashboardViewModel(), new LoansViewModel())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="dashboard">The dashboard view model.</param>
    /// <param name="loans">The loans view model.</param>
    [ActivatorUtilitiesConstructor]
    public MainViewModel(
        DashboardViewModel dashboard,
        LoansViewModel loans)
    {
        this.Dashboard = dashboard;
        this.Loans = loans;
        this.CurrentView = this.Dashboard;
        this.CurrentPageTitle = "Dashboard";
        this.CurrentPageSubtitle = "Resumen general del sistema";

        this.Dashboard.NavigateToNewLoanAction = () =>
        {
            this.NavigateToLoans();
            this.Loans.IsCreatingLoan = true;
        };

        this.Dashboard.NavigateToSearchLoanAction = () =>
        {
            this.NavigateToLoans();
        };
    }

    [RelayCommand]
    private void NavigateToDashboard()
    {
        this.CurrentView = this.Dashboard;
        this.CurrentPageTitle = "Dashboard";
        this.CurrentPageSubtitle = "Resumen general del sistema";

        // Reset the LoansViewModel state when navigating back to the dashboard
        this.Loans.SearchText = string.Empty;
        this.Loans.SearchResults.Clear();
        this.Loans.SelectedSearchResult = null;
        this.Loans.SelectedLoanDetail = null;
        this.Loans.IsCreatingLoan = false;
        this.Loans.IsShowingDetail = false;
        this.Loans.IsShowingResults = false;
    }

    [RelayCommand]
    private void NavigateToLoans()
    {
        this.CurrentView = this.Loans;
        this.CurrentPageTitle = "Préstamos";
        this.CurrentPageSubtitle = "Gestión de préstamos";
    }

    partial void OnCurrentViewChanged(ViewModelBase value)
    {
        this.OnPropertyChanged(nameof(this.IsDashboardActive));
        this.OnPropertyChanged(nameof(this.IsLoansActive));
    }

    partial void OnIsSidebarExpandedChanged(bool value)
    {
        this.OnPropertyChanged(nameof(this.SidebarWidth));
    }

    [RelayCommand]
    private void ToggleSidebar()
    {
        this.IsSidebarExpanded = !this.IsSidebarExpanded;
    }
}
