using Cocosoft.Framework.LoanControl.UI.Theme;
using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Framework.LoanControl.UI.Layout;

public abstract class BaseModalForm<TViewModel>(TViewModel viewModel) : MvvmForm<TViewModel>(viewModel)
    where TViewModel : ViewModelBase
{
    protected override void OnLoad(EventArgs e)
    {
        this.ApplyModalStyle();
        this.ApplyThemeToControls(this.Controls);
        base.OnLoad(e);
    }

    private void ApplyModalStyle()
    {
        this.MinimizeBox = false;
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.ShowInTaskbar = false;
        this.BackColor = AppColors.Background;
        this.ForeColor = AppColors.TextPrimary;
        this.Font = AppFonts.Default;
        this.Padding = new Padding(20);
    }

    private void ApplyThemeToControls(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            switch (control)
            {
                case Button button:
                    this.ApplyButtonStyle(button);
                    break;
                case TextBox textBox:
                    this.ApplyTextBoxStyle(textBox);
                    break;
                case Label label:
                    this.ApplyLabelStyle(label);
                    break;
            }

            if (control.HasChildren)
            {
                this.ApplyThemeToControls(control.Controls);
            }
        }
    }

    private void ApplyButtonStyle(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = AppFonts.Default;
        button.Cursor = Cursors.Hand;
        button.Height = 32;

        if (this.AcceptButton == button)
        {
            button.BackColor = AppColors.Primary;
            button.ForeColor = AppColors.Surface;
            button.FlatAppearance.MouseOverBackColor = AppColors.PrimaryLight;
            button.FlatAppearance.MouseDownBackColor = AppColors.PrimaryDark;
        }
        else
        {
            button.BackColor = AppColors.Surface;
            button.ForeColor = AppColors.Primary;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = AppColors.Secondary;
            button.FlatAppearance.MouseOverBackColor = AppColors.Background;
            button.FlatAppearance.MouseDownBackColor = AppColors.Border;
        }
    }

    private void ApplyTextBoxStyle(TextBox textBox)
    {
        textBox.Font = AppFonts.Default;
        textBox.BackColor = AppColors.Surface;
        textBox.ForeColor = AppColors.TextPrimary;
        textBox.BorderStyle = BorderStyle.FixedSingle;
    }

    private void ApplyLabelStyle(Label label)
    {
        label.Font = AppFonts.Label;
        label.ForeColor = AppColors.TextSecondary;
    }
}
