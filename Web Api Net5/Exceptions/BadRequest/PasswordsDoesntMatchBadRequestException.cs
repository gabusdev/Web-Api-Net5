using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class PasswordsDoesntMatchBadRequestException : BaseBadRequestException
    {
        public PasswordsDoesntMatchBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400003;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}