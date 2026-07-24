using Cocosoft.Framework.Mvvm.Commands;
using Cocosoft.Framework.Mvvm.Helpers;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Cocosoft.Framework.Mvvm;

/// <summary>
/// The <see cref="MvvmForm{TViewModel}"/> class is a base class for Windows Forms that follows the MVVM pattern.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="System.Windows.Forms.Form" />
public abstract class MvvmForm<TViewModel> : Form where TViewModel : ViewModelBase
{
    /// <summary>
    /// Gets the view model associated with this form.
    /// </summary>
    public TViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MvvmForm{TViewModel}"/> class.
    /// </summary>
    /// <param name="viewModel">The view model to associate with this form.</param>
    public MvvmForm(TViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.Load += OnFormLoad;
    }

    private void OnFormLoad(object? sender, EventArgs e)
    {
        OnBindViewModel();
    }

    protected virtual void OnBindViewModel()
    {
    }

    protected void BindProperty(
        Control control,
        string controlProperty,
        string viewModelProperty,
        string? formatString = null)
    {
        control.DataBindings.Add(
            controlProperty,
            ViewModel,
            viewModelProperty,
            true,
            DataSourceUpdateMode.OnPropertyChanged,
            null,
            formatString ?? string.Empty);
    }

    protected void BindProperty<TProperty>(
        Label label,
        Expression<Func<TViewModel, TProperty>> viewModelProperty,
        string? formatString = null)
    {
        var propertyName = ((MemberExpression)viewModelProperty.Body).Member.Name;
        BindProperty(label, nameof(label.Text), propertyName, formatString);
    }

    protected void BindProperty<TProperty>(
        TextBox txtBox,
        Expression<Func<TViewModel, TProperty>> viewModelProperty,
        string? formatString = null)
    {
        var propertyName = ((MemberExpression)viewModelProperty.Body).Member.Name;
        BindProperty(txtBox, nameof(txtBox.Text), propertyName, formatString);
    }

    protected void BindProperty<TProperty>(
        CheckBox checkBox,
        Expression<Func<TViewModel, TProperty>> viewModelProperty)
    {
        var propertyName = ((MemberExpression)viewModelProperty.Body).Member.Name;
        BindProperty(checkBox, nameof(checkBox.Checked), propertyName);
    }

    protected void BindProperty<TProperty>(
        Expression<Func<TViewModel, TProperty>> viewModelProperty,
        Action<TProperty> updateAction)
    {
        var propertyName = ((MemberExpression)viewModelProperty.Body).Member.Name;
        this.ViewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == propertyName)
            {
                var value = viewModelProperty.Compile()(this.ViewModel);
                updateAction(value);
            }
        };
    }

    protected void BindProperty<TRow>(
        DataGridView gridView,
        Expression<Func<TViewModel, BindingList<TRow>>> viewModelDataSource,
        Expression<Func<TViewModel, TRow?>> viewModelSelectedRow,
        Action<DataGridViewColumnCollection>? rowMapper = default)
    {
        var selectedRowPropertyName = ((MemberExpression)viewModelDataSource.Body).Member.Name;

        gridView.SelectionChanged += (s, e) =>
        {
            var propInfo = (viewModelSelectedRow.Body as MemberExpression)?.Member as System.Reflection.PropertyInfo;

            if (gridView.CurrentRow?.DataBoundItem is TRow selectedRow)
            {
                propInfo?.SetValue(this.ViewModel, selectedRow);
            }
            else
            {
                propInfo?.SetValue(this.ViewModel, default(TRow));
            }
        };

        gridView.DataSource = viewModelDataSource.Compile()(this.ViewModel);
        rowMapper?.Invoke(gridView.Columns);
    }

    protected void BindCommand(Control control, ICommand command, string eventName = "Click")
    {
        var eventInfo = control.GetType().GetEvent(eventName)
            ?? throw new ArgumentException($"The event {eventName} does not exist on {control.GetType().Name}");

        EventHandler eventHandler = (s, e) =>
        {
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        };

        eventInfo.AddEventHandler(control, eventHandler);

        control.Enabled = command.CanExecute(null);
        command.CanExecuteChanged += (s, e) =>
        {
            void SetControlEnable() => control.Enabled = command.CanExecute(null);

            if (control.InvokeRequired)
            {
                control.Invoke(SetControlEnable);
            }
            else
            {
                SetControlEnable();
            }
        };
    }

    protected void BindCommand<TParameter>(
        Control control,
        ICommand<TParameter> command,
        Expression<Func<TViewModel, TParameter>> parameterExpression,
        string eventName = "Click")
    {
        var eventInfo = control.GetType().GetEvent(eventName)
            ?? throw new ArgumentException($"The event {eventName} does not exist on {control.GetType().Name}");
        var getParameterFunc = parameterExpression.Compile();
        EventHandler eventHandler = (s, e) =>
        {
            var parameter = getParameterFunc(this.ViewModel);
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        };
        eventInfo.AddEventHandler(control, eventHandler);
        control.Enabled = command.CanExecute(getParameterFunc(this.ViewModel));
        command.CanExecuteChanged += (s, e) =>
        {
            void SetControlEnable() => control.Enabled = command.CanExecute(getParameterFunc(this.ViewModel));
            if (control.InvokeRequired)
            {
                control.Invoke(SetControlEnable);
            }
            else
            {
                SetControlEnable();
            }
        };
    }

    protected void BindCommand(TextBox txtBox, Expression<Func<TViewModel, ICommand<(object sender, KeyPressEventArgs e)>>> keyPressCommand)
    {
        var command = keyPressCommand.Compile()(this.ViewModel);
        txtBox.BindKeyPressCommand(command);
    }
}
