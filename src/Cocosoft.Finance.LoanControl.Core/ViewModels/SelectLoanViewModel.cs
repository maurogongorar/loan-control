using Cocosoft.Finance.LoanControl.Core.Resources;
using Cocosoft.Finance.LoanControl.Core.Services;
using Cocosoft.Framework.Mvvm;
using Cocosoft.Framework.Mvvm.Commands;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Cocosoft.Finance.LoanControl.Core.ViewModels;

public class SelectLoanViewModel : ViewModelBase, IViewModelSelectable
{
    private DialogResult myDialogResult;

    private bool myGetAllLoans;

    private readonly ILoanService myLoanService;

    private readonly ILocalizationService myLocalizer;

    private LoanRow? mySelectedLoan;

    public class LoanRow
    {
        public int Id { get; set; }

        public string? DebtorName { get; set; }

        public decimal InitialAmount { get; set; }

        public decimal CurrentBalance { get; set; }

        public string? IsClosed { get; set; }
    }

    public DialogResult DialogResult
    {
        get => this.myDialogResult;
        private set => this.SetProperty(ref this.myDialogResult, value);
    }

    public bool GetAllLoans
    {
        get => this.myGetAllLoans;
        set
        {
            if (this.SetProperty(ref this.myGetAllLoans, value))
            {
                this.LoadLoans(value);
            }
        }
    }

    public BindingList<LoanRow> Loans { get; } = new();

    public int? SelectedId => this.SelectedLoan?.Id;

    public LoanRow? SelectedLoan
    {
        get => this.mySelectedLoan;
        set
        {
            if (this.SetProperty(ref this.mySelectedLoan, value))
            {
                this.OkCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public ICommand CancelCommand { get; }

    public IObservableCommand OkCommand { get; }

    public SelectLoanViewModel(ILoanService loanService, ILocalizationService localizer)
    {
        this.myLoanService = loanService;
        this.myLocalizer = localizer;
        this.LoadLoans(getAll: false);

        this.OkCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.OK;
            },
            canExecute: () => this.SelectedLoan != null);
        this.CancelCommand = new RelayCommand(
            execute: () =>
            {
                this.DialogResult = DialogResult.Cancel;
            });
    }

    private void LoadLoans(bool getAll)
    {
        this.Loans.Clear();
        var loans = getAll ? this.myLoanService.GetAllLoans() : this.myLoanService.GetActiveLoans();
        foreach (var loan in loans)
        {
            this.Loans.Add(new LoanRow
            {
                Id = loan.Id,
                DebtorName = loan.DebtorName,
                InitialAmount = loan.InitialAmount,
                CurrentBalance = loan.CurrentBalance,
                IsClosed = loan.IsClosed ? this.myLocalizer["Yes"] : this.myLocalizer["No"]
            });
        }
    }
}
