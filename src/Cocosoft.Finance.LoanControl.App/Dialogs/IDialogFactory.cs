using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IDialogFactory
{
    IAddLoanView CreateAddLoanDialogView();

    IAddPaymentView CreateAddPaymentDialogView(Loan loan);

    IViewSelectable CreateSelectLoanDialogView(IRepository repository);
}
