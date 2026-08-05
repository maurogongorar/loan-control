using CommunityToolkit.Mvvm.ComponentModel;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

/// <summary>
/// Base class for all view models in the application, providing observable property change support
/// and data validation via <see cref="ObservableValidator"/>.
/// </summary>
/// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableValidator" />
public abstract class ViewModelBase : ObservableValidator
{
    /// <summary>
    /// Resets the view model to its initial state.
    /// </summary>
    public virtual void Reset()
    {
    }
}
