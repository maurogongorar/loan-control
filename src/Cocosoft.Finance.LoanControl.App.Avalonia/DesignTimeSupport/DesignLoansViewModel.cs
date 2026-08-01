using Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport;

internal class DesignLoansViewModel() : LoansViewModel(
    new DesignDialogService(),
    new DesignLoanService(),
    new DesignCreateLoanViewModel(),
    new DesignLogger<LoansViewModel>())
{
}
