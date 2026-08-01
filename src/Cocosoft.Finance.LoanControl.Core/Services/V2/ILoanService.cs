using Cocosoft.Finance.LoanControl.Domain.Loans;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

public interface ILoanService
{
    ValueTask<int> GetActiveLoanCountsAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalCurrentPendingPaymentDueAmountAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default);

    ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<LoanSearchResult>> SearchLoansAsync(string searchText, CancellationToken cancellationToken = default);

    ValueTask<LoanDto?> FindLoanAsync(int loanId, CancellationToken cancellationToken = default);
}
