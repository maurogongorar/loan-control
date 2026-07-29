using Cocosoft.Finance.LoanControl.App.ViewModels;
using Cocosoft.Finance.LoanControl.App.Views.Dialogs;
using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class AddLoanDialogForm : MvvmForm<AddLoanViewModel>, IAddLoanDialog
{
    public AddLoanDialogForm(AddLoanViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    protected override void OnBindViewModel()
    {
        BindProperty(debtorNameTextBox, vm => vm.DebtorName);
        BindProperty(amountTextBox, vm => vm.Amount, "C");
        BindProperty(annualInterestTextBox, vm => vm.AnnualInterest);
        BindProperty(monthlyInterestTextBox, vm => vm.MonthlyInterest);
        BindProperty(numberInstalmentsTextBox, vm => vm.NumberInstalments);
        BindProperty(monthlyFeeTextBox, vm => vm.MonthlyFee, "C");
        BindProperty(vm => vm.DialogResult, result => DialogResult = result);

        BindCommand(amountTextBox, vm => vm.NumericVerificationCommand);
        BindCommand(annualInterestTextBox, vm => vm.NumericVerificationCommand);
        BindCommand(monthlyInterestTextBox, vm => vm.NumericVerificationCommand);
        BindCommand(numberInstalmentsTextBox, vm => vm.NumericVerificationCommand);
        BindCommand(cancelButton, ViewModel.CancelCommand);
        BindCommand(okButton, ViewModel.OkCommand);
    }
}
