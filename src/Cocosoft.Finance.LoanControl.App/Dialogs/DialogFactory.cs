using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal class DialogFactory : IDialogFactory
{
    public IAddLoanView CreateAddLoanDialogView() => new AddLoanDialogForm();

    public IAddPaymentView CreateAddPaymentDialogView(Loan loan) => new AddPaymentDialogForm(loan);

    public IViewSelectable CreateSelectLoanDialogView(IRepository repository) => new SelectLoanDialogForm(repository);
}
