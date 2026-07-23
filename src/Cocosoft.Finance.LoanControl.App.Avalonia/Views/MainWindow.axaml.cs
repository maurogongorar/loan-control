using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Views;

public partial class MainWindow : Window
{
    private const double CollapseThreshold = 1000;

    private bool _isAutoCollapsed;

    public MainWindow()
    {
        this.InitializeComponent();
        this.SizeChanged += this.OnSizeChanged;
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MaximizeButton_Click(object? sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }

    private void MinimizeButton_Click(object? sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (this.DataContext is not ViewModels.MainViewModel vm)
        {
            return;
        }

        if (e.NewSize.Width < CollapseThreshold)
        {
            if (!this._isAutoCollapsed)
            {
                this._isAutoCollapsed = true;
                vm.IsSidebarExpanded = false;
                vm.CanToggleSidebar = false;
            }
        }
        else
        {
            if (this._isAutoCollapsed)
            {
                this._isAutoCollapsed = false;
                vm.IsSidebarExpanded = true;
                vm.CanToggleSidebar = true;
            }
        }
    }

    private void Resize_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Control control
            || !e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            return;
        }

        var edge = control.Name switch
        {
            "ResizeTop" => WindowEdge.North,
            "ResizeBottom" => WindowEdge.South,
            "ResizeLeft" => WindowEdge.West,
            "ResizeRight" => WindowEdge.East,
            "ResizeTopLeft" => WindowEdge.NorthWest,
            "ResizeTopRight" => WindowEdge.NorthEast,
            "ResizeBottomLeft" => WindowEdge.SouthWest,
            "ResizeBottomRight" => WindowEdge.SouthEast,
            _ => (WindowEdge?)null
        };

        if (edge.HasValue)
        {
            this.BeginResizeDrag(edge.Value, e);
        }
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        if (!point.Properties.IsLeftButtonPressed)
        {
            return;
        }

        if (e.ClickCount == 2)
        {
            this.WindowState = this.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }
        else
        {
            this.BeginMoveDrag(e);
        }
    }
}