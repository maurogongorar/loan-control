using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.Core.Services.V2;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// This class represents the view model for managing loans in the application.
/// It provides properties and commands for searching, creating, and viewing loan details.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class LoansViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;

    private readonly IDictionary<string, CancellationTokenSource?> _ctSources =
        new Dictionary<string, CancellationTokenSource?>
        {
            [nameof(LoansViewModel.GetActiveLoansCountAsync)] = null,
            [nameof(LoansViewModel.GetTotalDebtAsync)] = null,
            [nameof(LoansViewModel.GetTotalInterestCollectedAsync)] = null
        };

    private readonly IDialogService _dialogService;

    private readonly ILoanService _loanService;

    private readonly ILogger<LoansViewModel> _logger;

    private LoanSearchResult? _selectedSearchResult;

    /// <summary>
    /// Gets or sets the count of currently active loans.
    /// </summary>
    public Task<int> ActiveLoansCount => this.GetActiveLoansCountAsync();

    /// <summary>
    /// Gets the view model for the create-loan form.
    /// </summary>
    public CreateLoanViewModel CreateLoanForm { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the create loan form is currently displayed.
    /// </summary>
    [ObservableProperty]
    public partial bool IsCreatingLoan { get; set; }

    /// <summary>
    /// Get or sets a value indicating whether the application is currently loading data.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the application is currently loading data; otherwise, <c>false</c>.
    /// </value>
    [ObservableProperty]
    public partial bool IsLoading { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether a loan detail view is currently displayed.
    /// </summary>
    [ObservableProperty]
    public partial bool IsShowingDetail { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether search results are currently displayed.
    /// </summary>
    [ObservableProperty]
    public partial bool IsShowingResults { get; set; }

    /// <summary>
    /// Gets or sets the search text used for filtering loan search results.
    /// </summary>
    /// <value>
    /// The search text as a string. It is initialized to an empty string.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
    public partial string SearchText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the currently selected loan detail.
    /// </summary>
    [ObservableProperty]
    public partial LoanDto? SelectedLoanDetail { get; set; }

    /// <summary>
    /// Gets or sets the currently selected search result. Setting a non-null value
    /// triggers navigation to the loan detail view.
    /// </summary>
    public LoanSearchResult? SelectedSearchResult
    {
        get => this._selectedSearchResult;
        set
        {
            if (this.SetProperty(ref this._selectedSearchResult, value)
                && value is not null)
            {
                _ = this.SelectLoanAsync(value);
            }
        }
    }

    /// <summary>
    /// Gets the collection of loan search results.
    /// </summary>
    public ObservableCollection<LoanSearchResult> SearchResults { get; } = [];

    /// <summary>
    /// Gets or sets the total debt amount across all loans.
    /// </summary>
    public Task<decimal> TotalDebt => this.GetTotalDebtAsync();

    /// <summary>
    /// Gets or sets the total interest collected across all loans.
    /// </summary>
    public Task<decimal> TotalInterestCollected => this.GetTotalInterestCollectedAsync();

    /// <summary>
    /// Initializes a new instance of the <see cref="LoansViewModel"/> class.
    /// </summary>
    /// <param name="dialogService">The dialog service used for confirmation prompts.</param>
    /// <param name="loanService">The loan service used for managing loan data.</param>
    /// <param name="createLoanForm">The view model for the create-loan form.</param>
    [ActivatorUtilitiesConstructor]
    public LoansViewModel(
        IDialogService dialogService,
        ILoanService loanService,
        CreateLoanViewModel createLoanForm,
        ILogger<LoansViewModel> logger)
    {
        this._dialogService = dialogService;
        this._loanService = loanService;
        this.CreateLoanForm = createLoanForm;
        this.CreateLoanForm.LoanCreated += this.OnLoanCreated;
        this._logger = logger;
    }

    [RelayCommand]
    private void AddPayment()
    {
        // TODO: Implement add payment form
    }

    private bool CanSearch()
    {
        if (!string.IsNullOrWhiteSpace(this.SearchText))
        {
            return true;
        }

        return false;
    }

    [RelayCommand]
    private void CreateLoan()
    {
        this.IsCreatingLoan = true;
        this.IsShowingResults = false;
        this.IsShowingDetail = false;
    }

    private async Task<int> GetActiveLoansCountAsync()
    {
        this._ctSources[nameof(this.GetActiveLoansCountAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetActiveLoansCountAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._loanService.GetActiveLoanCountsAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetActiveLoansCountAsync operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            this._logger.LogError(ex, "An error occurred while getting the active loans count.");
            return 0;
        }
    }

    private async Task<decimal> GetTotalDebtAsync()
    {
        this._ctSources[nameof(this.GetTotalDebtAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalDebtAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._loanService.GetTotalActiveDebtAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetTotalDebtAsync operation was canceled.");
            return 0m;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            this._logger.LogError(ex, "An error occurred while getting the total debt.");
            return 0m;
        }
    }

    private async Task<decimal> GetTotalInterestCollectedAsync()
    {
        this._ctSources[nameof(this.GetTotalInterestCollectedAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalInterestCollectedAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._loanService.GetTotalInterestCollectedAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetTotalInteresCollectedAsync operation was canceled.");
            return 0m;
        }
        catch (Exception ex)
        {
            // TODO:
            this._logger.LogError(ex, "An error occurred while gettin the total interest collected.");
            return 0m;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        if (this.IsShowingDetail)
        {
            this._selectedSearchResult = null;
            this.OnPropertyChanged(nameof(this.SelectedSearchResult));
            this.IsShowingDetail = false;
            this.IsShowingResults = true;
        }
        else if (this.IsCreatingLoan)
        {
            if (this.CreateLoanForm.IsDirty)
            {
                var confirmed = await this._dialogService.ShowConfirmationAsync(
                    "Abandonar formulario",
                    "¿Desea abandonar el formulario? Los datos ingresados se perderán.");

                if (!confirmed)
                {
                    return;
                }
            }

            this.CreateLoanForm.Reset();
            this.SearchText = string.Empty;
            this.IsCreatingLoan = false;
        }
    }

    private void OnLoanCreated()
    {
        this.CreateLoanForm.Reset();
        this.IsCreatingLoan = false;
    }

    [RelayCommand(CanExecute = nameof(CanSearch))]
    private async Task Search()
    {
        this.IsLoading = true;
        var loansResult = await this._loanService.SearchLoansAsync(this.SearchText);
        this.IsLoading = false;

        this.SearchResults.Clear();
        
        foreach (var loan in loansResult)
        {
            this.SearchResults.Add(loan);
        }

        this.IsShowingResults = true;
        this.IsCreatingLoan = false;
        this.IsShowingDetail = false;
    }

    private async Task SelectLoanAsync(LoanSearchResult? loan)
    {
        if (loan is null)
        {
            return;
        }

        this._cancellationTokenSource?.Cancel();
        this._cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = this._cancellationTokenSource.Token;

        try
        {
            this.IsLoading = true;
            var loanDetail = await this._loanService.FindLoanAsync(loan.LoanId, cancellationToken);

            if (loanDetail is null)
            {
                return;
            }

            this.SelectedLoanDetail = loanDetail;
            this.IsShowingDetail = true;
            this.IsCreatingLoan = false;
            this.IsShowingResults = false;
        }
        catch (OperationCanceledException)
        {
            // The operation was canceled, no action needed.
            this._logger.LogInformation("Loan selection operation was canceled.");
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception (e.g., log it, show an error message to the user)
            this._logger.LogError(ex, "An error occurred while selecting the loan.");
        }
        finally
        {
            this.IsLoading = false;
        }
    }
}
