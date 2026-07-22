using Microsoft.Extensions.Localization;

namespace Cocosoft.Finance.LoanControl.Core.Resources;

internal class LocalizationService(IStringLocalizer<LoanControlResources> localizer) : ILocalizationService
{
    public string this[string key] => localizer[key];

    public string GetString(string key) => localizer.GetString(key);
}
