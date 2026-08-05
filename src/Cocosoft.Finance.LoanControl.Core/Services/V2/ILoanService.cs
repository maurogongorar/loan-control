using Cocosoft.Finance.LoanControl.Domain.Loans;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

/// <summary>
/// This interface defines the contract for a loan service that provides various operations related to loans,
/// such as finding loans, retrieving loan statistics, and searching for loans based on specific criteria.
/// </summary>
public interface ILoanService
{
    /// <summary>
    /// Asynchronously finds a loan by its identifier.
    /// </summary>
    /// <param name="loanId">The loan identifier.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the loan with the specified identifier, or <c>null</c> if no such loan exists.
    /// </returns>
    ValueTask<LoanDto?> FindLoanAsync(int loanId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the count of all active loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the count of all active loans.
    /// </returns>
    ValueTask<int> GetActiveLoansCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total debt amount across all active loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total debt amount across all active loans.
    /// </returns>
    ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total capital collected across all loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total capital collected across all loans.
    /// </returns>
    ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total amount collected across all loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total amount collected across all loans.
    /// </returns>
    ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total current due payment amount across all loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total current due payment amount.
    /// </returns>
    ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total current pending payment due amount across all loans.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total current pending payment due amount.
    /// </returns>
    ValueTask<decimal> GetTotalCurrentPendingDuePaymentAmountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total interest collected asynchronous.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total interest collected.
    /// </returns>
    ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously calculates the total amount of loans granted.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the total amount of loans granted.
    /// </returns>
    ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously searches for loans based on the provided search text.
    /// </summary>
    /// <param name="searchText">The text to search for in loans.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="LoanSearchResult"/> that match the search criteria.
    /// </returns>
    ValueTask<IEnumerable<LoanSearchResult>> SearchLoansAsync(string searchText, CancellationToken cancellationToken = default);
}
