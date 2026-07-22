using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.Core.Services;

public interface ILoanService
{
    IEnumerable<Loan> GetActiveLoans();

    IEnumerable<Loan> GetAllLoans();
}
