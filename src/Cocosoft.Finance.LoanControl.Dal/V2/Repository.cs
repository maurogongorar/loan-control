using Cocosoft.Finance.LoanControl.Dal.Model.V2;

namespace Cocosoft.Finance.LoanControl.Dal.V2;

internal class Repository(LoanDbContext dbContext) : IRepository
{
    /// <inheritdoc />
    public async ValueTask<TDomain> AddAsync<TDomain>(TDomain entity) where TDomain : class
    {
        var entry = await dbContext.AddAsync(entity);
        return entry.Entity;
    }
    
    /// <inheritdoc />
    public TDomain Attach<TDomain>(TDomain entity) where TDomain : class
    {
        var entry = dbContext.Attach(entity);
        return entry.Entity;
    }

    /// <inheritdoc />
    public ValueTask<TDomain?> FindAsync<TDomain>(params object?[]? keyValues)
        where TDomain : class
        => dbContext.FindAsync<TDomain>(keyValues);

    /// <inheritdoc />
    public TDomain Remove<TDomain>(TDomain entity) where TDomain : class
    {
        var entry = dbContext.Remove(entity);
        return entry.Entity;
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);

    /// <inheritdoc />
    public IQueryable<TDomain> Set<TDomain>() where TDomain : class
    {
        return dbContext.Set<TDomain>();
    }
}
