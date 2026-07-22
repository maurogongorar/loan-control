namespace Cocosoft.Framework.Mvvm;

public interface IMvvmViewSelectable<TViewModel> : IMvvmView<TViewModel> where TViewModel : ViewModelBase, IViewModelSelectable
{
}
