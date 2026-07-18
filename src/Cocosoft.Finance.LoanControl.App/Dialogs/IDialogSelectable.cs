namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IDialogSelectable : IDialog
{
    int? SelectedId { get; }
}
