namespace Cocosoft.Finance.LoanControl.Dal;

public interface ITransaction : IDisposable
{
    void Commit();

    void Rollback();
}