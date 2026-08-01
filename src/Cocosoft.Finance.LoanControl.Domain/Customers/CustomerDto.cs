namespace Cocosoft.Finance.LoanControl.Domain.Customers;

/// <summary>
/// The CustomerDto class represents a data transfer object for customer information.
/// This class is used to transfer customer data between different layers of the application.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    /// <value>
    /// The address of the customer.
    /// </value>
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the city of the customer.
    /// </summary>
    /// <value>
    /// The city of the customer.
    /// </value>
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    /// <value>
    /// The email of the customer.
    /// </value>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer. This property represents the first name or given name of the customer.
    /// </summary>
    /// <value>
    /// Teh name of the customer.
    /// </value>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the customer. This property is used as the primary key in the database.
    /// </summary>
    /// <value>
    /// The unique identifier for the customer.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the legal identification number of the customer.
    /// This is a unique identifier for the customer, such as a social security number or tax identification number.
    /// </summary>
    /// <value>
    /// The legal identification number of the customer.
    /// </value>
    public required string IdentificationNumber { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer. This property represents the last name or family name of the customer.
    /// </summary>
    /// <value>
    /// The last name of the customer.
    /// </value>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    /// <value>
    /// The phone number of the customer.
    /// </value>
    public required string PhoneNumber { get; set; }
}
