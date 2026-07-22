using Cocosoft.Finance.LoanControl.Core.Resources;
using Cocosoft.Finance.LoanControl.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services.AddLocalization()
            .AddSingleton<ILocalizationService, LocalizationService>()
            .AddSingleton<ILoanService, LoanService>();
    }
}
