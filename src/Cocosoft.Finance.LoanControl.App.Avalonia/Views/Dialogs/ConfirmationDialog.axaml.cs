using Avalonia.Controls;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Views.Dialogs;

/// <summary>
/// A modal dialog window that presents a confirmation prompt with a title, message,
/// and confirm/cancel buttons.
/// </summary>
/// <seealso cref="Avalonia.Controls.Window" />
public partial class ConfirmationDialog : Window
{
    /// <summary>
    /// Gets a value indicating whether the user confirmed the dialog.
    /// </summary>
    public bool Result { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmationDialog"/> class.
    /// </summary>
    public ConfirmationDialog()
    {
        this.InitializeComponent();

        var confirmButton = this.FindControl<Button>("ConfirmButton")!;
        var cancelButton = this.FindControl<Button>("CancelButton")!;

        confirmButton.Click += (_, _) =>
        {
            this.Result = true;
            this.Close();
        };

        cancelButton.Click += (_, _) =>
        {
            this.Result = false;
            this.Close();
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmationDialog"/> class
    /// with the specified title and message.
    /// </summary>
    /// <param name="title">The dialog title.</param>
    /// <param name="message">The message to display.</param>
    public ConfirmationDialog(string title, string message) : this()
    {
        this.FindControl<TextBlock>("TitleText")!.Text = title;
        this.FindControl<TextBlock>("MessageText")!.Text = message;
    }
}
