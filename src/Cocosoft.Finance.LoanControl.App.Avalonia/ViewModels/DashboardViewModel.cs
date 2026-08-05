using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cocosoft.Finance.LoanControl.Core.Services.V2;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// This class represents the view model for the dashboard view in the loan control application.
/// It provides properties and commands to manage and display financial data related to loans,
/// such as collected capital, interest, pending due amounts, and total loans granted.
/// The view model also includes navigation actions for creating new loans and searching existing loans.
/// </summary>
/// <param name="serviceProvider">The service provider used for dependency injection and service resolution.</param>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class DashboardViewModel(ILoanService loanService, ILogger<DashboardViewModel> logger) : ViewModelBase
{
    private readonly IDictionary<string, CancellationTokenSource?> _ctSources =
        new Dictionary<string, CancellationTokenSource?>
        {
            [nameof(DashboardViewModel.GetCapitalAngleAsync)] = null,
            [nameof(DashboardViewModel.GetPendingDuePercentageAsync)] = null,
            [nameof(DashboardViewModel.GetPendingDuePercentageFormattedAsync)] = null,
            [nameof(DashboardViewModel.GetTotalCollectedFormattedAsync)] = null,
            [nameof(DashboardViewModel.GetTotalCurrentDuePaymentAmountAsync)] = null,
            [nameof(DashboardViewModel.GetTotalLoansGrantedFormattedAsync)] = null
        };

    /// <summary>
    /// Gets the sweep angle in degrees representing the capital portion of the pie chart.
    /// </summary>
    public Task<double> CapitalAngle => this.GetCapitalAngleAsync();

    /// <summary>
    /// Gets or sets the action to navigate to the new loan view.
    /// </summary>
    public Action? NavigateToNewLoanAction { get; set; }

    /// <summary>
    /// Gets or sets the action to navigate to the loan search view.
    /// </summary>
    public Action? NavigateToSearchLoanAction { get; set; }

    /// <summary>
    /// Gets the pending due amount as a percentage of the total due amount.
    /// </summary>
    public Task<double> PendingDuePercentage => this.GetPendingDuePercentageAsync();

    /// <summary>
    /// Gets the pending due percentage formatted with one decimal place.
    /// </summary>
    public Task<string> PendingDuePercentageFormatted => this.GetPendingDuePercentageFormattedAsync();

    /// <summary>
    /// Gets the total collected amount formatted as currency.
    /// </summary>
    public Task<string> TotalCollectedFormatted => this.GetTotalCollectedFormattedAsync();

    /// <summary>
    /// Gets or sets the total due amount.
    /// </summary>
    public Task<decimal> TotalDueAmount => this.GetTotalCurrentDuePaymentAmountAsync();

    /// <summary>
    /// Gets the total loans granted formatted as currency.
    /// </summary>
    public Task<string> TotalLoansGrantedFormatted => this.GetTotalLoansGrantedFormattedAsync();

    private async Task<double> GetCapitalAngleAsync()
    {
        this._ctSources[nameof(this.GetCapitalAngleAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetCapitalAngleAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            var totalCollected = await loanService.GetTotalCollectedAsync(cancellationToken);

            if (totalCollected <= 0)
            {
                return 180;
            }

            var collectedCapital = await loanService.GetTotalCapitalCollectedAsync(cancellationToken);
            var capitalAngle = (double)(collectedCapital / totalCollected) * 360.0;
            return capitalAngle;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetCapitalAngle operation was canceled.");
            return 180;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the capital percentage angle.");
            return 180;
        }
    }

    private async Task<double> GetPendingDuePercentageAsync()
    {
        this._ctSources[nameof(this.GetPendingDuePercentageAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetPendingDuePercentageAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            var totalDueAmount = await loanService.GetTotalCurrentDuePaymentAmountAsync(cancellationToken);

            if (totalDueAmount <= 0)
            {
                return 0;
            }

            var pendingDueAmount = await loanService.GetTotalCurrentPendingDuePaymentAmountAsync(cancellationToken);
            var pendingDuePercentage = (double)(pendingDueAmount / totalDueAmount) * 100.0;
            return pendingDuePercentage;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetPendingDuePercentage operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the pending due value.");
            return 0;
        }
    }

    private async Task<string> GetPendingDuePercentageFormattedAsync()
    {
        this._ctSources[nameof(this.GetPendingDuePercentageFormattedAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetPendingDuePercentageFormattedAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            var totalDueAmount = await loanService.GetTotalCurrentDuePaymentAmountAsync(cancellationToken);

            if (totalDueAmount <= 0)
            {
                return "0.0%";
            }

            var pendingDueAmount = await loanService.GetTotalCurrentPendingDuePaymentAmountAsync(cancellationToken);
            var pendingDuePercentage = (double)(pendingDueAmount / totalDueAmount) * 100.0;
            return $"{pendingDuePercentage:F1}%";
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetPendingDuePercentageFormatted operation was canceled.");
            return "Canceled";
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the pending due percentage.");
            return "Ups!";
        }
    }

    private async Task<string> GetTotalCollectedFormattedAsync()
    {
        this._ctSources[nameof(this.GetTotalCollectedFormattedAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalCollectedFormattedAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            var totalCollected = await loanService.GetTotalCollectedAsync(cancellationToken);
            return totalCollected.ToString("C0");
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetTotalCollectedFormatted operation was canceled.");
            return "Canceled";
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the total collected.");
            return "Ups!";
        }
    }

    private async Task<decimal> GetTotalCurrentDuePaymentAmountAsync()
    {
        this._ctSources[nameof(this.GetTotalCurrentDuePaymentAmountAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalCurrentDuePaymentAmountAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await loanService.GetTotalCurrentDuePaymentAmountAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetTotalCurrentDuePaymentAmountAsync operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the total current due payment amount.");
            return 0;
        }
    }

    private async Task<string> GetTotalLoansGrantedFormattedAsync()
    {
        this._ctSources[nameof(this.GetTotalLoansGrantedFormattedAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalLoansGrantedFormattedAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            var totalLoanGranted = await loanService.GetTotalLoansGrantedAsync(cancellationToken);
            return totalLoanGranted.ToString("C0");
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("GetTotalLoansGrantedFormattedAsync operation was canceled.");
            return "Canceled";
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            logger.LogError(ex, "An error occurred while getting the total loans granted.");
            return "Ups!";
        }
    }

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
