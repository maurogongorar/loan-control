using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;
using Cocosoft.Finance.LoanControl.App.Avalonia.Views;
using Cocosoft.Finance.LoanControl.App.Avalonia.Views.Dialogs;
using Cocosoft.Finance.LoanControl.Core.Extensions;
using Cocosoft.Finance.LoanControl.Dal.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.App.Avalonia;

/// <summary>
/// Application entry point that configures dependency injection and initializes the main window.
/// </summary>
/// <seealso cref="Avalonia.Application" />
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();

            // Register configuration and settings for dependency injection
            services.AddConfiguration<Program>();

            // Register presentation services and view models for dependency injection
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<LoansViewModel>()
                .AddSingleton<CreateLoanViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<ConfirmationDialog>();

            // Register infrastructure services for dependency injection
            services.AddRepositoryV2();

            var provider = services.BuildServiceProvider();

            // Migrate database
            provider.MigrateDatabase();


            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}