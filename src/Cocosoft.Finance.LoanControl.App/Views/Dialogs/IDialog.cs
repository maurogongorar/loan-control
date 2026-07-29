namespace Cocosoft.Finance.LoanControl.App.Views.Dialogs;

internal interface IDialog : IDisposable
{
    DialogResult ShowDialog(IWin32Window? owner);
}
