using System.Windows.Input;

namespace Cocosoft.Framework.Mvvm.Commands;

public interface IObservableCommand : ICommand
{
    void RaiseCanExecuteChanged();
}
