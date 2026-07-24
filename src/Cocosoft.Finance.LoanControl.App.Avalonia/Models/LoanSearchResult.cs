using System;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Models;

/// <summary>
/// Represents a loan search result item containing summary information
/// about a loan for display in search result listings.
/// </summary>
public sealed class LoanSearchResult
{
    /// <summary>
    /// Gets the current outstanding debt of the loan.
    /// </summary>
    public required decimal CurrentDebt { get; init; }

    /// <summary>
    /// Gets the full name of the debtor.
    /// </summary>
    public required string DebtorName { get; init; }

    /// <summary>
    /// Gets the fixed installment amount per period.
    /// </summary>
    public required decimal InstallmentAmount { get; init; }

    /// <summary>
    /// Gets the date of the last payment, or <see langword="null"/> if no payments have been made.
    /// </summary>
    public required DateTime? LastPaymentDate { get; init; }

    /// <summary>
    /// Gets the original loan amount.
    /// </summary>
    public required decimal LoanAmount { get; init; }

    /// <summary>
    /// Gets the unique loan number identifier.
    /// </summary>
    public required string LoanNumber { get; init; }
}
