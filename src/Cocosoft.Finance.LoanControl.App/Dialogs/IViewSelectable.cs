namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IViewSelectable : IDialog
{
    int? SelectedId { get; }
}
