using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Services;

/// <summary>
/// Defines methods for displaying dialog windows to the user.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows a confirmation dialog with the specified title and message.
    /// </summary>
    /// <param name="title">The dialog title.</param>
    /// <param name="message">The message to display.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> that represents the asynchronous operation.
    /// The task result is <see langword="true"/> if the user confirmed; otherwise, <see langword="false"/>.
    /// </returns>
    Task<bool> ShowConfirmationAsync(string title, string message);
}
