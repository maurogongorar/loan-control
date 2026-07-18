using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using System.Globalization;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class AddPaymentDialogForm : Form, IAddPaymentDialog
{
    private readonly decimal myInterestDue;

    private decimal myCapitalPayment;

    private readonly Loan myLoan;

    public Payment? Payment { get; private set; }

    public AddPaymentDialogForm(Loan loan)
    {
        this.myLoan = loan;

        InitializeComponent();

        var now = DateTime.Now;
        if (now.Day == 31)
        {
            now = now.AddDays(-1);
        }

        this.myInterestDue = AddPaymentDialogForm.CalculateInterestDue(loan);
        this.interestTextBox.Text = this.myInterestDue.ToString("C");
        this.feeTextBox.Text = loan.Fee.ToString();
    }

    private static decimal CalculateInterestDue(Loan loan)
    {
        var now = DateTime.Now;
        var lastPaymentDate = loan.LastPaymentDate ?? loan.DisbursementDate;
        var daysSinceLastPayment = AddPaymentDialogForm.CountDays(lastPaymentDate, now);
        var interestRate = Math.Pow(1 + loan.AnnualInterest, daysSinceLastPayment / 360.0) - 1;
        return Math.Round(loan.CurrentBalance * (decimal)interestRate, 2);
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

    private void CancelButton_Click(object sender, EventArgs e) => this.Close();

    private void FeeTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        // allows 0-9, backspace, and decimal
        if ((e.KeyChar < 48 || e.KeyChar > 57) &&
            e.KeyChar != 8 &&
            e.KeyChar != CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0])
        {
            e.Handled = true;
            return;
        }

        // checks to make sure only 1 decimal is allowed
        if (e.KeyChar == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] &&
            ((TextBox)sender).Text.Contains(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void FeeTextBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        if (!decimal.TryParse(textBox.Text, out var fee))
        {
            textBox.Text = string.Empty;
            fee = 0;
        }

        this.myCapitalPayment = fee - this.myInterestDue;
        this.capitalTextBox.Text = this.myCapitalPayment.ToString("C");
        this.newBalanceTextBox.Text = (this.myLoan.CurrentBalance - this.myCapitalPayment).ToString("C");

        this.okButton.Enabled = this.myCapitalPayment > 0;
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Payment = new Payment
        {
            Amount = decimal.Parse(this.feeTextBox.Text),
            Capital = this.myCapitalPayment,
            Date = DateTime.Now,
            Interest = this.myInterestDue,
            LoanId = this.myLoan.Id,
            NewBalance = this.myLoan.CurrentBalance - this.myCapitalPayment
        };

        this.Close();
    }
}
