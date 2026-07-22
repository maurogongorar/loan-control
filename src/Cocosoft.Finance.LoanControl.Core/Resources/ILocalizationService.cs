namespace Cocosoft.Finance.LoanControl.Core.Resources;

public interface ILocalizationService
{
    string this[string key] { get; }

    string GetString(string key);
}
