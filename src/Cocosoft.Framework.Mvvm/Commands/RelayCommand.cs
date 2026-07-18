using System.Windows.Input;

namespace Cocosoft.Framework.Mvvm.Commands;

public class RelayCommand(Action execute, Func<bool>? canExecute = null) : IObservableCommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => execute();

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

public class RelayCommand<T>(Action<T> execute, Func<T, bool>? canExecute = null) : IObservableCommand, ICommand<T>
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if (parameter is T tParam)
        {
            return canExecute?.Invoke(tParam) ?? true;
        }

        return false;
    }

    public void Execute(object? parameter)
    {
        if (parameter is T tParam)
        {
            execute(tParam);
        }
    }

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
