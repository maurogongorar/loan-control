using System;
using System.Globalization;
using System.Text;
using Avalonia;
using Avalonia.Controls;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Controls;

/// <summary>
/// Provides attached properties for restricting TextBox input to numeric values.
/// Supports integer-only, decimal, and currency (decimal with thousand separators) modes.
/// </summary>
public static class NumericInput
{
    private static readonly AttachedProperty<string> LastValidTextProperty =
        AvaloniaProperty.RegisterAttached<TextBox, string>("LastValidText", typeof(NumericInput), string.Empty);

    /// <summary>
    /// Identifies the IsCurrency attached property, which enables currency input mode
    /// (digits, decimal separator, and automatic thousand-separator formatting).
    /// </summary>
    public static readonly AttachedProperty<bool> IsCurrencyProperty =
        AvaloniaProperty.RegisterAttached<TextBox, bool>("IsCurrency", typeof(NumericInput));

    /// <summary>
    /// Identifies the IsDecimal attached property, which restricts input to digits
    /// and at most one decimal separator based on the current culture.
    /// </summary>
    public static readonly AttachedProperty<bool> IsDecimalProperty =
        AvaloniaProperty.RegisterAttached<TextBox, bool>("IsDecimal", typeof(NumericInput));

    /// <summary>
    /// Identifies the IsInteger attached property, which restricts input to digits only.
    /// </summary>
    public static readonly AttachedProperty<bool> IsIntegerProperty =
        AvaloniaProperty.RegisterAttached<TextBox, bool>("IsInteger", typeof(NumericInput));

    static NumericInput()
    {
        IsCurrencyProperty.Changed.AddClassHandler<TextBox>(OnIsCurrencyChanged);
        IsDecimalProperty.Changed.AddClassHandler<TextBox>(OnIsDecimalChanged);
        IsIntegerProperty.Changed.AddClassHandler<TextBox>(OnIsIntegerChanged);
    }

    /// <summary>
    /// Gets the value of the <see cref="IsCurrencyProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <returns><see langword="true"/> if currency mode is enabled; otherwise, <see langword="false"/>.</returns>
    public static bool GetIsCurrency(TextBox textBox) => textBox.GetValue(IsCurrencyProperty);

    /// <summary>
    /// Gets the value of the <see cref="IsDecimalProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <returns><see langword="true"/> if decimal mode is enabled; otherwise, <see langword="false"/>.</returns>
    public static bool GetIsDecimal(TextBox textBox) => textBox.GetValue(IsDecimalProperty);

