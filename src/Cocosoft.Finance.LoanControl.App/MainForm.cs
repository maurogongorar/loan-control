using Cocosoft.Finance.LoanControl.App.Dialogs;
using Cocosoft.Finance.LoanControl.Core.Resources;
using Cocosoft.Finance.LoanControl.Dal;
using Cocosoft.Finance.LoanControl.Dal.Model.Entities;

namespace Cocosoft.Finance.LoanControl.App
{
    internal partial class MainForm : Form
    {
        private Loan? myCurrentLoan;

        private readonly IDialogFactory myDialogFactory;

        private readonly ILocalizationService myLocalizer;

        private readonly IRepository myRepository;

        public MainForm(IRepository repository, IDialogFactory dialogFactory, ILocalizationService localizer)
        {
            this.myRepository = repository;
            this.myDialogFactory = dialogFactory;
            this.myLocalizer = localizer;
            InitializeComponent();
        }

        private void AddLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var prompt = this.myDialogFactory.CreateAddLoanDialogView();
            var result = prompt.ShowDialog(this);
            if (result != DialogResult.OK || prompt.ViewModel.Loan == null)
            {
                return;
            }

            this.myRepository.AddLoanAsync(prompt.ViewModel.Loan);
            this.myCurrentLoan = prompt.ViewModel.Loan;
            this.LoadLoan(this.myCurrentLoan.Id);
        }

        private void AddPaymentButton_Click(object sender, EventArgs e)
        {
            if (this.myCurrentLoan == null)
            {
                MessageBox.Show(
                    this,
                    this.myLocalizer["LoanNotLoadedMessage"],
                    this.myLocalizer["Warning"],
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var prompt = this.myDialogFactory.CreateAddPaymentDialogView(this.myCurrentLoan);
            var result = prompt.ShowDialog(this);

            if (result != DialogResult.OK || prompt.ViewModel.Payment == null)
            {
                return;
            }

            this.myCurrentLoan.CurrentBalance = prompt.ViewModel.Payment.NewBalance > 0 ? prompt.ViewModel.Payment.NewBalance : 0;
            this.myCurrentLoan.IsClosed = this.myCurrentLoan.CurrentBalance == 0;
            this.myCurrentLoan.InterestCollected += prompt.ViewModel.Payment.Interest;
            this.myCurrentLoan.LastPaymentDate = prompt.ViewModel.Payment.Date;

            using var tran = this.myRepository.BeginTransaction();
            try
            {
                this.myRepository.AddPaymentAsync(prompt.ViewModel.Payment);
                this.myRepository.UpdateLoanAsync(this.myCurrentLoan);
                tran.Commit();
                this.LoadLoan(this.myCurrentLoan.Id);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this.myLocalizer["UnhandledErrorMessage"],
                    this.myLocalizer["Error"],
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                tran.Rollback();
            }
        }

        private string GetDisplayInterest(double annualInterest)
            => $"{annualInterest:P} {this.myLocalizer["AnnualEffective"]} " +
            $"({Math.Pow(1 + annualInterest, 1.0 / 12) - 1:P} {this.myLocalizer["MonthlyEffective"]})";

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
            using var prompt = this.myDialogFactory.CreateSelectLoanDialogView();
            var result = prompt.ShowDialog(this);
            if (result == DialogResult.OK && prompt.ViewModel.SelectedId.HasValue)
            {
                this.LoadLoan(prompt.ViewModel.SelectedId.Value);
                this.addPaymentButton.Enabled = true;
            }
        }
    }
}
