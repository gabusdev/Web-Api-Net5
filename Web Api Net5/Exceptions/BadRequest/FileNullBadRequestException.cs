using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class FileNullBadRequestException : BaseBadRequestException
    {
        public FileNullBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400005;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}