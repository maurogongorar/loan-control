using Cocosoft.Finance.LoanControl.Dal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.Dal.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceProvider InitializeDbContext(this IServiceProvider serviceProvider)
    {
        using var dbContext = serviceProvider.GetRequiredService<LoanDbContext>();
        dbContext.Database.EnsureCreated();
        return serviceProvider;
    }

    public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<Model.V2.LoanDbContext>();
            dbContext.Database.Migrate();
            return serviceProvider;

        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            throw;
        }
    }
}
