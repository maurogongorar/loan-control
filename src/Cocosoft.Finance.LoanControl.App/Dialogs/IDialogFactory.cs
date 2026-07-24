using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IDialogFactory
{
    IAddLoanDialog CreateAddLoanDialogView();

    IAddPaymentDialog CreateAddPaymentDialogView(Loan loan);

    ISelectLoanDialog CreateSelectLoanDialogView();
}
