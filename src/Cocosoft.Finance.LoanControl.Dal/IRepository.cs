using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using System.Transactions;

namespace Cocosoft.Finance.LoanControl.Dal;

public interface IRepository
{
    Task AddLoanAsync(Loan loan);

    Task AddPaymentAsync(Payment payment);

    ITransaction BeginTransaction();

    Loan GetLoan(int loanId);

    IEnumerable<Payment> GetLoanPayments(int loanId);

    IEnumerable<Loan> GetLoans(bool getAll = false);

    Task UpdateLoanAsync(Loan loan);
}
