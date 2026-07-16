using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using System.Globalization;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class AddLoanDialogForm : Form, IAddLoanView
{
    private decimal myAmount;

    private double myAnnualInterest = 0.12;

    private decimal myFee;

    private double myMonthlyInterest = Math.Pow(1.12, 1.0 / 12) - 1;

    private int myNumberInstalments = 12;

    public Loan? Loan { get; private set; }

    public AddLoanDialogForm()
    {
        InitializeComponent();
        this.monthlyInterestTextBox.Text = Math.Round(this.myMonthlyInterest * 100, 2).ToString();
    }

    private void AmountTextBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        if (!decimal.TryParse(textBox.Text, out this.myAmount))
        {
            textBox.Text = string.Empty;
            this.myAmount = 0;
        }

        this.CalculateFee();
    }

    private void AnnualInterestTextBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;

        if (!double.TryParse(textBox.Text, out this.myAnnualInterest))
        {
            textBox.Text = string.Empty;
            this.myAnnualInterest = 0;
        }

        this.myAnnualInterest /= 100;

        if (textBox.Focused)
        {
            this.myMonthlyInterest = Math.Pow(1 + this.myAnnualInterest, 1.0 / 12) - 1;
            this.monthlyInterestTextBox.Text = Math.Round(this.myMonthlyInterest * 100, 2).ToString();
        }

        this.CalculateFee();
    }

    private void CalculateFee()
    {
        if (this.myAnnualInterest > 0 && this.myNumberInstalments > 0)
        {
            var monthlyInterest = Math.Pow(1 + this.myAnnualInterest, 1.0 / 12) - 1;
            this.myFee = this.myAmount * (decimal)monthlyInterest /
                (decimal)(1 - Math.Pow(1 + monthlyInterest, -this.myNumberInstalments));
            this.monthlyFeeTextBox.Text = this.myFee.ToString("C");
        }
        else
        {
            this.myFee = 0;
        }

        this.okButton.Enabled = this.myFee > 0 && this.debtorNameTextBox.Text.Length >= 3;
    }

    private void CancelButton_Click(object sender, EventArgs e) => this.Close();

    private void MonthlyInterestTextBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        if (!double.TryParse(textBox.Text, out this.myMonthlyInterest))
        {
            textBox.Text = string.Empty;
            this.myMonthlyInterest = 0;
        }

        this.myMonthlyInterest /= 100;

        if (textBox.Focused)
        {
            this.myAnnualInterest = Math.Pow(1 + this.myMonthlyInterest, 12) - 1;
            this.annualInterestTextBox.Text = Math.Round(this.myAnnualInterest * 100, 2).ToString();
        }

        this.CalculateFee();
    }

    private void NumberInstalmentsTextBox_TextChanged(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        if (!int.TryParse(textBox.Text, out this.myNumberInstalments))
        {
            textBox.Text = string.Empty;
            this.myNumberInstalments = 0;
        }

        this.CalculateFee();
    }

    private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        var textBox = (TextBox)sender;
        // allows 0-9, backspace, and decimal, if allowed
        if ((e.KeyChar < 48 || e.KeyChar > 57) &&
            e.KeyChar != 8 &&
            (textBox.Tag?.ToString()?.Equals("integer") == true ||
            e.KeyChar != CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]))
        {
            e.Handled = true;
            return;
        }

        // checks to make sure only 1 decimal is allowed
        if (e.KeyChar == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] &&
            textBox.Text.Contains(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Loan = new Loan
        {
            AnnualInterest = this.myAnnualInterest,
            CurrentBalance = this.myAmount,
            DebtorName = this.debtorNameTextBox.Text,
            DisbursementDate = DateTime.Now,
            Fee = Math.Round(this.myFee),
            InitialAmount = this.myAmount,
            NumberInstalments = this.myNumberInstalments,
        };

        this.Close();
    }
}
