using Cocosoft.Finance.LoanControl.App.ViewModels;
using Cocosoft.Finance.LoanControl.App.Views.Dialogs;
using Cocosoft.Framework.Mvvm;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class AddPaymentDialogForm : MvvmForm<AddPaymentViewModel>, IAddPaymentDialog
{
    public AddPaymentDialogForm(AddPaymentViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    protected override void OnBindViewModel()
    {
        BindProperty(capitalTextBox, vm => vm.CapitalPayment, "C");
        BindProperty(interestTextBox, vm => vm.InterestDue, "C");
        BindProperty(feeTextBox, vm => vm.Fee, "C");
        BindProperty(newBalanceTextBox, vm => vm.NewBalance, "C");
        BindProperty(vm => vm.DialogResult, result => DialogResult = result);

        BindCommand(feeTextBox, vm => vm.NumericVerificationCommand);
        BindCommand(cancelButton, ViewModel.CancelCommand);
        BindCommand(okButton, ViewModel.OkCommand);
    }
}
