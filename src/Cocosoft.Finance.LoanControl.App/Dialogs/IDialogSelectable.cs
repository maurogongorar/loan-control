using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IDialogSelectable<TViewModel> : IDialog, IMvvmViewSelectable<TViewModel>
    where TViewModel : ViewModelBase, IViewModelSelectable
{
}
