namespace Cocosoft.Finance.LoanControl.Domain.Customers;

/// <summary>
/// The result of a customer search operation, containing essential information about the customer.
/// </summary>
public class CustomerSearchResult
{
    /// <summary>
    /// Gets or sets the document number of the customer.
    /// This property represents a unique identifier for the customer,
    /// such as a social security number or tax identification number.
    /// </summary>
    /// <value>
    /// The document number of the customer.
    /// </value>
    public required string DocumentNumber { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the customer. This property is used as the primary key in the database.
    /// </summary>
    /// <value>
    /// The unique identifier for the customer.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer. This property represents the first name or given name of the customer.
    /// </summary>
    /// <value>
    /// The name of the customer.
    /// </value>
    public required string Name { get; set; }
}
