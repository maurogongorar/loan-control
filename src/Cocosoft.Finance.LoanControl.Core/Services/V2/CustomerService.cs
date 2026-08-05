using Cocosoft.Finance.LoanControl.Dal.V2.Repositories;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

/// <summary>
/// The <c>CustomerService</c> class provides methods for managing customers, including adding, retrieving, counting, and searching customer records.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.Core.Services.V2.ICustomerService" />
internal class CustomerService(ICustomerRepository repository, IMemoryCache cache, ILogger<CustomerService> logger)
    : ICustomerService
{
    /// <inheritdoc />
    public async ValueTask<CustomerDto?> AddCustomerAsync(
        CustomerDto customer,
        CancellationToken cancellationToken = default)
    {
        //TODO: Implement properties validation and business rules

        try
        {
            var newly = await repository.AddAsync(customer, cancellationToken);

            // Invalidate cache entries related to customer counts
            cache.Remove(CacheEntryKeys.TotalCustomers);
            cache.Remove(CacheEntryKeys.NewCustomers);
            return newly;
        }
        catch (Exception ex)
        {
            //TODO: Implement proper exception handling and logging
            logger.LogError(ex, "An error occurred while adding a new customer.");
            return null;
        }
    }

    /// <inheritdoc />
    public async ValueTask<CustomerDto?> FindCustomerAsync(int id, CancellationToken cancellationToken)
    {
        return await repository.FindByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<int> GetCustomersCountAsync(CancellationToken cancellationToken)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.TotalCustomers, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.CountCustomersAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<int> GetCustomersWithActiveLoanCountAsync(CancellationToken cancellationToken)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.CustomersWithLoan, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.CountCustomersWithLoanAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<int> GetNewCustomersCountAsync(CancellationToken cancellationToken)
    {
        var cached = await cache.GetOrCreateAsync(CacheEntryKeys.NewCustomers, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await repository.CountNewCustomersAsync(cancellationToken);
        });

        return cached;
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(
        string documentNumber,
        CancellationToken cancellationToken = default)
    {
        return await repository.FindByIDocumentAsync(documentNumber, cancellationToken);
    }
}
