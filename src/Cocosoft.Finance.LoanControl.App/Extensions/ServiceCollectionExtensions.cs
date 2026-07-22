using Cocosoft.Finance.LoanControl.App.Dialogs;
using Cocosoft.Finance.LoanControl.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.App.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDialogs(this IServiceCollection services)
    {
        services.AddSingleton<IDialogFactory, DialogFactory>();
        services.AddTransient<IAddLoanDialog, AddLoanDialogForm>()
            .AddTransient<AddLoanViewModel>();
        services.AddTransient<IAddPaymentDialog, AddPaymentDialogForm>()
            .AddTransient<AddPaymentViewModel>();
        services.AddTransient<ISelectLoanDialog, SelectLoanDialogForm>()
            .AddTransient<SelectLoanViewModel>();
        return services;
    }
}
