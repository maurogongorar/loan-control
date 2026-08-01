using Cocosoft.Finance.LoanControl.Domain.Customers;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

/// <summary>
/// The ICustomerRepository interface defines the contract for a repository that manages customer data.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Asynchronously adds a new customer to the repository.
    /// </summary>
    /// <param name="customerDto">The customer DTO to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the added added customer DTO.
    /// </returns>
    ValueTask<CustomerDto> AddAsync(CustomerDto customerDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously soft deletes a customer from the repository.
    /// </summary>
    /// <param name="customerId">The ID of the customer to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains a boolean indicating whether the customer was successfully deleted.
    /// </returns>
    ValueTask<bool> DeleteAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds a customer by their ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the customer DTO if found; otherwise, null.
    /// </returns>
    ValueTask<CustomerDto?> FindByIdAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds customers by their document number.
    /// </summary>
    /// <param name="documentNumber">The document number to search for.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains a collection of customer search results that match the document number.
    /// </returns>
    ValueTask<IEnumerable<CustomerSearchResult>> FindByIDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing customer in the repository.
    /// </summary>
    /// <param name="customerDto">The customer DTO to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the updated customer DTO.
    /// </returns>
    ValueTask<CustomerDto> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken = default);
}
