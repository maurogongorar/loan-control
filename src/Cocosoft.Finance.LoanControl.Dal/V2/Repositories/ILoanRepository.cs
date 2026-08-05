using Cocosoft.Finance.LoanControl.Domain.Loans;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

/// <summary>
/// The ILoanRepository interface defines the contract for a repository that manages loan data.
/// </summary>
public interface ILoanRepository
{
    /// <summary>
    /// Asynchronously adds a new loan to the repository.
    /// </summary>
    /// <param name="loan">The loan to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the added loan.
    /// </returns>
    ValueTask<LoanDto> AddAsync(LoanDto loan, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds a loan by its unique identifier.
    /// </summary>
    /// <param name="loanId">The unique identifier of the loan.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the loan with the specified identifier, or <c>null</c> if no such loan exists.
    /// </returns>
    ValueTask<LoanDto?> FindAsync(int loanId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds a loan by its loan number.
    /// </summary>
    /// <param name="loanNumber">The loan number.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the loan with the specified loan number, or <c>null</c> if no such loan exists.
    /// </returns>
    ValueTask<LoanDto?> FindByNumberAsync(string loanNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the count of all active loans in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> that represents the asynchronous operation.
    /// The task result contains the count of all active loans in the repository.
    /// </returns>
    ValueTask<int> GetActiveLoansCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total debt amount across all active loans in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total debt amount across all active loans in the repository.
    /// </returns>
    ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total capital collected across all loans in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total capital amount collected across all loans in the repository.
    /// </returns>
    ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total amount collected from all loans in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total amount collected from all loans.
    /// </returns>
    ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total current due payment amount
    /// for all loans in the repository as of a specified date.
    /// </summary>
    /// <param name="asOfDate">
    /// The date as of which to calculate the total current due payment amount.
    /// If not specified, the current date is used.
    /// </param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total current due payment amount for all loans as of the specified date.
    /// </returns>
    ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(
        DateTime? asOfDate = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total current pending due payment amount
    /// for all loans in the repository as of a specified date.
    /// </summary>
    /// <param name="asOfDate">The date as of which to calculate the total current pending due payment amount.
    /// If not specified, the current date is used.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total current pending due payment amount for all loans as of the specified date.
    /// </returns>
    ValueTask<decimal> GetTotalCurrentPendingDuePaymentAmountAsync(
        DateTime? asOfDate = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total interest collected from all loans in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total interest collected from all loans in the repository.
    /// </returns>
    ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total amount of loans granted in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total amount of loans granted in the repository.
    /// </returns>
    ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously searches for loans by loan number, account number, or customer document.
    /// </summary>
    /// <param name="searchTerm">The search term to match against loan number, account number, or customer document.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains a collection of loans that match the search criteria.
    /// </returns>
    ValueTask<IEnumerable<LoanSearchResult>> SearchByNumberOrCustomerDocumentAsync(
        string searchTerm,
        CancellationToken cancellationToken = default);
}