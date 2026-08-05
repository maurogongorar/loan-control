using Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport;

internal class DesignCreateCustomerViewModel()
    : CreateCustomerViewModel(
        new DesignDialogService(),
        new DesignCustomerService(),
        new DesignLogger<CreateCustomerViewModel>())
{
}
