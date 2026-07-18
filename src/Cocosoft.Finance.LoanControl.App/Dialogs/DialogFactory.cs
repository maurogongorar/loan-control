using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal class DialogFactory(IServiceProvider serviceProvider) : IDialogFactory
{
    public IAddLoanDialog CreateAddLoanDialogView() => serviceProvider.GetRequiredService<IAddLoanDialog>();

    public IAddPaymentDialog CreateAddPaymentDialogView(Loan loan) => new AddPaymentDialogForm(loan);

    public IDialogSelectable CreateSelectLoanDialogView(IRepository repository) => new SelectLoanDialogForm(repository);
}
