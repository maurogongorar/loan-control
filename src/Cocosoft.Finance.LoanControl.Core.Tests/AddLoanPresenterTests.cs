using Cocosoft.Finance.LoanControl.Core.ViewModels;

namespace Cocosoft.Finance.LoanControl.Core.Tests;

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

        Assert.True(vm.IsValid);
        Assert.False(string.IsNullOrEmpty(vm.MonthlyFeeText));
    }

    [Fact]
    public void SettingAmount_WithZeroInterest_IsNotValid()
    {
        var vm = new AddLoanViewModel
        {
            DebtorName = "Test",
            AnnualInterest = 0,
            NumberInstalments = 12,
            Amount = 1000000m
        };

        Assert.False(vm.IsValid);
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
    public void IsValid_FalseWhenDebtorNameTooShort()
    {
        var vm = new AddLoanViewModel
        {
            Amount = 1000000m,
            AnnualInterest = 12,
            NumberInstalments = 12,
            DebtorName = "Ab"
        };

        Assert.False(vm.IsValid);
    }

    [Fact]
    public void PropertyChanged_RaisedWhenMonthlyFeeChanges()
    {
        var vm = new AddLoanViewModel { AnnualInterest = 12, NumberInstalments = 12 };
        var raised = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AddLoanViewModel.MonthlyFeeText)) raised = true; };

        vm.Amount = 1000000m;

        Assert.True(raised);
    }
}
