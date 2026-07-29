using Cocosoft.Finance.LoanControl.App.ViewModels;

namespace Cocosoft.Finance.LoanControl.Tests.App;

public class AddLoanViewModelTests
{
    [Fact]
    public void SettingAmount_WithValidInputs_CalculatesFee()
    {
        var vm = new AddLoanViewModel
        {
            DebtorName = "Test Debtor",
            AnnualInterest = 12,
            NumberInstalments = 12,
            Amount = 1000000m
        };

        Assert.True(vm.MonthlyFee > 0);
    }

    [Fact]
    public void SettingAmount_WithZeroInterest_FeeIsZero()
    {
        var vm = new AddLoanViewModel
        {
            DebtorName = "Test",
            AnnualInterest = 0,
            NumberInstalments = 12,
            Amount = 1000000m
        };

        Assert.Equal(0m, vm.MonthlyFee);
    }

    [Fact]
    public void SettingAnnualInterest_UpdatesMonthlyInterest()
    {
        var vm = new AddLoanViewModel();

        vm.AnnualInterest = 12;

        Assert.True(vm.MonthlyInterest > 0);
    }

    [Fact]
    public void SettingMonthlyInterest_UpdatesAnnualInterest()
    {
        var vm = new AddLoanViewModel();

        vm.MonthlyInterest = 0.95;

        Assert.True(vm.AnnualInterest > 0);
    }

    [Fact]
    public void CreateLoan_ReturnsLoanWithCorrectValues()
    {
        var vm = new AddLoanViewModel
        {
            Amount = 5000000m,
            AnnualInterest = 12,
            NumberInstalments = 24,
            DebtorName = "Juan Pérez"
        };

        var loan = vm.CreateLoan();

        Assert.Equal(0.12, loan.AnnualInterest);
        Assert.Equal(5000000m, loan.CurrentBalance);
        Assert.Equal(5000000m, loan.InitialAmount);
        Assert.Equal("Juan Pérez", loan.DebtorName);
        Assert.Equal(24, loan.NumberInstalments);
        Assert.True(loan.Fee > 0);
    }

    [Fact]
    public void OkCommand_CannotExecute_WhenDebtorNameTooShort()
    {
        var vm = new AddLoanViewModel
        {
            Amount = 1000000m,
            AnnualInterest = 12,
            NumberInstalments = 12,
            DebtorName = "Ab"
        };

        Assert.False(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void OkCommand_CanExecute_WhenAllFieldsValid()
    {
        var vm = new AddLoanViewModel
        {
            DebtorName = "Test Debtor",
            AnnualInterest = 12,
            NumberInstalments = 12,
            Amount = 1000000m
        };

        Assert.True(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void OkCommand_CannotExecute_WhenFeeIsZero()
    {
        var vm = new AddLoanViewModel
        {
            DebtorName = "Test Debtor",
            AnnualInterest = 0,
            NumberInstalments = 12,
            Amount = 1000000m
        };

        Assert.False(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void PropertyChanged_RaisedWhenMonthlyFeeChanges()
    {
        var vm = new AddLoanViewModel { AnnualInterest = 12, NumberInstalments = 12 };
        var raised = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AddLoanViewModel.MonthlyFee)) raised = true; };

        vm.Amount = 1000000m;

        Assert.True(raised);
    }
}
