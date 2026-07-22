using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

internal interface IDialog : IDisposable
{
    DialogResult ShowDialog(IWin32Window? owner);
}
