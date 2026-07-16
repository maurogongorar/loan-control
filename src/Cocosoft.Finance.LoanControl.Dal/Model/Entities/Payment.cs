namespace Cocosoft.Finance.LoanControl.Dal.Model.Entities;

public class Payment
{
    public decimal Amount { get; set; }

    public decimal Capital { get; set; }

    public DateTime Date { get; set; }

   public int Id { get; set; }

    public decimal Interest { get; set; }

    public int LoanId { get; set; }

     public decimal NewBalance { get; set; }

    public Loan? Loan { get; set; }
}
