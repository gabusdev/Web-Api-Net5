using Microsoft.Extensions.Localization;

namespace Services.Exceptions
{
    public class UnauthorizedException : BaseUnauthorizedException
    {
        public UnauthorizedException(string message = null) : base()
        {
            CustomCode = 401001;
            CustomMessage = message ?? CustomCode.ToString();
        }
        public UnauthorizedException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 401001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}