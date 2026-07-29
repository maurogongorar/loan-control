using Cocosoft.Finance.LoanControl.App.ViewModels;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.Tests.App;

public class AddPaymentViewModelTests
{
    [Fact]
    public void SetLoan_SetsInterestDueAndFee()
    {
        var loan = new Loan
        {
            Id = 1,
            CurrentBalance = 1000000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            Fee = 100000m
        };

        var vm = new AddPaymentViewModel();
        vm.SetLoan(loan);

        Assert.True(vm.InterestDue > 0);
        Assert.Equal(100000m, vm.Fee);
    }

    [Fact]
    public void SettingFee_CalculatesCapitalAndNewBalance()
    {
        var loan = new Loan
        {
            Id = 1,
            CurrentBalance = 1000000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            Fee = 100000m
        };

        var vm = new AddPaymentViewModel();
        vm.SetLoan(loan);
        vm.Fee = 120000m;

        Assert.True(vm.CapitalPayment > 0);
        Assert.True(vm.NewBalance < 1000000m);
    }

    [Fact]
    public void OkCommand_CanExecute_WhenLoanSetAndCapitalPositive()
    {
        var loan = new Loan
        {
            Id = 42,
            CurrentBalance = 500000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            Fee = 50000m
        };

        var vm = new AddPaymentViewModel();
        vm.SetLoan(loan);
        vm.Fee = 50000m;

        Assert.True(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void OkCommand_CannotExecute_WhenNoLoan()
    {
        var vm = new AddPaymentViewModel();

        Assert.False(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void InterestDue_WithNoPayments_UsesDisubrsementDate()
    {
        var loan = new Loan
        {
            CurrentBalance = 1000000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            LastPaymentDate = null,
            Fee = 100000m
        };

        var vm = new AddPaymentViewModel();
        vm.SetLoan(loan);

        Assert.True(vm.InterestDue > 0);
    }

    [Fact]
    public void PropertyChanged_RaisedWhenFeeSet()
    {
        var loan = new Loan
        {
            CurrentBalance = 1000000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            Fee = 100000m
        };

        var vm = new AddPaymentViewModel();
        vm.SetLoan(loan);
        var raised = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AddPaymentViewModel.CapitalPayment)) raised = true; };

        vm.Fee = 120000m;

        Assert.True(raised);
    }
}
