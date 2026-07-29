using Cocosoft.Finance.LoanControl.App.ViewModels;
using Cocosoft.Finance.LoanControl.Core.Resources;
using Cocosoft.Finance.LoanControl.Core.Services;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using NSubstitute;

namespace Cocosoft.Finance.LoanControl.Tests.App;

public class SelectLoanViewModelTests
{
    private readonly ILoanService myLoanService;
    private readonly ILocalizationService myLocalizer;

    public SelectLoanViewModelTests()
    {
        this.myLoanService = Substitute.For<ILoanService>();
        this.myLocalizer = Substitute.For<ILocalizationService>();
        this.myLocalizer["Yes"].Returns("Yes");
        this.myLocalizer["No"].Returns("No");
    }

    [Fact]
    public void Constructor_LoadsActiveLoans()
    {
        this.myLoanService.GetActiveLoans().Returns(
        [
            new Loan { Id = 1, DebtorName = "Test", InitialAmount = 1000m, CurrentBalance = 500m }
        ]);

        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);

        Assert.Single(vm.Loans);
        Assert.Equal("Test", vm.Loans[0].DebtorName);
    }

    [Fact]
    public void GetAllLoans_True_LoadsAllLoans()
    {
        this.myLoanService.GetActiveLoans().Returns([]);
        this.myLoanService.GetAllLoans().Returns(
        [
            new Loan { Id = 1, DebtorName = "Active", IsClosed = false },
            new Loan { Id = 2, DebtorName = "Closed", IsClosed = true }
        ]);

        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);
        vm.GetAllLoans = true;

        Assert.Equal(2, vm.Loans.Count);
    }

    [Fact]
    public void OkCommand_CannotExecute_WhenNoSelection()
    {
        this.myLoanService.GetActiveLoans().Returns([]);
        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);

        Assert.False(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void OkCommand_CanExecute_WhenLoanSelected()
    {
        this.myLoanService.GetActiveLoans().Returns(
        [
            new Loan { Id = 1, DebtorName = "Test", InitialAmount = 1000m, CurrentBalance = 500m }
        ]);

        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);
        vm.SelectedLoan = vm.Loans[0];

        Assert.True(vm.OkCommand.CanExecute(null));
    }

    [Fact]
    public void SelectedId_ReturnsSelectedLoanId()
    {
        this.myLoanService.GetActiveLoans().Returns(
        [
            new Loan { Id = 5, DebtorName = "Test" }
        ]);

        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);
        vm.SelectedLoan = vm.Loans[0];

        Assert.Equal(5, vm.SelectedId);
    }

    [Fact]
    public void SelectedId_Null_WhenNoSelection()
    {
        this.myLoanService.GetActiveLoans().Returns([]);
        var vm = new SelectLoanViewModel(this.myLoanService, this.myLocalizer);

        Assert.Null(vm.SelectedId);
    }
}
