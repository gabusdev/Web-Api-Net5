using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class AccountDeactivatedForbiddenException : BaseForbiddenException
    {
        public AccountDeactivatedForbiddenException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 403002;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}