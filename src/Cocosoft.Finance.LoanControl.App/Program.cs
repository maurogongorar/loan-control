using Cocosoft.Finance.LoanControl.App.Dialogs;
using Cocosoft.Finance.LoanControl.Dal.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cocosoft.Finance.LoanControl.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<IDialogFactory, DialogFactory>()
                .AddRepository()
                .AddScoped<MainForm>();

            using var app = builder.Build();
            app.Services.InitializeDbContext();
            Application.Run(app.Services.GetRequiredService<MainForm>());
        }
    }
}