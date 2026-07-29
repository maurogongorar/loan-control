using Cocosoft.Finance.LoanControl.Dal.Model.V2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cocosoft.Finance.LoanControl.Dal.V2;

internal class DesignTimeLoanDbContextFactory : IDesignTimeDbContextFactory<LoanDbContext>
{
    public LoanDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<LoanDbContext>();
        builder.UseSqlite("Data Source=.\\loan.dat");

        return new LoanDbContext(builder.Options);
    }
}
