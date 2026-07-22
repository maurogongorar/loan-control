namespace Cocosoft.Framework.Mvvm;

public interface IMvvmView<TViewModel> : IDisposable where TViewModel : ViewModelBase
{
    TViewModel ViewModel { get; }
}
