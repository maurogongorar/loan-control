using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

/// <summary>
/// The <see cref="IRepository"/> interface defines a contract for a repository that provides basic CRUD operations for entities in a data store.
/// </summary>
internal interface IRepository
{
    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the added entity.
    /// </returns>
    ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Attaches an existing entity to the repository, allowing it to be tracked for changes.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to attach.</param>
    /// <returns>The attached entity.</returns>
    TEntity Attach<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the transaction.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <param name="isolationLevel">The isolation level for the transaction.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the transaction.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously finds an entity with the specified primary key values.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="keyValues">The primary key values of the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> that represents the asynchronous operation.
    /// The task result contains the found entity, or <c>null</c> if no entity is found.
    /// </returns>
    ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Marks the entity as deleted so it will be removed from the repository upon saving changes.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>The removed entity.</returns>
    TEntity Remove<TEntity>(TEntity entity) where TEntity : class;

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
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>A queryable set of entities.</returns>
    IQueryable<TEntity> Set<TEntity>() where TEntity : class;
}
