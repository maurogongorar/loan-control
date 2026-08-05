using Cocosoft.Finance.LoanControl.Dal.V2.Repositories;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using Microsoft.Extensions.Caching.Memory;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

/// <summary>
/// The <c>LoanService</c> class provides methods for managing loans, including retrieval, counting, and searching of loan records.
/// It utilizes caching to improve performance for frequently accessed data.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.Core.Services.V2.ILoanService" />
internal class LoanService(ILoanRepository repository, IMemoryCache cache) : ILoanService
{
    /// <inheritdoc />
    public async ValueTask<LoanDto?> FindLoanAsync(int loanId, CancellationToken cancellationToken = default)
    {
        return await repository.FindAsync(loanId, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<int> GetActiveLoansCountAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.ActiveLoans, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetActiveLoansCountAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.TotalDebt, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalActiveDebtAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.CapitalCollected, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCapitalCollectedAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.TotalCollected, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCollectedAsync(cancellationToken);
        });
        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.CurrentDuePaymentAmount, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCurrentDuePaymentAmountAsync(cancellationToken: cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentPendingDuePaymentAmountAsync(
        CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(
            CacheEntryKeys.CurrentPendingDuePaymentAmount,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await repository.GetTotalCurrentPendingDuePaymentAmountAsync(
                    cancellationToken: cancellationToken);
            });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.TotalInterestCollected, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalInterestCollectedAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.TotalLoansGranted, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalLoansGrantedAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<LoanSearchResult>> SearchLoansAsync(
        string searchText,
        CancellationToken cancellationToken = default)
    {
        return await repository.SearchByNumberOrCustomerDocumentAsync(searchText, cancellationToken);
    }
}
