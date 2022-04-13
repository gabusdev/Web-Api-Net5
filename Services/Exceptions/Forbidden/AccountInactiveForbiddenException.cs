using Microsoft.Extensions.Localization;

namespace Services.Exceptions
{
    public class AccountInactiveForbiddenException : BaseForbiddenException
    {
        public AccountInactiveForbiddenException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 403001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}