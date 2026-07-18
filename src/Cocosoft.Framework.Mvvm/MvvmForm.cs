using Cocosoft.Framework.Mvvm.Commands;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Cocosoft.Framework.Mvvm;

public abstract class MvvmForm<TViewModel> : Form where TViewModel : ViewModelBase
{
    protected TViewModel ViewModel { get; }

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
}
