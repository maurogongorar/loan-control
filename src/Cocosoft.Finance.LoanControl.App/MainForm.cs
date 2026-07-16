using Cocosoft.Finance.LoanControl.App.Dialogs;
using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;
using System.ComponentModel;

namespace Cocosoft.Finance.LoanControl.App
{
    internal partial class MainForm : Form
    {
        private Loan? myCurrentLoan;

        private readonly IDialogFactory myDialogFactory;

        private readonly IRepository myRepository;

        private readonly ComponentResourceManager myResources = new(typeof(Program));

        public MainForm(IRepository repository, IDialogFactory dialogFactory)
        {
            this.myRepository = repository;
            this.myDialogFactory = dialogFactory;
            InitializeComponent();
        }

        private void AddLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var prompt = this.myDialogFactory.CreateAddLoanDialogView();
            var result = prompt.ShowDialog(this);
            if (result != DialogResult.OK || prompt.Loan == null)
            {
                return;
            }

            this.myRepository.AddLoanAsync(prompt.Loan);
            this.myCurrentLoan = prompt.Loan;
            this.LoadLoan(this.myCurrentLoan.Id);
        }

        private void AddPaymentButton_Click(object sender, EventArgs e)
        {
            if (this.myCurrentLoan == null)
            {
                MessageBox.Show(
                    this,
                    this.myResources.GetString("LoanNotLoadedMessage"),
                    this.myResources.GetString("Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var prompt = this.myDialogFactory.CreateAddPaymentDialogView(this.myCurrentLoan);
            var result = prompt.ShowDialog(this);

            if (result != DialogResult.OK || prompt.Payment == null)
            {
                return;
            }

            this.myCurrentLoan.CurrentBalance = prompt.Payment.NewBalance > 0 ? prompt.Payment.NewBalance : 0;
            this.myCurrentLoan.IsClosed = this.myCurrentLoan.CurrentBalance == 0;
            this.myCurrentLoan.InterestCollected += prompt.Payment.Interest;
            this.myCurrentLoan.LastPaymentDate = prompt.Payment.Date;

            using var tran = this.myRepository.BeginTransaction();
            try
            {
                this.myRepository.AddPaymentAsync(prompt.Payment);
                this.myRepository.UpdateLoanAsync(this.myCurrentLoan);
                tran.Commit();
                this.LoadLoan(this.myCurrentLoan.Id);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this.myResources.GetString("UnhandledErrorMessage"),
                    this.myResources.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                tran.Rollback();
            }
        }

        private string GetDisplayInterest(double annualInterest)
            => $"{annualInterest:P} {this.myResources.GetString("AnnualEffective")} " +
            $"({Math.Pow(1 + annualInterest, 1.0 / 12) - 1:P} {this.myResources.GetString("MonthlyEffective")})";

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void LoadLoan(int loanId)
        {
            this.myCurrentLoan = this.myRepository.GetLoan(loanId);
            this.addPaymentButton.Enabled = !this.myCurrentLoan.IsClosed;
            this.debtorNameTextBox.Text = this.myCurrentLoan.DebtorName;
            this.initialLoanTextBox.Text = this.myCurrentLoan.InitialAmount.ToString("C");
            this.currentDebtTextBox.Text = this.myCurrentLoan.CurrentBalance.ToString("C");
            this.disbursementDateTextBox.Text = this.myCurrentLoan.DisbursementDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.interestTextBox.Text = this.GetDisplayInterest(this.myCurrentLoan.AnnualInterest);
            this.instalmentsPaidTextBox.Text = $"{this.myRepository.GetLoanPayments(loanId).Count()}/" +
                $"{this.myCurrentLoan.NumberInstalments}";
            this.feeTextBox.Text = this.myCurrentLoan.Fee.ToString("C");
            this.interestcollectedTextBox.Text = this.myCurrentLoan.InterestCollected.ToString("C");

            this.paymentsDataGridView.Rows.Clear();
            foreach (var payment in this.myRepository.GetLoanPayments(loanId))
            {
                this.paymentsDataGridView.Rows.Add([
                    payment.Date.ToString("yyy-MM-dd HH:mm:ss"),
                    payment.Amount.ToString("C"),
                    payment.Interest.ToString("C"),
                    payment.Capital.ToString("C"),
                    payment.NewBalance.ToString("C")]);
            }
        }

        private void OpenLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var prompt = this.myDialogFactory.CreateSelectLoanDialogView(this.myRepository);
            var result = prompt.ShowDialog(this);
            if (result == DialogResult.OK && prompt.SelectedId.HasValue)
            {
                this.LoadLoan(prompt.SelectedId.Value);
                this.addPaymentButton.Enabled = true;
            }
        }
    }
}
