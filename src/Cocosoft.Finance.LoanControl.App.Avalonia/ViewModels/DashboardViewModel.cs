using System;
using CommunityToolkit.Mvvm.Input;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// This class represents the view model for the dashboard view in the loan control application.
/// It provides properties and commands to manage and display financial data related to loans,
/// such as collected capital, interest, pending due amounts, and total loans granted.
/// The view model also includes navigation actions for creating new loans and searching existing loans.
/// </summary>
/// <param name="serviceProvider">The service provider used for dependency injection and service resolution.</param>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class DashboardViewModel : ViewModelBase
{
    /// <summary>
    /// Gets the sweep angle in degrees representing the capital portion of the pie chart.
    /// </summary>
    public double CapitalAngle => this.TotalCollected > 0
        ? (double)(this.CollectedCapital / this.TotalCollected) * 360.0
        : 180.0;

    /// <summary>
    /// Gets or sets the total collected capital amount.
    /// </summary>
    public decimal CollectedCapital { get; set; } = 52_500_000m;

    /// <summary>
    /// Gets the collected capital formatted as currency.
    /// </summary>
    public string CollectedCapitalFormatted => this.CollectedCapital.ToString("C0");

    /// <summary>
    /// Gets or sets the total collected interest amount.
    /// </summary>
    public decimal CollectedInterest { get; set; } = 35_000_000m;

    /// <summary>
    /// Gets the collected interest formatted as currency.
    /// </summary>
    public string CollectedInterestFormatted => this.CollectedInterest.ToString("C0");

    /// <summary>
    /// Gets the sweep angle in degrees representing the interest portion of the pie chart.
    /// </summary>
    public double InterestAngle => 360.0 - this.CapitalAngle;

    /// <summary>
    /// Gets or sets the action to navigate to the new loan view.
    /// </summary>
    public Action? NavigateToNewLoanAction { get; set; }

    /// <summary>
    /// Gets or sets the action to navigate to the loan search view.
    /// </summary>
    public Action? NavigateToSearchLoanAction { get; set; }

    /// <summary>
    /// Gets or sets the pending due amount.
    /// </summary>
    public decimal PendingDueAmount { get; set; } = 4_800_000m;

    /// <summary>
    /// Gets the pending due amount as a percentage of the total due amount.
    /// </summary>
    public double PendingDuePercentage => this.TotalDueAmount > 0
        ? (double)(this.PendingDueAmount / this.TotalDueAmount) * 100.0
        : 0;

    /// <summary>
    /// Gets the pending due percentage formatted with one decimal place.
    /// </summary>
    public string PendingDuePercentageFormatted => $"{this.PendingDuePercentage:F1}%";

    /// <summary>
    /// Gets or sets the total amount collected from all loans.
    /// </summary>
    public decimal TotalCollected { get; set; } = 87_500_000m;

    /// <summary>
    /// Gets the total collected amount formatted as currency.
    /// </summary>
    public string TotalCollectedFormatted => this.TotalCollected.ToString("C0");

    /// <summary>
    /// Gets or sets the total due amount.
    /// </summary>
    public decimal TotalDueAmount { get; set; } = 12_000_000m;

    /// <summary>
    /// Gets or sets the total amount of loans granted.
    /// </summary>
    public decimal TotalLoansGranted { get; set; } = 150_000_000m;

    /// <summary>
    /// Gets the total loans granted formatted as currency.
    /// </summary>
    public string TotalLoansGrantedFormatted => this.TotalLoansGranted.ToString("C0");

    [RelayCommand]
    private void NewLoan()
    {
        this.NavigateToNewLoanAction?.Invoke();
    }

    [RelayCommand]
    private void RegisterPayment()
    {
        // TODO: Navigate to payment registration
    }

    [RelayCommand]
    private void SearchLoan()
    {
        this.NavigateToSearchLoanAction?.Invoke();
    }
}
