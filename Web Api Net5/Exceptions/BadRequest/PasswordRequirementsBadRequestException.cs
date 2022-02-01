using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class PasswordRequirementsBadRequestException : BaseBadRequestException
    {
        public PasswordRequirementsBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400002;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}