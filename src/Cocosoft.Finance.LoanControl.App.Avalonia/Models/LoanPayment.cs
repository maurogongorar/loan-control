using System;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Models;

/// <summary>
/// Represents a single payment made against a loan, including the breakdown
/// of principal and interest portions.
/// </summary>
public sealed class LoanPayment
{
    /// <summary>
    /// Gets the interest portion of this payment.
    /// </summary>
    public required decimal InterestPaid { get; init; }

    /// <summary>
    /// Gets the total payment amount.
    /// </summary>
    public required decimal PaymentAmount { get; init; }

    /// <summary>
    /// Gets the date when the payment was made.
    /// </summary>
    public required DateTime PaymentDate { get; init; }

    /// <summary>
    /// Gets the principal portion of this payment.
    /// </summary>
    public required decimal PrincipalPaid { get; init; }
}
