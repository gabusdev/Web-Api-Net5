using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class UnauthorizedException : BaseUnauthorizedException
    {
        public UnauthorizedException() : base()
        {
            CustomCode = 401001;
            CustomMessage = CustomCode.ToString();
        }
        public UnauthorizedException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 401001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}