using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Cocosoft.Finance.LoanControl.App.Avalonia.Views.Dialogs;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Services;

/// <summary>
/// Provides dialog functionality by displaying Avalonia modal windows. 
/// </summary>
/// <seealso cref="IDialogService" />
public sealed class DialogService : IDialogService
{
    /// <inheritdoc />
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        var dialog = new ConfirmationDialog(title, message);

        if (Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop
            && desktop.MainWindow is not null)
        {
            await dialog.ShowDialog(desktop.MainWindow);
        }

        return dialog.Result;
    }
}
