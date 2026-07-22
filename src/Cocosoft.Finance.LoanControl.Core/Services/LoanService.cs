using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.Core.Services;

internal class LoanService(IRepository repository) : ILoanService
{
    public IEnumerable<Loan> GetActiveLoans() => repository.GetLoans();

    public IEnumerable<Loan> GetAllLoans() => repository.GetLoans(getAll: true);
}
