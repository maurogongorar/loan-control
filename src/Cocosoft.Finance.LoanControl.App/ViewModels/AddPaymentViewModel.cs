using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using Cocosoft.Framework.Mvvm;
using Cocosoft.Framework.Mvvm.Commands;
using Cocosoft.Framework.Mvvm.Helpers;
using System.Windows.Input;

namespace Cocosoft.Finance.LoanControl.App.ViewModels;

public class AddPaymentViewModel : ViewModelBase
{
    private decimal myCapitalPayment;

    private DialogResult myDialogResult;

    private decimal myFee;

    private decimal myInterestDue;

    private decimal myNewBalance;

    private Payment? myPayment;

    public decimal CapitalPayment
    {
        get => this.myCapitalPayment;
        private set
        {
            if (this.myCapitalPayment != value)
            {
                this.myCapitalPayment = value;
                this.OnPropertyChanged(nameof(this.CapitalPayment));
            }
        }
    }

    public DialogResult DialogResult
    {
        get => this.myDialogResult;
        private set => this.SetProperty(ref this.myDialogResult, value);
    }

    public decimal Fee
    {
        get => this.myFee;
        set
        {
            if (this.myFee != value)
            {
                this.myFee = value;
                this.CalculatePayment();
                this.OnPropertyChanged(nameof(this.Fee));
            }
        }
    }

    public decimal InterestDue
    {
        get => this.myInterestDue;
        private set
        {
            if (this.myInterestDue != value)
            {
                this.myInterestDue = value;
                this.OnPropertyChanged(nameof(this.InterestDue));
            }
        }
    }

    public Loan? Loan { get; private set; }

    public decimal NewBalance
    {
        get => this.myNewBalance;
        private set
        {
            if (this.myNewBalance != value)
            {
                this.myNewBalance = value;
                this.OnPropertyChanged(nameof(this.NewBalance));
            }
        }
    }

    public Payment? Payment
    {
        get => this.myPayment;
        private set => this.SetProperty(ref this.myPayment, value);
    }

    public ICommand CancelCommand { get; }

    public ICommand<(object sender, KeyPressEventArgs e)> NumericVerificationCommand { get; }

    public IObservableCommand OkCommand { get; }

    public AddPaymentViewModel()
    {
        this.NumericVerificationCommand = TextBoxCommandBinderHelper.CreateValidateNumericCommand();
        this.OkCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.OK;
                this.Payment = new Payment
                {
                    Amount = this.Fee,
                    Capital = this.CapitalPayment,
                    Date = DateTime.Now,
                    Interest = this.InterestDue,
                    LoanId = this.Loan?.Id ?? 0,
                    NewBalance = (this.Loan?.CurrentBalance ?? 0) - this.CapitalPayment
                };
            },
            canExecute: () => this.Loan != null && this.myCapitalPayment > 0);
        this.CancelCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.Cancel;
            });
    }

    public void SetLoan(Loan loan)
    {
        this.Loan = loan;
        this.InterestDue = CalculateInterestDue();
        this.Fee = this.Loan.Fee;
        this.OkCommand.RaiseCanExecuteChanged();
    }

    private decimal CalculateInterestDue()
    {
        if (this.Loan == null)
        {
            return 0;
        }

        var now = DateTime.Now;
        var lastPaymentDate = this.Loan.LastPaymentDate ?? this.Loan.DisbursementDate;
        var daysSinceLastPayment = CountDays(lastPaymentDate, now);
        var interestRate = Math.Pow(1 + this.Loan.AnnualInterest, daysSinceLastPayment / 360.0) - 1;
        return Math.Round(this.Loan.CurrentBalance * (decimal)interestRate, 2);
    }

    private void CalculatePayment()
    {
        this.CapitalPayment = this.Fee - this.myInterestDue;
        this.NewBalance = (this.Loan?.CurrentBalance ?? 0) - this.myCapitalPayment;
        this.OkCommand.RaiseCanExecuteChanged();
    }

    private static int CountDays(DateTime startDate, DateTime endDate)
    {
        var startDay = startDate.Day;
        var endDay = endDate.Day;

        if (startDay == DateTime.DaysInMonth(startDate.Year, startDate.Month))
        {
            startDay = 30;
        }

        if (endDay == DateTime.DaysInMonth(endDate.Year, endDate.Month))
        {
            if (startDay == 30)
            {
                endDate = endDate.AddDays(1);
                endDay = 1;
            }
            else
            {
                endDay = 30;
            }
        }

        var days = (endDate.Year - startDate.Year) * 360 + (endDate.Month - startDate.Month) * 30 + (endDay - startDay);
        return days;
    }
}
