using Cocosoft.Framework.LoanControl.UI.Theme;
using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Framework.LoanControl.UI.Layout;

public abstract class BaseLayoutForm<TViewModel>(TViewModel viewModel) : MvvmForm<TViewModel>(viewModel)
    where TViewModel : ViewModelBase
{
    private Panel myNavigationPanel = default!;
    private Panel myHeaderPanel = default!;
    private Panel myContentPanel = default!;

    protected Panel PnlNavigation => this.myNavigationPanel;
    protected Panel PnlHeader => this.myHeaderPanel;
    protected Panel PnlContent => this.myContentPanel;

    protected override void OnLoad(EventArgs e)
    {
        this.ApplyLayoutStyle();
        this.BuildLayout();
        base.OnLoad(e);
    }

    private void ApplyLayoutStyle()
    {
        this.BackColor = AppColors.Background;
        this.ForeColor = AppColors.TextPrimary;
        this.Font = AppFonts.Default;
    }

    private void BuildLayout()
    {
        this.myNavigationPanel = new Panel
        {
            Dock = DockStyle.Left,
            Width = 220,
            BackColor = AppColors.Primary,
            Padding = new Padding(0, 16, 0, 16),
        };

        this.myHeaderPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 56,
            BackColor = AppColors.Surface,
            Padding = new Padding(20, 0, 20, 0),
        };

        this.myContentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = AppColors.Background,
            Padding = new Padding(20),
        };

        this.Controls.Add(this.myContentPanel);
        this.Controls.Add(this.myHeaderPanel);
        this.Controls.Add(this.myNavigationPanel);
    }
}
