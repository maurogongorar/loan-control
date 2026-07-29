using Cocosoft.Finance.LoanControl.Dal.V2;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Core.Services;

internal class CustomerService(IRepository repository) : ICustomerService
{
    /// <inheritdoc />
    public ValueTask<IEnumerable<CustomerSearchResult>> SearchByDocumentAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<CustomerSearchResult>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await repository.Set<CustomerDto>().Where(c => c.IdentificationNumber.StartsWith(searchTerm)).Select(p => new CustomerSearchResult()).ToListAsync(cancellationToken);
    }
}
