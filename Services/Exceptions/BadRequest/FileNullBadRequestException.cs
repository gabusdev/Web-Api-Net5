using Microsoft.Extensions.Localization;

namespace Services.Exceptions
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