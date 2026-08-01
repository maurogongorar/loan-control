using Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport;

internal class DesignMainViewModel() : MainViewModel(new DesignDashboardViewModel(), new DesignLoansViewModel())
{
}
