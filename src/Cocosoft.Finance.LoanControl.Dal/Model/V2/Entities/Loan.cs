using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;

/// <summary>
/// The Loan class represents a loan record in the database. It contains properties that describe the loan's details,
/// such as the debtor's name, disbursement date, interest rate, and payment information. This class is used to map to
/// the "LOANS" table in the database and is versioned to allow for tracking changes over time.
/// </summary>
[Table("LOANS")]
[PrimaryKey(nameof(Id), nameof(Version))]
[Index(nameof(LoanNumber), Name = "IX_U_LOANS_LOAN_NUMBER", IsUnique = true)]
internal class Loan
{
    /// <summary>
    /// Gets or sets the annual interest rate applied to the loan. This value is used to calculate interest accruals
    /// and payments.
    /// </summary>
    /// <value>
    /// The annual interest rate.
    /// </value>
    [Column("ANNUAL_INTEREST", Order = 11)]
    public double AnnualInterest { get; set; }

    /// <summary>
    /// Gets or sets the current balance of the loan. This value represents the outstanding amount that the debtor
    /// owes at any given time.
    /// </summary>
    /// <value>
    /// The current balance of the loan.
    /// </value>
    [Column("CURRENT_BALANCE", Order = 8)]
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets the debtor associated with the loan. This property is used for identification and
    /// navigation purposes.
    /// </summary>
    /// <value>
    /// The the debtor associated with the loan.
    /// </value>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the customer associated with the loan. This property is used to establish
    /// a relationship between the loan and the customer entity.
    /// </summary>
    /// <value>
    /// The identifier of the customer associated with the loan.
    /// </value>
    [Column("CUSTOMER_ID", Order = 4)]
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the version number of the customer record associated with the loan. This property is used to track
    /// changes to the customer over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record associated with the loan.
    /// </value>
    [Column("CUSTOMER_VERSION", Order = 5)]
    public int CustomerVersion { get; set; }

    /// <summary>
    /// Gets or sets the date when the loan was disbursed to the debtor. This date is important for calculating
    /// interest accruals and determining the loan's repayment schedule.
    /// </summary>
    /// <value>
    /// The date when the loan was disbursed.
    /// </value>
    [Column("DISBURSEMENT_DATE", Order = 7)]
    public DateTime DisbursementDate { get; set; }

    /// <summary>
    /// Gets or sets the day of the month when the loan payment is due.
    /// This value is used to determine the payment schedule.
    /// </summary>
    /// <value>
    /// The day of the month when the loan payment is due.
    /// </value>
    [Column("DUE_DAY", Order = 10)]
    public int DueDay { get; set; } = 30;

    /// <summary>
    /// Gets or sets the fixed installment amount that the debtor is required to pay for each installment period.
    /// </summary>
    /// <value>
    /// The fixed installment amount.
    /// </value>
    [Column("FEE", Order = 9)]
    public decimal Fee { get; set; }

    /// <summary>
    /// Gets or sets the unique loan number identifier. This property is used to uniquely identify
    /// the loan in the system.
    /// </summary>
    /// <value>
    /// The unique loan number identifier.
    /// </value>
    [Column("ID", Order = 0)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the initial amount granted for the loan. This value represents the original principal amount
    /// that was lent to the borrower.
    /// </summary>
    /// <value>
    /// The initial amount granted for the loan.
    /// </value>
    [Column("INITIAL_AMOUNT", Order = 6)]
    public decimal InitialAmount { get; set; }

    /// <summary>
    /// Gets or sets the acumulated interest collected on the loan. This value represents the total interest
    /// that has been collected from the borrower over the life of the loan.
    /// </summary>
    /// <value>
    /// The acumulated interest collected on the loan.
    /// </value>
    [Column("INTEREST_COLLECTED", Order = 14)]
    public decimal InterestCollected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this loan is closed. A closed loan indicates that the borrower has
    /// fully repaid the loan and no further payments are expected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this loan is closed; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_CLOSED", Order = 15)]
    public bool IsClosed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is the current version of the customer record.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is the current version; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_CURRENT", Order = 2)]
    public bool IsCurrent { get; set; } = true;

    /// <summary>
    /// Gets or sets the date of the last payment made on the loan. This value is used to track
    /// the most recent payment activity.
    /// </summary>
    /// <value>
    /// The date of the last payment made on the loan.
    /// </value>
    [Column("LAST_PAYMENT_DATE", Order = 12)]
    public DateTime? LastPaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the unique loan number identifier. This property is used to uniquely identify
    /// </summary>
    /// <value>
    /// The unique loan number identifier.
    /// </value>
    [Column("LOAN_NUMBER", Order = 3)]
    public required string LoanNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of installments that have been pacted for the loan. This value is used to determine
    /// the repayment schedule and the total number of payments expected.
    /// </summary>
    /// <value>
    /// The number of installments that have been pacted for the loan.
    /// </value>
    [Column("NUMBER_INSTALMENTS", Order = 13)]
    public int NumberInstalments { get; set; }

    /// <summary>
    /// Gets or sets the collection of payments made against this loan. This property establishes a relationship
    /// between the loan and its associated payments.
    /// </summary>
    /// <value>
    /// The payments made against this loan.
    /// </value>
    public ICollection<Payment> Payments { get; internal set; } = [];

    /// <summary>
    /// Gets or sets the version number of the customer record. This property is used to track changes to the customer data over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record.
    /// </value>
    [Column("VERSION", Order = 1)]
    public int Version { get; set; }
}
