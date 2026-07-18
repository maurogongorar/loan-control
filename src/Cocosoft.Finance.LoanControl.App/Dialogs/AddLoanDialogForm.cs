using Cocosoft.Finance.LoanControl.Core.ViewModels;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Cocosoft.Framework.Mvvm;
using Cocosoft.Framework.Mvvm.Helpers;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class AddLoanDialogForm : MvvmForm<AddLoanViewModel>, IAddLoanDialog
{
    public Loan? Loan { get; private set; }

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
        BindProperty(vm => vm.Loan, loan => Loan = loan);

        amountTextBox.BindKeyPressCommand(ViewModel.NumericVerificationCommand);
        annualInterestTextBox.BindKeyPressCommand(ViewModel.NumericVerificationCommand);
        monthlyInterestTextBox.BindKeyPressCommand(ViewModel.NumericVerificationCommand);
        numberInstalmentsTextBox.BindKeyPressCommand(ViewModel.NumericVerificationCommand);
        BindCommand(cancelButton, ViewModel.CancelCommand);
        BindCommand(okButton, ViewModel.OkCommand);
    }
}
