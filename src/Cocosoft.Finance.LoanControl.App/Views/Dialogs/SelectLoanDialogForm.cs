using Cocosoft.Finance.LoanControl.App.ViewModels;
using Cocosoft.Finance.LoanControl.App.Views.Dialogs;
using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class SelectLoanDialogForm : MvvmForm<SelectLoanViewModel>, ISelectLoanDialog
{
    public SelectLoanDialogForm(SelectLoanViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    protected override void OnBindViewModel()
    {
        BindProperty(loansDataGridView, vm => vm.Loans, vm => vm.SelectedLoan);
        BindProperty(getAllLoansCheckBox, vm => vm.GetAllLoans);

        BindCommand(cancelButton, ViewModel.CancelCommand);
        BindCommand(okButton, ViewModel.OkCommand);
    }

    private void OkButton_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;
}