using Cocosoft.Finance.LoanControl.Core.Services.V2;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;

internal class DesignLoanService : ILoanService
{
    private const int SimulatedDelayMilliseconds = 0;

    /// <inheritdoc />
    public async ValueTask<int> GetActiveLoansCountAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 12;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCollectedAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 87_500_000m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentDuePaymentAmountAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 12_000_000m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCurrentPendingDuePaymentAmountAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 4_800_000m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalActiveDebtAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 485_000m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalLoansGrantedAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 150_000_000m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalInterestCollectedAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 62_300m;
    }

    /// <inheritdoc />
    public async ValueTask<decimal> GetTotalCapitalCollectedAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 52_500_000m;
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<LoanSearchResult>> SearchLoansAsync(
        string searchText,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return [
            new LoanSearchResult
            {
                LoanNumber = "CR-001",
                DebtorName = "Juan Pérez",
                LoanAmount = 50_000m,
                InstallmentAmount = 4_800m,
                CurrentDebt = 38_200m,
                LastPaymentDate = new DateTime(2025, 6, 15)
            },
            new LoanSearchResult
            {
                LoanNumber = "CR-002",
                DebtorName = "María López",
                LoanAmount = 120_000m,
                InstallmentAmount = 11_500m,
                CurrentDebt = 95_000m,
                LastPaymentDate = new DateTime(2025, 6, 20)
            }];
    }

    /// <inheritdoc />
    public async ValueTask<LoanDto?> FindLoanAsync(int loanId, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return new LoanDto
        {
            LoanNumber = "CR-001",
            DebtorName = "Juan Pérez",
            DebtorIdentification = "ID-123456789",
            DisbursementDate = new DateTime(2025, 1, 15),
            InitialAmount = 50_000m,
            InstallmentAmount = 4_800m,
            TermMonths = 12,
            AnnualInterestRate = 18m,
            MonthlyInterestRate = 1.5m,
            PaidInstallments = 3,
            TotalInstallments = 12,
            TotalInterestPaid = 2_065m,
            CurrentDebt = 38_200m,
            Payments = [
                new()
                {
                    PaymentDate = new DateTime(2025, 4, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 750m,
                    PrincipalPaid = 4_050m
                },
                new()
                {
                    PaymentDate = new DateTime(2025, 5, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 690m,
                    PrincipalPaid = 4_110m
                },
                new()
                {
                    PaymentDate = new DateTime(2025, 6, 15),
                    PaymentAmount = 4_800m,
                    InterestPaid = 625m,
                    PrincipalPaid = 4_175m
                }
            ]
        };
    }
}
