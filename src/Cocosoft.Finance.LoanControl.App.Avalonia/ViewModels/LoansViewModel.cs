using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// This class represents the view model for managing loans in the application.
/// It provides properties and commands for searching, creating, and viewing loan details.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class LoansViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;

    private LoanSearchResult? _selectedSearchResult;

    /// <summary>
    /// Gets or sets the count of currently active loans.
    /// </summary>
    [ObservableProperty]
    public partial int ActiveLoansCount { get; set; } = 12;

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
                this.SelectLoan(value);
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
    [ObservableProperty]
    public partial decimal TotalDebt { get; set; } = 485_000m;

    /// <summary>
    /// Gets or sets the total interest collected across all loans.
    /// </summary>
    [ObservableProperty]
    public partial decimal TotalInterestCollected { get; set; } = 62_300m;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoansViewModel"/> class
    /// using a default <see cref="DialogService"/> (design-time support).
    /// </summary>
    public LoansViewModel() : this(new DialogService(), new CreateLoanViewModel())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoansViewModel"/> class.
    /// </summary>
    /// <param name="dialogService">The dialog service used for confirmation prompts.</param>
    /// <param name="createLoanForm">The view model for the create-loan form.</param>
    [ActivatorUtilitiesConstructor]
    public LoansViewModel(IDialogService dialogService, CreateLoanViewModel createLoanForm)
    {
        this._dialogService = dialogService;
        this.CreateLoanForm = createLoanForm;
        this.CreateLoanForm.LoanCreated += this.OnLoanCreated;
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
    private void Search()
    {
        this.SearchResults.Clear();
        this.SearchResults.Add(new LoanSearchResult
        {
            LoanNumber = "CR-001",
            DebtorName = "Juan Pérez",
            LoanAmount = 50_000m,
            InstallmentAmount = 4_800m,
            CurrentDebt = 38_200m,
            LastPaymentDate = new DateTime(2025, 6, 15)
        });
        this.SearchResults.Add(new LoanSearchResult
        {
            LoanNumber = "CR-002",
            DebtorName = "María López",
            LoanAmount = 120_000m,
            InstallmentAmount = 11_500m,
            CurrentDebt = 95_000m,
            LastPaymentDate = new DateTime(2025, 6, 20)
        });

        this.IsShowingResults = true;
        this.IsCreatingLoan = false;
        this.IsShowingDetail = false;
    }

    private void SelectLoan(LoanSearchResult? loan)
    {
        if (loan is null)
        {
            return;
        }

        this.SelectedLoanDetail = new LoanDto
        {
            LoanNumber = loan.LoanNumber,
            DebtorName = loan.DebtorName,
            DebtorIdentification = "ID-123456789",
            DisbursementDate = new DateTime(2025, 1, 15),
            InitialAmount = loan.LoanAmount,
            InstallmentAmount = loan.InstallmentAmount,
            TermMonths = 12,
            AnnualInterestRate = 18m,
            MonthlyInterestRate = 1.5m,
            PaidInstallments = 3,
            TotalInstallments = 12,
            TotalInterestPaid = 2_065m,
            CurrentDebt = loan.CurrentDebt,
            Payments = new ObservableCollection<PaymentDto>
            {
                new()
                {
                    PaymentDate = new DateTime(2025, 4, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 750m,
                    PrincipalPaid = 4_050m
                },
                new()
                {
                    PaymentDate = new DateTime(2025, 5, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 690m,
                    PrincipalPaid = 4_110m
                },
                new()
                {
                    PaymentDate = new DateTime(2025, 6, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 625m,
                    PrincipalPaid = 4_175m
                },
            }
        };

        this.IsShowingDetail = true;
        this.IsCreatingLoan = false;
        this.IsShowingResults = false;
    }
}
