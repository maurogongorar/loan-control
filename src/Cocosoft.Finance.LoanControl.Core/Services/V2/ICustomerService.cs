using Cocosoft.Finance.LoanControl.Domain.Customers;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

/// <summary>
/// This interface defines the contract for customer-related operations in the loan control system.
/// It provides methods for adding, finding, counting, and searching customers based on various criteria.
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Asynchronously adds a new customer to the system.
    /// </summary>
    /// <param name="customer">The customer to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the customer that was added, or null if the operation failed.
    /// </returns>
    ValueTask<CustomerDto?> AddCustomerAsync(CustomerDto customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds a customer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the customer with the specified identifier, or null if no such customer exists.
    /// </returns>
    ValueTask<CustomerDto?> FindCustomerAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the total count of customers in the system.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total count of customers.
    /// </returns>
    ValueTask<int> GetCustomersCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the count of customers with active loans in the system.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the count of customers with active loans.
    /// </returns>
    ValueTask<int> GetCustomersWithActiveLoanCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the count of new customers in the last month in the system.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the count of new customers.
    /// </returns>
    ValueTask<int> GetNewCustomersCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously searches for customers by their document number.
    /// </summary>
    /// <param name="documentNumber">The document number to search for.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains a collection of customers matching the document number.
    /// </returns>
    ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(
        string documentNumber,
        CancellationToken cancellationToken = default);
}
