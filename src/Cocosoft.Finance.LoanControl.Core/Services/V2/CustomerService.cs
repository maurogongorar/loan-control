using Cocosoft.Finance.LoanControl.Dal.V2.Repositories;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using Microsoft.Extensions.Logging;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

internal class CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger) : ICustomerService
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
    public ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(
        string documentNumber,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
