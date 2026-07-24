using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;
using Cocosoft.Finance.LoanControl.App.Avalonia.Views;
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
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<LoansViewModel>();
            services.AddSingleton<MainViewModel>();

            var provider = services.BuildServiceProvider();

            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}