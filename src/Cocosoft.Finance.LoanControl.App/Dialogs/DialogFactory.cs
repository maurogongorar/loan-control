using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal class DialogFactory(IServiceProvider serviceProvider) : IDialogFactory
{
    public IAddLoanDialog CreateAddLoanDialogView() => serviceProvider.GetRequiredService<IAddLoanDialog>();

    public IAddPaymentDialog CreateAddPaymentDialogView(Loan loan)
    {
        var dialog = serviceProvider.GetRequiredService<IAddPaymentDialog>();
        dialog.ViewModel.SetLoan(loan);
        return dialog;
    }

    public ISelectLoanDialog CreateSelectLoanDialogView() => serviceProvider.GetRequiredService<ISelectLoanDialog>();
}
