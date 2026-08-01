using Cocosoft.Finance.LoanControl.Core.Resources;
using Cocosoft.Finance.LoanControl.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration<TEntryPoint>(this IServiceCollection services)
        where TEntryPoint : class
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        var confBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<TEntryPoint>()
            .AddEnvironmentVariables();
        return services.AddSingleton<IConfiguration>(confBuilder.Build());
    }

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services.AddLocalization()
            .AddSingleton<ILocalizationService, LocalizationService>()
            .AddSingleton<ILoanService, LoanService>();
    }

    public static IServiceCollection AddCoreServicesV2(this IServiceCollection services)
    {
        return services.AddLocalization()
            .AddSingleton<ILocalizationService, LocalizationService>()
            .AddSingleton<Services.V2.ILoanService, Services.V2.LoanService>()
            .AddSingleton<Services.V2.ICustomerService, Services.V2.CustomerService>();
    }
}
