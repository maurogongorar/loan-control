using AutoMapper;
using Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

/// <summary>
/// The <see cref="LoanRepository"/> class is responsible for managing loan data in the database.
/// It provides methods to add, update, delete, and retrieve loan information.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.Dal.V2.Repositories.ILoanRepository" />
internal class LoanRepository(IRepository repository, IMapper mapper) : ILoanRepository
{
    /// <inheritdoc />
    public async ValueTask<LoanDto> AddAsync(LoanDto loan, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Loan>(loan);
        entity.Version = 1;
        var newly = await repository.AddAsync(entity, cancellationToken);
        return mapper.Map<LoanDto>(newly);
    }

    /// <inheritdoc />
    public async ValueTask<LoanDto?> FindAsync(int loanId, CancellationToken cancellationToken = default)
    {
        var loan = await repository.Set<Loan>()
            .FirstOrDefaultAsync(l => l.Id == loanId && l.IsCurrent && !l.IsClosed, cancellationToken);
        return mapper.Map<LoanDto?>(loan);
    }

    /// <inheritdoc />
    public async ValueTask<LoanDto?> FindByNumberAsync(
        string loanNumber,
        CancellationToken cancellationToken = default)
    {
        var loan = await repository.Set<Loan>()
            .FirstOrDefaultAsync(l => l.LoanNumber == loanNumber && l.IsCurrent && !l.IsClosed, cancellationToken);
        return mapper.Map<LoanDto?>(loan);
    }

    /// <inheritdoc />
    public async ValueTask<int> GetActiveLoansCountAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .CountAsync(l => l.IsCurrent && !l.IsClosed, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .Where(l => l.IsCurrent && !l.IsClosed)
            .SumAsync(l => l.CurrentBalance, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .Where(l => l.IsCurrent)
            .SelectMany(l => l.Payments)
            .Where(p => p.IsCurrent && !p.IsDeleted)
            .SumAsync(p => p.Capital, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .Where(l => l.IsCurrent)
            .SelectMany(l => l.Payments)
            .Where(p => p.IsCurrent && !p.IsDeleted)
            .SumAsync(p => p.Amount, cancellationToken);
    }

    public async ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(
        DateTime? asOfDate = default,
        CancellationToken cancellationToken = default)
    {
        asOfDate ??= DateTime.UtcNow;
        return await repository.Set<Loan>()
            .Include(l => l.Payments.Where(p => p.IsCurrent && !p.IsDeleted && p.Date.Month >= asOfDate.Value.Month))
            .Where(l => l.IsCurrent && !l.IsClosed)
            .AsAsyncEnumerable()
            .Where(l => l.Payments.Count > 0)
            .Select(l => l.Payments.Sum(p => p.Amount))
            .SumAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentPendingDuePaymentAmountAsync(
        DateTime? asOfDate = default,
        CancellationToken cancellationToken = default)
    {
        asOfDate ??= DateTime.UtcNow;
        return await repository.Set<Loan>()
            .Include(l => l.Payments.Where(p => p.IsCurrent && !p.IsDeleted && p.Date.Month >= asOfDate.Value.Month))
            .Where(l => l.IsCurrent && !l.IsClosed)
            .AsAsyncEnumerable()
            .Where(l => l.Payments.Count == 0)
            .Select(l => l.Fee)
            .SumAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .Where(l => l.IsCurrent)
            .SelectMany(l => l.Payments)
            .Where(p => p.IsCurrent && !p.IsDeleted)
            .SumAsync(p => p.Interest, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Set<Loan>()
            .Where(l => l.IsCurrent)
            .SumAsync(l => l.InitialAmount, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<LoanSearchResult>> SearchByNumberOrCustomerDocumentAsync(
        string searchTerm,
        CancellationToken cancellationToken = default)
    {
        var loans = await repository.Set<Loan>()
            .Where(l => l.IsCurrent && !l.IsClosed && (l.LoanNumber.StartsWith(searchTerm)
                || (l.Customer != null && l.Customer.IdentificationNumber.StartsWith(searchTerm))))
            .Select(l => mapper.Map<LoanSearchResult>(l))
            .ToListAsync(cancellationToken);
        return loans;
    }
}
