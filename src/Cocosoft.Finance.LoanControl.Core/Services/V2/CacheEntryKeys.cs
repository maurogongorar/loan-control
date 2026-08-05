namespace Cocosoft.Finance.LoanControl.Core.Services.V2;

internal static class CacheEntryKeys
{
    public const string ActiveLoans = "core.services.loan.active_loans_count";

    public const string CapitalCollected = "core.services.loan.total_capital_collected";

    public const string CurrentDuePaymentAmount = "core.services.loan.total_current_due_payment_amount";

    public const string CurrentPendingDuePaymentAmount = "core.services.loan.total_current_pending_due_payment_amount";

    public const string CustomersWithLoan = "core.services.customer.customers_with_loan";

    public const string NewCustomers = "core.services.customer.new_customers";

    public const string TotalCollected = "core.services.loan.total_collected";

    public const string TotalCustomers = "core.services.customer.total_customers";

    public const string TotalDebt = "core.services.loan.total_debt";

    public const string TotalInterestCollected = "core.services.loan.total_interest_collected";

    public const string TotalLoansGranted = "core.services.loan.total_loans_granted";
}
