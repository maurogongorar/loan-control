using Cocosoft.Finance.LoanControl.Dal;
using System.ComponentModel;

namespace Cocosoft.Finance.LoanControl.App.Dialogs;

public partial class SelectLoanDialogForm : Form, IDialogSelectable
{
    private readonly IRepository myRepository;

    public int? SelectedId { get; private set; }

    public SelectLoanDialogForm(IRepository repository)
    {
        this.myRepository = repository;
        InitializeComponent();
    }

    private void CancelButton_Click(object sender, EventArgs e) => this.Close();

    private void GetAllLoansCheckBox_CheckedChange(object sender, EventArgs e) => this.LoadLoans(((CheckBox)sender).Checked);

    private void LoadLoans(bool getAll = false)
    {
        var resources = new ComponentResourceManager(typeof(Program));
        this.loansDataGridView.Rows.Clear();
        foreach (var row in this.myRepository.GetLoans(getAll))
        {
            this.loansDataGridView.Rows.Add([
                row.Id,
                row.DebtorName,
                row.InitialAmount.ToString("C"),
                row.CurrentBalance.ToString("C"),
                row.IsClosed ? resources.GetString("Yes")! : resources.GetString("No")!]);
        }
    }

    private void LoansDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        if (this.loansDataGridView.SelectedRows.Count > 0)
        {
            this.SelectedId = (int)this.loansDataGridView.SelectedRows[0].Cells["loanIdDataGridColumn"].Value!;
            this.okButton.Enabled = true;
            return;
        }

        this.SelectedId = null;
        this.okButton.Enabled = false;
    }

    private void OkButton_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    private void SelectLoanDialogForm_Load(object sender, EventArgs e) => this.LoadLoans();
}