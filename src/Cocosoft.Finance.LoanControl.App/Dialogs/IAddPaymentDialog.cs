using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IAddPaymentDialog : IDialog
{
    Payment? Payment { get; }
}