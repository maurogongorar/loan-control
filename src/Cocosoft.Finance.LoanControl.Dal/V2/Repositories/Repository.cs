using System.Data;
using Cocosoft.Finance.LoanControl.Dal.Model.V2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

internal class Repository(LoanDbContext dbContext) : IRepository
{
    /// <inheritdoc />
    public async ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var entry = await dbContext.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    /// <inheritdoc />
    public TEntity Attach<TEntity>(TEntity entity) where TEntity : class
    {
        var entry = dbContext.Attach(entity);
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await dbContext.Database.BeginTransactionAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        => await dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    /// <inheritdoc />
    public ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken = default)
        where TEntity : class
        => dbContext.FindAsync<TEntity>(keyValues, cancellationToken);

    /// <inheritdoc />
    public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
    {
        var entry = dbContext.Remove(entity);
        return entry.Entity;
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);

    /// <inheritdoc />
    public IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return dbContext.Set<TEntity>();
    }
}
