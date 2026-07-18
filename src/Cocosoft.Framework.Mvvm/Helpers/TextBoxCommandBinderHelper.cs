using Cocosoft.Framework.Mvvm.Commands;
using System.Globalization;

namespace Cocosoft.Framework.Mvvm.Helpers;

public static class TextBoxCommandBinderHelper
{
    public static void BindKeyPressCommand(this TextBox txtBox, ICommand<(object, KeyPressEventArgs)> command)
    {
        txtBox.KeyPress += (sender, e) =>
        {
            if (command.CanExecute((sender, e)))
            {
                command.Execute((sender, e));
            }
        };
    }

    public static ICommand<(object, KeyPressEventArgs)> CreateValidateNumericCommand()
        => new RelayCommand<(object sender, KeyPressEventArgs e)>(
            execute: arg =>
            {
                if (arg.sender is not TextBox txtBox)
                {
                    return;
                }

                // Allows digits, control characters (like backspace)
                if (char.IsControl(arg.e.KeyChar) || char.IsDigit(arg.e.KeyChar))
                {
                    return;
                }

                // Allows decimal separator if the TextBox is not tagged as "integer" and it doesn't already contain
                // a decimal separator
                if (txtBox.Tag?.ToString()?.Equals("integer") != true &&
                arg.e.KeyChar == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] &&
                !txtBox.Text.Contains(arg.e.KeyChar))
                {
                    return;
                }

                arg.e.Handled = true; // Ignora la tecla presionada
            });
}
