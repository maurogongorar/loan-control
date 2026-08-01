using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// View model for the create-loan form. Exposes borrower and loan fields,
/// tracks dirty state, and handles submission with a confirmation dialog.
/// </summary>
/// <seealso cref="ViewModelBase" />
/// <remarks>
/// Initializes a new instance of the <see cref="CreateLoanViewModel"/> class.
/// </remarks>
/// <param name="dialogService">The dialog service used for confirmation prompts.</param>
public partial class CreateLoanViewModel(IDialogService dialogService) : ViewModelBase
{
    /// <summary>
    /// Gets or sets the annual interest rate entered by the user.
    /// </summary>
    [ObservableProperty]
    public partial string AnnualInterestRate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the borrower's first name. This field is read-only in the UI.
    /// </summary>
    [ObservableProperty]
    public partial string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the borrower's identification number.
    /// </summary>
    [ObservableProperty]
    public partial string IdentificationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether any form field has been modified.
    /// </summary>
    public bool IsDirty =>
        !string.IsNullOrEmpty(this.IdentificationNumber)
        || !string.IsNullOrEmpty(this.FirstName)
        || !string.IsNullOrEmpty(this.LastName)
        || !string.IsNullOrEmpty(this.LoanAmount)
        || !string.IsNullOrEmpty(this.NumberOfInstallments)
        || !string.IsNullOrEmpty(this.AnnualInterestRate)
        || !string.IsNullOrEmpty(this.MonthlyInterestRate)
        || !string.IsNullOrEmpty(this.MonthlyInstallmentAmount);

    /// <summary>
    /// Gets or sets the borrower's last name. This field is read-only in the UI.
    /// </summary>
    [ObservableProperty]
    public partial string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the loan amount entered by the user.
    /// </summary>
    [ObservableProperty]
    public partial string LoanAmount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the monthly installment amount entered by the user.
    /// </summary>
    [ObservableProperty]
    public partial string MonthlyInstallmentAmount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the monthly interest rate entered by the user.
    /// </summary>
    [ObservableProperty]
    public partial string MonthlyInterestRate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of installments entered by the user.
    /// </summary>
    [ObservableProperty]
    public partial string NumberOfInstallments { get; set; } = string.Empty;

    /// <summary>
    /// Occurs when a loan has been successfully created after user confirmation.
    /// </summary>
    public event Action? LoanCreated;

    /// <summary>
    /// Resets all form fields to their default empty values.
    /// </summary>
    public void Reset()
    {
        this.AnnualInterestRate = string.Empty;
        this.FirstName = string.Empty;
        this.IdentificationNumber = string.Empty;
        this.LastName = string.Empty;
        this.LoanAmount = string.Empty;
        this.MonthlyInstallmentAmount = string.Empty;
        this.MonthlyInterestRate = string.Empty;
        this.NumberOfInstallments = string.Empty;
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        var summary = $"Identificaci\u00f3n: {this.IdentificationNumber}\n"
            + $"Nombres: {this.FirstName}\n"
            + $"Apellidos: {this.LastName}\n"
            + $"Monto: {this.LoanAmount}\n"
            + $"Cuotas: {this.NumberOfInstallments}\n"
            + $"Inter\u00e9s Anual: {this.AnnualInterestRate}%\n"
            + $"Inter\u00e9s Mensual: {this.MonthlyInterestRate}%\n"
            + $"Cuota Mensual: {this.MonthlyInstallmentAmount}";

        var confirmed = await dialogService.ShowConfirmationAsync(
            "Confirmar Pr\u00e9stamo", summary);

        if (confirmed)
        {
            // TODO: Persist the loan
            this.OnLoanCreated();
        }
    }

    private void OnLoanCreated() => this.LoanCreated?.Invoke();
}
