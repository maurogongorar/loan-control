using Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport;

internal class DesignCustomersViewModel()
    : CustomersViewModel(
        new DesignDialogService(),
        new DesignCustomerService(),
        new DesignCreateCustomerViewModel(),
        new DesignLogger<CustomersViewModel>())
{
}
