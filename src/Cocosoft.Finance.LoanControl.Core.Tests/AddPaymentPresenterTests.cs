using Cocosoft.Finance.LoanControl.Core.ViewModels;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.Core.Tests;

public class AddPaymentViewModelTests
{
    [Fact]
    public void CountDays_SameMonth_ReturnsCorrectDays()
    {
        var days = AddPaymentViewModel.CountDays(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        Assert.Equal(14, days);
    }

    [Fact]
    public void CountDays_DifferentMonths_ReturnsCorrectDays()
    {
        var days = AddPaymentViewModel.CountDays(new DateTime(2024, 1, 15), new DateTime(2024, 3, 15));
        Assert.Equal(60, days);
    }

    [Fact]
    public void CountDays_FullYear_Returns360()
    {
        var days = AddPaymentViewModel.CountDays(new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
        Assert.Equal(360, days);
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

        var vm = new AddPaymentViewModel(loan);
        vm.Fee = 120000m;

        Assert.False(string.IsNullOrEmpty(vm.CapitalText));
        Assert.False(string.IsNullOrEmpty(vm.NewBalanceText));
    }

    [Fact]
    public void CreatePayment_ReturnsPaymentWithCorrectLoanId()
    {
        var loan = new Loan
        {
            Id = 42,
            CurrentBalance = 500000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            Fee = 50000m
        };

        var vm = new AddPaymentViewModel(loan);
        vm.Fee = 50000m;

        var payment = vm.CreatePayment();

        Assert.Equal(42, payment.LoanId);
        Assert.Equal(50000m, payment.Amount);
    }

    [Fact]
    public void CalculateInterestDue_WithNoPayments_UsesDisubrsementDate()
    {
        var loan = new Loan
        {
            CurrentBalance = 1000000m,
            AnnualInterest = 0.12,
            DisbursementDate = DateTime.Now.AddDays(-30),
            LastPaymentDate = null
        };

        var interest = AddPaymentViewModel.CalculateInterestDue(loan);

        Assert.True(interest > 0);
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

        var vm = new AddPaymentViewModel(loan);
        var raised = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AddPaymentViewModel.IsValid)) raised = true; };

        vm.Fee = 120000m;

        Assert.True(raised);
    }
}
