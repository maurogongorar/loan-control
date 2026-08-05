using Cocosoft.Finance.LoanControl.Core.Services.V2;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;

internal class DesignCustomerService : ICustomerService
{
    private const int SimulatedDelayMilliseconds = 0;

    /// <inheritdoc />
    public async ValueTask<CustomerDto?> AddCustomerAsync(CustomerDto customer, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return customer;
    }

    /// <inheritdoc />
    public async ValueTask<CustomerDto?> FindCustomerAsync(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return new CustomerDto {
            Id = id,
            FirstName = "Design Time Customer",
            LastName = "Customer",
            Address = "123 Design St.",
            City = "Design City",
            IdentificationNumber = "123456789",
            PhoneNumber = "123-456-7890"
        };
    }

    /// <inheritdoc />
    public async ValueTask<int> GetCustomersCountAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 27;
    }

    /// <inheritdoc />
    public async ValueTask<int> GetCustomersWithActiveLoanCountAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 12;
    }

    /// <inheritdoc />
    public async ValueTask<int> GetNewCustomersCountAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return 6;
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedDelayMilliseconds, cancellationToken); // Simulate some async work
        return [
            new CustomerSearchResult
            {
                DocumentNumber = "12345",
                Name = "Juan Pérez",
                Id = 1
            },
            new CustomerSearchResult
            {
                DocumentNumber = "67890",
                Name = "María López",
                Id = 2
            }];
    }
}
