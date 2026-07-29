using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Views.Dialogs;

internal interface IDialogSelectable<TViewModel> : IDialog, IMvvmViewSelectable<TViewModel>
    where TViewModel : ViewModelBase, IViewModelSelectable
{
}
