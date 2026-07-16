using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IAddPaymentView : IDialog
{
    Payment? Payment { get; }
}