    /// <summary>
    /// Gets the value of the <see cref="IsIntegerProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <returns><see langword="true"/> if integer mode is enabled; otherwise, <see langword="false"/>.</returns>
    public static bool GetIsInteger(TextBox textBox) => textBox.GetValue(IsIntegerProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsCurrencyProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <param name="value">Whether to enable currency mode.</param>
    public static void SetIsCurrency(TextBox textBox, bool value) => textBox.SetValue(IsCurrencyProperty, value);

    /// <summary>
    /// Sets the value of the <see cref="IsDecimalProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <param name="value">Whether to enable decimal mode.</param>
    public static void SetIsDecimal(TextBox textBox, bool value) => textBox.SetValue(IsDecimalProperty, value);

    /// <summary>
    /// Sets the value of the <see cref="IsIntegerProperty"/> for the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <param name="value">Whether to enable integer mode.</param>
    public static void SetIsInteger(TextBox textBox, bool value) => textBox.SetValue(IsIntegerProperty, value);

    private static void OnIsCurrencyChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            textBox.TextChanged += OnCurrencyTextChanged;
        }
        else
        {
            textBox.TextChanged -= OnCurrencyTextChanged;
        }
    }

    private static void OnIsDecimalChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            textBox.TextChanged += OnDecimalTextChanged;
        }
        else
        {
            textBox.TextChanged -= OnDecimalTextChanged;
        }
    }

    private static void OnIsIntegerChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            textBox.TextChanged += OnIntegerTextChanged;
        }
        else
        {
            textBox.TextChanged -= OnIntegerTextChanged;
        }
    }

    private static void OnCurrencyTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox)
        {
            return;
        }

        var text = textBox.Text ?? string.Empty;
        var nfi = CultureInfo.CurrentCulture.NumberFormat;
        var decimalSeparator = nfi.NumberDecimalSeparator;
        var groupSeparator = nfi.NumberGroupSeparator;

        // Strip group separators to get the raw numeric value
        var raw = text.Replace(groupSeparator, string.Empty);

        if (raw.Length == 0)
        {
            textBox.SetValue(LastValidTextProperty, string.Empty);
            return;
        }

        if (!IsValidDecimal(raw, decimalSeparator))
        {
            var lastValid = textBox.GetValue(LastValidTextProperty);
            textBox.Text = lastValid;
            textBox.CaretIndex = lastValid.Length;
            return;
        }

        var normalized = NormalizeDecimal(raw, decimalSeparator);
        var formatted = FormatCurrency(normalized, decimalSeparator, groupSeparator, nfi.NumberGroupSizes);

        if (formatted != text)
        {
            // Calculate new caret position after reformatting
            var caretIndex = textBox.CaretIndex;
            var digitsBeforeCaret = CountDigitsAndSeparator(text, caretIndex, decimalSeparator);
            textBox.Text = formatted;
            textBox.CaretIndex = FindCaretPosition(formatted, digitsBeforeCaret, decimalSeparator);
        }

        textBox.SetValue(LastValidTextProperty, formatted);
    }

    private static void OnDecimalTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox)
        {
            return;
        }

        var text = textBox.Text ?? string.Empty;
        var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        if (text.Length == 0 || IsValidDecimal(text, decimalSeparator))
        {
            var normalized = NormalizeDecimal(text, decimalSeparator);

            if (normalized != text)
            {
                textBox.Text = normalized;
                textBox.CaretIndex = normalized.Length;
            }

            textBox.SetValue(LastValidTextProperty, normalized);
            return;
        }

        var lastValid = textBox.GetValue(LastValidTextProperty);
        var caretIndex = textBox.CaretIndex;

        textBox.Text = lastValid;
        textBox.CaretIndex = Math.Max(0, Math.Min(caretIndex - 1, lastValid.Length));
    }

    private static void OnIntegerTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox)
        {
            return;
        }

        var text = textBox.Text ?? string.Empty;

        if (text.Length == 0 || IsAllDigits(text))
        {
            var normalized = NormalizeInteger(text);

            if (normalized != text)
            {
                textBox.Text = normalized;
                textBox.CaretIndex = normalized.Length;
            }

            textBox.SetValue(LastValidTextProperty, normalized);
            return;
        }

        var lastValid = textBox.GetValue(LastValidTextProperty);
        var caretIndex = textBox.CaretIndex;

        textBox.Text = lastValid;
        textBox.CaretIndex = Math.Max(0, Math.Min(caretIndex - 1, lastValid.Length));
    }

    private static bool IsValidDecimal(string text, string decimalSeparator)
    {
        var separatorCount = 0;

        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]))
            {
                continue;
            }

            if (decimalSeparator.Length == 1 && text[i] == decimalSeparator[0])
            {
                separatorCount++;
                if (separatorCount > 1)
                {
                    return false;
                }
            }
            else if (text[i..].StartsWith(decimalSeparator, StringComparison.Ordinal))
            {
                separatorCount++;
                if (separatorCount > 1)
                {
                    return false;
                }

                i += decimalSeparator.Length - 1;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsAllDigits(string text)
    {
        foreach (var c in text)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }

    private static string NormalizeDecimal(string text, string decimalSeparator)
    {
        if (text.Length == 0)
        {
            return text;
        }

        // Prepend "0" when the text starts with the decimal separator
        if (text.StartsWith(decimalSeparator, StringComparison.Ordinal))
        {
            return "0" + text;
        }

        // Strip leading zeros while preserving a single "0" or "0."
        var separatorIndex = text.IndexOf(decimalSeparator, StringComparison.Ordinal);

        if (separatorIndex < 0)
        {
            // No decimal part: remove leading zeros
            var trimmed = text.TrimStart('0');
            return trimmed.Length == 0 ? "0" : trimmed;
        }

        // Has decimal part: remove leading zeros from the integer portion
        var integerPart = text[..separatorIndex].TrimStart('0');

        if (integerPart.Length == 0)
        {
            integerPart = "0";
        }

        return integerPart + text[separatorIndex..];
    }

    private static string NormalizeInteger(string text)
    {
        if (text.Length == 0)
        {
            return text;
        }

        var trimmed = text.TrimStart('0');
        return trimmed.Length == 0 ? "0" : trimmed;
    }

    private static string FormatCurrency(
        string raw,
        string decimalSeparator,
        string groupSeparator,
        int[] groupSizes)
    {
        var separatorIndex = raw.IndexOf(decimalSeparator, StringComparison.Ordinal);
        var integerPart = separatorIndex < 0 ? raw : raw[..separatorIndex];
        var decimalPart = separatorIndex < 0 ? string.Empty : raw[separatorIndex..];

        if (integerPart.Length == 0)
        {
            return "0" + decimalPart;
        }

        // Apply thousand-group separators
        var groupSize = groupSizes.Length > 0 ? groupSizes[0] : 3;
        var result = new StringBuilder();
        var count = 0;

        for (var i = integerPart.Length - 1; i >= 0; i--)
        {
            if (count > 0 && count % groupSize == 0)
            {
                result.Insert(0, groupSeparator);
            }

            result.Insert(0, integerPart[i]);
            count++;
        }

        return result + decimalPart;
    }

    private static int CountDigitsAndSeparator(string text, int upTo, string decimalSeparator)
    {
        var count = 0;

        for (var i = 0; i < upTo && i < text.Length; i++)
        {
            if (char.IsDigit(text[i]) || text[i..].StartsWith(decimalSeparator, StringComparison.Ordinal))
            {
                count++;
            }
        }

        return count;
    }

    private static int FindCaretPosition(string formatted, int targetDigitCount, string decimalSeparator)
    {
        var count = 0;

        for (var i = 0; i < formatted.Length; i++)
        {
            if (count == targetDigitCount)
            {
                return i;
            }

            if (char.IsDigit(formatted[i]) || formatted[i..].StartsWith(decimalSeparator, StringComparison.Ordinal))
            {
                count++;
            }
        }

        return formatted.Length;
    }
}
