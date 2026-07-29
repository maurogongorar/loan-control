using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;

/// <summary>
/// The Payment class represents a payment entity in the database. It contains properties that map to the columns
/// of the "PAYMENTS" table. The class also establishes a relationship with the Loan entity through the LoanId property.
/// </summary>
[Table("PAYMENTS")]
[PrimaryKey(nameof(Id), nameof(Version))]
internal class Payment
{
    /// <summary>
    /// Gets or sets the amount of the payment. This property represents the monetary value of
    /// the payment made towards a loan.
    /// </summary>
    /// <value>
    /// The amount of the payment.
    /// </value>
    [Column("AMOUNT", Order = 6)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the capital portion of the payment. This property represents the amount of the payment
    /// that is applied towards the principal balance of the loan.
    /// </summary>
    /// <value>
    /// The capital portion of the payment.
    /// </value>
    [Column("CAPITAL", Order = 7)]
    public decimal Capital { get; set; }

    /// <summary>
    /// Gets or sets the date of the payment. This property represents the date on which the payment was made.
    /// </summary>
    /// <value>
    /// The date of the payment.
    /// </value>
    [Column("DATE", Order = 5)]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the payment. This property is used as the primary key in the database
    /// </summary>
    /// <value>
    /// The unique identifier for the payment.
    /// </value>
    [Column("ID", Order = 0)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the interest portion of the payment. This property represents the amount of the payment
    /// that is applied towards the interest of the loan.
    /// </summary>
    /// <value>
    /// The interest portion of the payment.
    /// </value>
    [Column("INTEREST", Order = 8)]
    public decimal Interest { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is the current version of the customer record.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is the current version; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_CURRENT", Order = 2)]
    public bool IsCurrent { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is marked as deleted.
    /// This property is used for soft deletion.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is marked as deleted; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_DELETED", Order = 10)]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the loan associated with this payment. This property establishes a navigation property
    /// to the Loan entity, allowing access to the related loan details.
    /// </summary>
    /// <value>
    /// The loan associated with this payment.
    /// </value>
    public Loan? Loan { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the loan associated with this payment. This property establishes
    /// a relationship with the Loan entity.
    /// </summary>
    /// <value>
    /// The unique identifier for the loan associated with this payment.
    /// </value>
    [Column("LOAN_ID", Order = 3)]
    public int LoanId { get; set; }

    /// <summary>
    /// Gets or sets the version number of the loan associated with this payment. This property is used to track
    /// changes to the loan over time.
    /// </summary>
    /// <value>
    /// The version number of the loan associated with this payment.
    /// </value>
    [Column("LOAN_VERSION", Order = 4)]
    public int LoanVersion { get; set; }

    /// <summary>
    /// Gets or sets the new balance of the loan after this payment has been applied. This property represents
    /// the remaining principal balance of the loan.
    /// </summary>
    /// <value>
    /// The new balance of the loan after this payment has been applied.
    /// </value>
    [Column("NEW_BALANCE", Order = 9)]
    public decimal NewBalance { get; set; }

    /// <summary>
    /// Gets or sets the version number of the customer record. This property is used to track changes to the customer data over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record.
    /// </value>
    [Column("VERSION", Order = 1)]
    public int Version { get; set; }
}
