using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Dal.Model;

internal class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    public DbSet<Loan> Loans => Set<Loan>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasMany(l => l.Payments)
                  .WithOne(p => p.Loan)
                  .HasForeignKey(p => p.LoanId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
