using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;

/// <summary>
/// The Account class represents an account entity in the database. It is associated with a customer and can have
/// multiple loans. The class includes properties for account number, customer reference, status flags, and versioning
/// information.
/// </summary>
[Table("ACCOUNTS")]
[PrimaryKey(nameof(Id), nameof(Version))]
[Index(nameof(AccountNumber), Name = "IX_U_ACCOUNTS_ACCOUNT_NUMBER", IsUnique = true)]
internal class Account
{
    /// <summary>
    /// Gets or sets the account number. This property is used to uniquely identify the account and is indexed for
    /// quick lookups.
    /// </summary>
    /// <value>
    /// The account number.
    /// </value>
    [Column("ACCOUNT_NUMBER", Order = 3)]
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with this account.
    /// This property establishes a relationship between the account and the customer entity,
    /// allowing for navigation and data retrieval related to the customer.
    /// </summary>
    /// <value>
    /// The customer associated with this account.
    /// </value>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the customer associated with this account. This property is used to establish
    /// a relationship between the account and the customer entity.
    /// </summary>
    /// <value>
    /// The identifier of the customer associated with this account.
    /// </value>
    [Column("CUSTOMER_ID", Order = 4)]
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the version number of the customer record associated with this account. This property is used to track
    /// changes to the customer data over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record.
    /// </value>
    [Column("CUSTOMER_VERSION", Order = 5)]
    public int CustomerVersion { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the account. This property is used as the primary key in the
    /// database and is automatically generated.
    /// </summary>
    /// <value>
    /// The unique identifier for the account.
    /// </value>
    [Column("ID", Order = 0)]
    public int Id { get; set; }

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
    [Column("IS_DELETED", Order = 7)]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is locked.
    /// This property can be used to prevent modifications to the account.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_LOCKED", Order = 6)]
    public bool IsLocked { get; set; }

    /// <summary>
    /// Gets or sets the collection of loans associated with this account. This property establishes a one-to-many
    /// relationship between the account and the loan entities.
    /// </summary>
    /// <value>
    /// The collection of loans associated with this account.
    /// </value>
    public ICollection<Loan> Loans { get; set; } = [];

    /// <summary>
    /// Gets or sets the version number of the customer record. This property is used to track changes to the customer data over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record.
    /// </value>
    [Column("VERSION", Order = 1)]
    public int Version { get; set; }
}
