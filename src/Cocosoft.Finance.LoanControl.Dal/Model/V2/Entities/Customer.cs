using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;

/// <summary>
/// The Customer class represents a customer entity in the database.
/// </summary>
[Table("CUSTOMERS")]
[PrimaryKey(nameof(Id), nameof(Version))]
[Index(nameof(IdentificationNumber), Name = "IX_U_CUSTOMERS_IDENTIFICATION_NUMBER", IsUnique = true)]
internal class Customer
{
    /// <summary>
    /// Gets or sets the collection of accounts associated with the customer.
    /// This property represents a one-to-many relationship between the customer and their accounts.
    /// </summary>
    /// <value>
    /// The collection of accounts associated with the customer.
    /// </value>
    public ICollection<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    /// <value>
    /// The address of the customer.
    /// </value>
    [Column("ADDRESS", Order = 9)]
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the city of the customer.
    /// </summary>
    /// <value>
    /// The city of the customer.
    /// </value>
    [Column("CITY", Order = 8)]
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    /// <value>
    /// The email of the customer.
    /// </value>
    [Column("EMAIL", Order = 7)]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the customer. This property is used as the primary key in the database.
    /// </summary>
    /// <value>
    /// The unique identifier for the customer.
    /// </value>
    [Column("ID", Order = 0)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the legal identification number of the customer.
    /// This is a unique identifier for the customer, such as a social security number or tax identification number.
    /// </summary>
    /// <value>
    /// The legal identification number of the customer.
    /// </value>
    [Column("IDENTIFICATION_NUMBER", Order = 3)]
    public required string IdentificationNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is the current version of the customer record.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is the current version; otherwise, <c>false</c>.
    /// </value>
    [Column("IS_CURRENT", Order = 2)]
    public bool IsCurrent { get; set; } = true;

    /// <summary>
    /// Gets or sets the name of the customer. This property represents the first name or given name of the customer.
    /// </summary>
    /// <value>
    /// Teh name of the customer.
    /// </value>
    [Column("NAME", Order = 4)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    /// <value>
    /// The phone number of the customer.
    /// </value>
    [Column("PHONE_NUMBER", Order = 6)]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the surname of the customer. This property represents the last name or family name of the customer.
    /// </summary>
    /// <value>
    /// The surname of the customer.
    /// </value>
    [Column("SURNAME", Order = 5)]
    public required string Surname { get; set; }

    /// <summary>
    /// Gets or sets the version number of the customer record. This property is used to track changes to the customer data over time.
    /// </summary>
    /// <value>
    /// The version number of the customer record.
    /// </value>
    [Column("VERSION", Order = 1)]
    public int Version { get; set; }
}
