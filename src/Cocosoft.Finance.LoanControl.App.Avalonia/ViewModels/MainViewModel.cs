using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private const double SidebarCollapsedWidth = 60;

    private const double SidebarExpandedWidth = 240;

    [ObservableProperty]
    public partial bool CanToggleSidebar { get; set; } = true;

    [ObservableProperty]
    public partial bool IsSidebarExpanded { get; set; } = true;

    public double SidebarWidth => this.IsSidebarExpanded
        ? SidebarExpandedWidth
        : SidebarCollapsedWidth;

    partial void OnIsSidebarExpandedChanged(bool value)
    {
        this.OnPropertyChanged(nameof(this.SidebarWidth));
    }

    [RelayCommand]
    private void ToggleSidebar()
    {
        this.IsSidebarExpanded = !this.IsSidebarExpanded;
    }
}
