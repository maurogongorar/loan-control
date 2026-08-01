using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;

internal class DesignDialogService : IDialogService
{
    public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(true);
}
