using Cocosoft.Finance.LoanControl.Core.ViewModels;
using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using NSubstitute;

namespace Cocosoft.Finance.LoanControl.Core.Tests;

public class MainFormViewModelTests
{
    private readonly IRepository myRepository;
    private readonly MainFormViewModel myViewModel;

    public MainFormViewModelTests()
    {
        this.myRepository = Substitute.For<IRepository>();
        this.myViewModel = new MainFormViewModel(this.myRepository);
    }

    [Fact]
    public void HasLoanLoaded_InitiallyFalse()
    {
        Assert.False(this.myViewModel.HasLoanLoaded);
    }

    [Fact]
    public void AddLoan_CallsRepositoryAndLoadsLoan()
    {
        var loan = new Loan { Id = 1, DebtorName = "Test" };
        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);

        this.myViewModel.AddLoan(loan);

        this.myRepository.Received().AddLoanAsync(loan);
        Assert.True(this.myViewModel.HasLoanLoaded);
    }

    [Fact]
    public void LoadLoan_UpdatesProperties()
    {
        var loan = new Loan
        {
            Id = 1,
            DebtorName = "Test Debtor",
            InitialAmount = 1000000m,
            CurrentBalance = 800000m,
            DisbursementDate = new DateTime(2024, 1, 15),
            AnnualInterest = 0.12,
            NumberInstalments = 12,
            Fee = 90000m,
            InterestCollected = 50000m
        };

        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);

        this.myViewModel.LoadLoan(1);

        Assert.Equal("Test Debtor", this.myViewModel.DebtorNameText);
        Assert.True(this.myViewModel.AddPaymentEnabled);
    }

    [Fact]
    public void ProcessPayment_UpdatesLoanAndCommitsTransaction()
    {
        var loan = new Loan { Id = 1, CurrentBalance = 1000000m };
        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);
        this.myViewModel.LoadLoan(1);

        var transaction = Substitute.For<ITransaction>();
        this.myRepository.BeginTransaction().Returns(transaction);

        var payment = new Payment
        {
            LoanId = 1,
            Interest = 10000m,
            NewBalance = 900000m,
            Date = DateTime.Now
        };

        this.myViewModel.ProcessPayment(payment);

        transaction.Received().Commit();
        this.myRepository.Received().AddPaymentAsync(payment);
    }

    [Fact]
    public void ProcessPayment_WhenExceptionThrown_RollsBack()
    {
        var loan = new Loan { Id = 1, CurrentBalance = 1000000m };
        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);
        this.myViewModel.LoadLoan(1);

        var transaction = Substitute.For<ITransaction>();
        this.myRepository.BeginTransaction().Returns(transaction);
        this.myRepository.AddPaymentAsync(Arg.Any<Payment>()).Returns(x => throw new Exception("DB error"));

        var payment = new Payment { LoanId = 1, NewBalance = 900000m, Date = DateTime.Now };

        Assert.Throws<Exception>(() => this.myViewModel.ProcessPayment(payment));
        transaction.Received().Rollback();
    }

    [Fact]
    public void ProcessPayment_WhenBalanceZero_ClosesLoan()
    {
        var loan = new Loan { Id = 1, CurrentBalance = 100000m };
        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);
        this.myViewModel.LoadLoan(1);

        var transaction = Substitute.For<ITransaction>();
        this.myRepository.BeginTransaction().Returns(transaction);

        var payment = new Payment { LoanId = 1, NewBalance = 0, Interest = 5000m, Date = DateTime.Now };

        this.myViewModel.ProcessPayment(payment);

        Assert.True(loan.IsClosed);
        Assert.Equal(0m, loan.CurrentBalance);
    }

    [Fact]
    public void GetDisplayInterest_FormatsCorrectly()
    {
        var result = MainFormViewModel.GetDisplayInterest(0.12);
        Assert.Contains("12", result);
    }

    [Fact]
    public void PropertyChanged_RaisedOnLoadLoan()
    {
        var loan = new Loan { Id = 1, DebtorName = "Test" };
        this.myRepository.GetLoan(1).Returns(loan);
        this.myRepository.GetLoanPayments(1).Returns([]);

        var raised = new List<string>();
        this.myViewModel.PropertyChanged += (s, e) => raised.Add(e.PropertyName!);

        this.myViewModel.LoadLoan(1);

        Assert.Contains(nameof(MainFormViewModel.DebtorNameText), raised);
        Assert.Contains(nameof(MainFormViewModel.PaymentRows), raised);
    }
}
