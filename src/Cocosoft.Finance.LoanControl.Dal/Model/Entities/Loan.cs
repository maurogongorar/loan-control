namespace Cocosoft.Finance.LoanControl.Dal.Model.Entities;

public class Loan
{
    public double AnnualInterest { get; set; }

    public decimal CurrentBalance { get; set; }

    public string DebtorName { get; set; } = string.Empty;

    public DateTime DisbursementDate { get; set; }

    public decimal Fee { get; set; }

    public int Id { get; set; }

    public decimal InitialAmount { get; set; }

    public decimal InterestCollected { get; set; }

    public bool IsClosed { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public int NumberInstalments { get; set; }

    public ICollection<Payment> Payments { get; internal set; } = [];
}
