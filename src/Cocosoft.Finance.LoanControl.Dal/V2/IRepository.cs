namespace Cocosoft.Finance.LoanControl.Dal.V2;

/// <summary>
/// The <see cref="IRepository"/> interface defines a contract for a repository that provides basic CRUD operations for entities in a data store.
/// </summary>
public interface IRepository
{
    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the added entity.
    /// </returns>
    ValueTask<TDomain> AddAsync<TDomain>(TDomain entity) where TDomain : class;

    /// <summary>
    /// Attaches an existing entity to the repository, allowing it to be tracked for changes.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    /// <param name="entity">The entity to attach.</param>
    /// <returns>The attached entity.</returns>
    TDomain Attach<TDomain>(TDomain entity) where TDomain : class;

    /// <summary>
    /// Asynchronously finds an entity with the specified primary key values.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    /// <param name="keyValues">The primary key values of the entity.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the found entity, or <c>null</c> if no entity is found.
    /// </returns>
    ValueTask<TDomain?> FindAsync<TDomain>(params object?[]? keyValues) where TDomain : class;

    /// <summary>
    /// Marks the entity as deleted so it will be removed from the repository upon saving changes.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>The removed entity.</returns>
    TDomain Remove<TDomain>(TDomain entity) where TDomain : class;

    /// <summary>
    /// Asynchronously saves all changes made in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a queryable set of entities of the specified type.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    /// <returns>A queryable set of entities.</returns>
    IQueryable<TDomain> Set<TDomain>() where TDomain : class;
}
