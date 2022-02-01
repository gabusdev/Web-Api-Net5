using Microsoft.Extensions.Localization;

namespace Services.Exceptions
{
    public class UserNotFoundException : BaseNotFoundException
    {
        public UserNotFoundException(string message = null) : base()
        {
            CustomCode = 404001;
            CustomMessage = message ?? CustomCode.ToString();
        }
        public UserNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}