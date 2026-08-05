using AutoMapper;
using Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Cocosoft.Finance.LoanControl.Dal.V2.Repositories;

/// <summary>
/// The <see cref="CustomerRepository"/> class is responsible for managing customer data in the database.
/// It provides methods to add, update, delete, and retrieve customer information.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.Dal.V2.Repositories.ICustomerRepository" />
internal class CustomerRepository(IRepository repository, IMapper mapper) : ICustomerRepository
{
    ///<inheritdoc />
    public async ValueTask<CustomerDto> AddAsync(CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        using var transaction = await repository.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
        var entity = mapper.Map<Customer>(customerDto);
        var maxId = await repository.Set<Customer>()
            .MaxAsync(c => (int?)c.Id, cancellationToken) ?? 0;
        entity.Id = maxId + 1;
        entity.Version = 1;
        var newly = await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return mapper.Map<CustomerDto>(newly);
    }

    ///<inheritdoc />
    public async ValueTask<int> CountCustomersAsync(CancellationToken cancellationToken)
        => await repository.Set<Customer>()
            .CountAsync(c => c.IsCurrent && !c.IsDeleted, cancellationToken);

    ///<inheritdoc />
    public async ValueTask<int> CountCustomersWithLoanAsync(CancellationToken cancellationToken)
        => await repository.Set<Customer>()
            .CountAsync(c => c.IsCurrent && !c.IsDeleted && c.Loans.Any(l => l.IsCurrent && !l.IsClosed), cancellationToken);

    ///<inheritdoc />
    public async ValueTask<int> CountNewCustomersAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var startDate = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        return await repository.Set<Customer>()
            .CountAsync(c => c.IsCurrent && !c.IsDeleted && EF.Property<DateTime>(c, "CreatedAt") >= startDate, cancellationToken);
    }

    ///<inheritdoc />
    public async ValueTask<bool> DeleteAsync(int customerId, CancellationToken cancellationToken = default)
    {
        var current = await repository.Set<Customer>()
            .FirstOrDefaultAsync(c => c.Id == customerId && c.IsCurrent && !c.IsDeleted, cancellationToken);

        if (current != null)
        {
            var deleted = mapper.Map<Customer>(current);
            deleted.Version++;
            deleted.IsDeleted = true;
            current.IsCurrent = false;

            await repository.AddAsync(deleted, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }

    ///<inheritdoc />
    public async ValueTask<CustomerDto?> FindByIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        var customer = await repository.Set<Customer>()
            .FirstOrDefaultAsync(c => c.Id == customerId && c.IsCurrent && !c.IsDeleted, cancellationToken);
        return customer != null ? mapper.Map<CustomerDto>(customer) : null;
    }

    ///<inheritdoc />
    public async ValueTask<IEnumerable<CustomerSearchResult>> FindByIDocumentAsync(
        string documentNumber,
        CancellationToken cancellationToken = default)
    {
        var customers = await repository.Set<Customer>()
            .Where(c => c.IdentificationNumber.StartsWith(documentNumber) && c.IsCurrent && !c.IsDeleted)
            .Select(c => new CustomerSearchResult
            {
                Id = c.Id,
                Name = $"{c.Name} {c.Surname}",
                DocumentNumber = c.IdentificationNumber
            })
            .ToListAsync(cancellationToken);

        return customers;
    }

    ///<inheritdoc />
    public async ValueTask<CustomerDto> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        var current = repository.Set<Customer>()
            .FirstOrDefault(c => c.Id == customerDto.Id && c.IsCurrent && !c.IsDeleted)
            ?? throw new InvalidOperationException($"Customer with ID {customerDto.Id} not found or is deleted.");

        var updated = mapper.Map<Customer>(customerDto);
        updated.Version++;
        current.IsCurrent = false;
        await repository.AddAsync(updated, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return mapper.Map<CustomerDto>(updated);
    }
}
