using Cocosoft.Finance.LoanControl.Dal.V2.Repositories;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using Microsoft.Extensions.Caching.Memory;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

/// <summary>
/// Th
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
    public async ValueTask<int> GetActiveLoanCountsAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.active_loan_counts", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetActiveLoanCountsAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_debt", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalActiveDebtAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_capital_collected", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCapitalCollectedAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_collected", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCollectedAsync(cancellationToken);
        });
        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_current_due_payment_amount", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalCurrentDuePaymentAmountAsync(cancellationToken: cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentPendingPaymentDueAmountAsync(
        CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync(
            "core.services.loan.total_current_pending_payment_due_amount",
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
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_interest_collected", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.GetTotalInterestCollectedAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default)
    {
        var cached = await cache.GetOrCreateAsync("core.services.loan.total_loans_granted", async entry =>
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
