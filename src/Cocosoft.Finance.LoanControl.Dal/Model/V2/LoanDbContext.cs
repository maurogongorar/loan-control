using Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2;

/// <summary>
/// The LoanDbContext class represents the database context for the loan control system.
/// It is responsible for managing the entity sets and their relationships, as well as
/// configuring the database schema using the Entity Framework Core ORM.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
internal class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    private static void AuditColumns(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Creation date
            modelBuilder.Entity(entityType.ClrType)
                .Property<DateTime>("CreatedAt")
                .HasColumnName("CreatedAt");

            // Creation user
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>("CreatedBy")
                .HasMaxLength(50)
                .HasColumnName("CreatedBy");

            // Modification date (allows null for entities that have not been modified)
            modelBuilder.Entity(entityType.ClrType)
                .Property<DateTime?>("UpdatedAt")
                .HasColumnName("UpdatedAt");

            // Modification user
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>("UpdatedBy")
                .HasMaxLength(50)
                .HasColumnName("UpdatedBy");
        }
    }

    private void ApplyAuditInformation()
    {
        // currently hardcoded, but in a real application, you would get the current user from the context
        var currentUser = "user1";

        // We are only interested in entities that are being Inserted or Updated
        var entries = this.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                // Assign creation values
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("CreatedBy").CurrentValue = currentUser;
            }
            else if (entry.State == EntityState.Modified)
            {
                // Assign modification values
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("UpdatedBy").CurrentValue = currentUser;

                // Security measure: Prevent someone from modifying the original creation data
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("CreatedBy").IsModified = false;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasMany(l => l.Payments)
                  .WithOne(p => p.Loan)
                  .HasForeignKey(p => new { p.LoanId, p.LoanVersion })
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.Customer)
                  .WithMany(c => c.Loans)
                  .HasForeignKey(l => new { l.CustomerId, l.CustomerVersion })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(a => a.Customer)
                  .WithMany(c => c.Accounts)
                  .HasForeignKey(a => new { a.CustomerId, a.CustomerVersion })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        AuditColumns(modelBuilder);
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        ApplyAuditInformation();
        return base.SaveChanges();
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }
}
