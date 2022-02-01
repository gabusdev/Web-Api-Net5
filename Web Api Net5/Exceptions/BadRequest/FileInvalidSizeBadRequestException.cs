using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class FileInvalidSizeBadRequestException : BaseBadRequestException
    {
        public FileInvalidSizeBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400006;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}