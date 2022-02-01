using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    internal class FileInvalidTypeBadRequestException : BaseBadRequestException
    {
        public FileInvalidTypeBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400007;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}