using Microsoft.Extensions.Localization;

namespace Services.Exceptions
{
    public class EmptyEmailBadRequestException : BaseBadRequestException
    {
        public EmptyEmailBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400011;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}