using Cocosoft.Finance.LoanControl.Dal.Model;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.Dal;

internal class Repository(LoanDbContext dbContext) : IRepository
{
    public async Task AddLoanAsync(Loan loan)
    {
        await dbContext.Loans.AddAsync(loan);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await dbContext.Payments.AddAsync(payment);
        await dbContext.SaveChangesAsync();
    }   

    public ITransaction BeginTransaction() => new Transaction();

    public Loan GetLoan(int loanId) => dbContext.Loans.First(p => p.Id == loanId);

    public IEnumerable<Payment> GetLoanPayments(int loanId)
        => dbContext.Payments.Where(p => p.LoanId == loanId).OrderByDescending(p => p.Date).AsEnumerable();

    public IEnumerable<Loan> GetLoans(bool getAll = false)
        => dbContext.Loans.Where(p => getAll || !p.IsClosed).AsEnumerable();

    public async Task UpdateLoanAsync(Loan loan)
    {
        dbContext.Attach(loan);
        await dbContext.SaveChangesAsync();
    }
}
