using Cocosoft.Finance.LoanControl.Dal.Model.V2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cocosoft.Finance.LoanControl.Dal.V2;

/// <summary>
/// This class is a factory for creating instances of the LoanDbContext at design time.
/// It implements the IDesignTimeDbContextFactory interface, which allows for the creation of a DbContext instance
/// with specific options, such as the database provider and connection string.
/// This is particularly useful for scenarios like migrations, where a DbContext instance is needed without having to
/// run the application.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory&lt;Cocosoft.Finance.LoanControl.Dal.Model.V2.LoanDbContext&gt;" />
internal class DesignTimeLoanDbContextFactory : IDesignTimeDbContextFactory<LoanDbContext>
{
    /// <inheritdoc />
    public LoanDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<LoanDbContext>();
        builder.UseSqlite("Data Source=.\\loan.dat");

        return new LoanDbContext(builder.Options);
    }
}
