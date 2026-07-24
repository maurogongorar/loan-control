namespace Cocosoft.Framework.Mvvm;

/// <summary>
/// The interface IMvvmView represents a view in the Model-View-ViewModel (MVVM) pattern that is associated with a
/// specific view model of type TViewModel.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="System.IDisposable" />
public interface IMvvmView<TViewModel> : IDisposable where TViewModel : ViewModelBase
{
    /// <summary>
    /// Gets the view model associated with the view.
    /// </summary>
    /// <value>
    /// The view model of type TViewModel that is associated with the view.
    /// </value>
    TViewModel ViewModel { get; }
}
