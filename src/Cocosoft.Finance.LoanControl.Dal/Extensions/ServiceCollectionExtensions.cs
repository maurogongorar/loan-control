using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
        => services.AddRepository<Model.LoanDbContext, IRepository, Repository>();

    public static IServiceCollection AddRepositoryV2(this IServiceCollection services)
        => services.AddRepository<Model.V2.LoanDbContext, V2.IRepository, V2.Repository>();

    private static IServiceCollection AddRepository<TDbContext, TRepository, TRepositoryImpl>(this IServiceCollection services)
        where TDbContext : DbContext
        where TRepository : class
        where TRepositoryImpl : class, TRepository
    {
        services.AddDbContext<TDbContext>(
            (sp, options) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var defaultDbDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Cocosoft",
                    "LoanControl");
                var dataSource = config["COCOFOST_LOAN_DB_PATH"] ?? Path.Combine(defaultDbDirectory, "loan.dat");
                var directory = Path.GetDirectoryName(dataSource) ?? defaultDbDirectory;

                // check directory exists, if not create it
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                options.UseSqlite($"Data Source={dataSource}");
            });
        return services.AddScoped<TRepository, TRepositoryImpl>();
    }
}
