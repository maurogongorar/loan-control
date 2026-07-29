using Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2;

internal class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasMany(l => l.Payments)
                  .WithOne(p => p.Loan)
                  .HasForeignKey(p => new { p.LoanId, p.LoanVersion })
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.Account)
                  .WithMany(a => a.Loans)
                  .HasForeignKey(l => new { l.AccountId, l.AccountVersion })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(a => a.Customer)
                  .WithMany(c => c.Accounts)
                  .HasForeignKey(a => new { a.CustomerId, a.CustomerVersion })
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
