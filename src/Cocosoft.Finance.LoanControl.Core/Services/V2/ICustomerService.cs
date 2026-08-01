using Cocosoft.Finance.LoanControl.Domain.Customers;

namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

public interface ICustomerService
{
    ValueTask<CustomerDto?> AddCustomerAsync(CustomerDto customer, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(
        string documentNumber,
        CancellationToken cancellationToken = default);
}
