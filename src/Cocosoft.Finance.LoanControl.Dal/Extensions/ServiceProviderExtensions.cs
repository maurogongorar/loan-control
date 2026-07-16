using Cocosoft.Finance.LoanControl.Dal.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.Dal.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceProvider InitializeDbContext(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<LoanDbContext>();
        dbContext.Database.EnsureCreated();
        return serviceProvider;
    }
}
