using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class OldPasswordIncorrectBadRequestException : BaseBadRequestException
    {
        public OldPasswordIncorrectBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400004;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}