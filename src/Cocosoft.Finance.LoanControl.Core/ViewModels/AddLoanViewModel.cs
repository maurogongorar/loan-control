using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Cocosoft.Framework.Mvvm;
using Cocosoft.Framework.Mvvm.Commands;
using Cocosoft.Framework.Mvvm.Helpers;
using System.Windows.Forms;

namespace Cocosoft.Finance.LoanControl.Core.ViewModels;

public class AddLoanViewModel : ViewModelBase
{
    private decimal myAmount;

    private double myAnnualInterest = 12;

    private string myDebtorName = string.Empty;

    private DialogResult myDialogResult;

    private decimal myMonthlyFee;

    private Loan? myLoan;

    private double myMonthlyInterest = Math.Round((Math.Pow(1.12, 1.0 / 12) - 1) * 100, 2);

    private int myNumberInstalments = 12;

    public event Action? RequestClose;

    public decimal Amount
    {
        get => this.myAmount;
        set
        {
            if (this.SetProperty(ref this.myAmount, value))
            {
                this.CalculateFee();
            }
        }
    }

    public double AnnualInterest
    {
        get => this.myAnnualInterest;
        set
        {
            if (this.SetProperty(ref this.myAnnualInterest, value))
            {
                this.myMonthlyInterest = Math.Round((Math.Pow(1 + value / 100, 1.0 / 12) - 1) * 100, 2);
                this.OnPropertyChanged(nameof(this.MonthlyInterest));
                this.CalculateFee();
            }
        }
    }

    public string DebtorName
    {
        get => this.myDebtorName;
        set
        {
            if (this.SetProperty(ref this.myDebtorName, value))
            {
                this.OkCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public double MonthlyInterest
    {
        get => this.myMonthlyInterest;
        set
        {
            if (this.SetProperty(ref this.myMonthlyInterest, value))
            {
                this.myAnnualInterest = Math.Round((Math.Pow(1 + value / 100, 12) - 1) * 100, 2);
                this.OnPropertyChanged(nameof(this.AnnualInterest));
                this.CalculateFee();
            }
        }
    }

    public decimal MonthlyFee
    {
        get => this.myMonthlyFee;
        private set => this.SetProperty(ref this.myMonthlyFee, value);
    }

    public int NumberInstalments
    {
        get => this.myNumberInstalments;
        set
        {
            if (this.SetProperty(ref this.myNumberInstalments, value))
            {
                this.CalculateFee();
            }
        }
    }
    
    public DialogResult DialogResult {
        get => this.myDialogResult;
        private set => this.SetProperty(ref this.myDialogResult, value);
    }

    public Loan? Loan
    {
        get => this.myLoan;
        private set => this.SetProperty(ref this.myLoan, value);
    }

    public RelayCommand CancelCommand { get; }

    public ICommand<(object sender, KeyPressEventArgs e)> NumericVerificationCommand { get; }

    public IObservableCommand OkCommand { get; }

    public AddLoanViewModel()
    {
        this.NumericVerificationCommand = TextBoxCommandBinderHelper.CreateValidateNumericCommand();
        this.OkCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.OK;
                this.Loan = this.CreateLoan();
                this.RequestClose?.Invoke();
            },
            canExecute: () => this.myMonthlyFee > 0 && this.myDebtorName.Length >= 3);
        this.CancelCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.RequestClose?.Invoke();
            });
    }

    public Loan CreateLoan()
    {
        return new Loan
        {
            AnnualInterest = this.myAnnualInterest / 100,
            CurrentBalance = this.myAmount,
            DebtorName = this.myDebtorName,
            DisbursementDate = DateTime.Now,
            Fee = Math.Round(this.myMonthlyFee),
            InitialAmount = this.myAmount,
            NumberInstalments = this.myNumberInstalments,
        };
    }

    private void CalculateFee()
    {
        var annualInterest = this.myAnnualInterest / 100;
        if (annualInterest > 0 && this.myNumberInstalments > 0)
        {
            var monthlyInterest = Math.Pow(1 + annualInterest, 1.0 / 12) - 1;
            this.MonthlyFee = this.myAmount * (decimal)monthlyInterest /
                (decimal)(1 - Math.Pow(1 + monthlyInterest, -this.myNumberInstalments));
        }
        else
        {
            this.MonthlyFee = 0;
        }

        this.OkCommand.RaiseCanExecuteChanged();
    }
}
