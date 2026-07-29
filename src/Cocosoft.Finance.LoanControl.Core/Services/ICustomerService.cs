using Cocosoft.Finance.LoanControl.Domain.Customers;

namespace Cocosoft.Finance.LoanControl.Core.Services;

public interface ICustomerService
{
    ValueTask<IEnumerable<CustomerSearchResult>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(Guid customerId, CancellationToken cancellationToken = default);
}
