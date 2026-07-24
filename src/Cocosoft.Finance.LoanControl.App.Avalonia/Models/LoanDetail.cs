using System;
using System.Collections.ObjectModel;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Models;

/// <summary>
/// Represents the detailed information of a loan, including debtor data,
/// financial terms, payment history, and current debt status.
/// </summary>
public sealed class LoanDetail
{
    /// <summary>
    /// Gets the annual interest rate applied to the loan.
    /// </summary>
    public required decimal AnnualInterestRate { get; init; }

    /// <summary>
    /// Gets the current outstanding debt of the loan.
    /// </summary>
    public required decimal CurrentDebt { get; init; }

    /// <summary>
    /// Gets the identification number of the debtor.
    /// </summary>
    public required string DebtorIdentification { get; init; }

    /// <summary>
    /// Gets the full name of the debtor.
    /// </summary>
    public required string DebtorName { get; init; }

    /// <summary>
    /// Gets the date when the loan was disbursed.
    /// </summary>
    public required DateTime DisbursementDate { get; init; }

    /// <summary>
    /// Gets the initial amount granted for the loan.
    /// </summary>
    public required decimal InitialAmount { get; init; }

    /// <summary>
    /// Gets the fixed installment amount to be paid each period.
    /// </summary>
    public required decimal InstallmentAmount { get; init; }

    /// <summary>
    /// Gets the unique loan number identifier.
    /// </summary>
    public required string LoanNumber { get; init; }

    /// <summary>
    /// Gets the monthly interest rate applied to the loan.
    /// </summary>
    public required decimal MonthlyInterestRate { get; init; }

    /// <summary>
    /// Gets the number of installments that have been paid.
    /// </summary>
    public required int PaidInstallments { get; init; }

    /// <summary>
    /// Gets the collection of payments made against this loan.
    /// </summary>
    public required ObservableCollection<LoanPayment> Payments { get; init; }

    /// <summary>
    /// Gets the total term of the loan in months.
    /// </summary>
    public required int TermMonths { get; init; }

    /// <summary>
    /// Gets the total number of installments for the loan.
    /// </summary>
    public required int TotalInstallments { get; init; }

    /// <summary>
    /// Gets the total amount of interest paid so far.
    /// </summary>
    public required decimal TotalInterestPaid { get; init; }
}
