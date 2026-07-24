using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Avalonia;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    /// <summary>
    /// Creates the view control that corresponds to the given view model instance.
    /// </summary>
    /// <param name="param">The view model instance to resolve a view for.</param>
    /// <returns>
    /// The matching <see cref="Control"/>, or a <see cref="TextBlock"/> with an error message
    /// if no matching view type is found.
    /// </returns>
    public Control? Build(object? param)
    {
        if (param is null)
        {
            return null;
        }
        
        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        
        return new TextBlock { Text = "Not Found: " + name };
    }

    /// <summary>
    /// Determines whether this data template can handle the specified data object.
    /// </summary>
    /// <param name="data">The data object to evaluate.</param>
    /// <returns>
    /// <see langword="true"/> if the data object is a <see cref="ViewModelBase"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
