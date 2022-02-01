using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class UserNotFoundException : BaseNotFoundException
    {
        public UserNotFoundException() : base()
        {
            CustomCode = 404001;
            CustomMessage = CustomCode.ToString();
        }
        public UserNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}