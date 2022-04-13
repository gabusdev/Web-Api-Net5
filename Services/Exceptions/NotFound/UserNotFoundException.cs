using Microsoft.Extensions.Localization;
using System;

namespace Services.Exceptions
{
    public class UserNotFoundException : BaseNotFoundException
    {
        public UserNotFoundException(string message = null, int customCode = 0) : base()
        {
            CustomCode = Convert.ToBoolean(customCode) ? customCode : 404001;
            CustomMessage = message ?? CustomCode.ToString();
        }
        public UserNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}