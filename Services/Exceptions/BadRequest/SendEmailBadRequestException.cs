using Microsoft.Extensions.Localization;
using System.Text;


namespace Services.Exceptions
{

    public class SendEmailBadRequestException : BaseBadRequestException
    {
        public SendEmailBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400008;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}
