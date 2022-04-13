using Microsoft.Extensions.Localization;
using System;

namespace Services.Exceptions
{
    public class UnauthorizedException : BaseUnauthorizedException
    {
        public UnauthorizedException(string message = null, int customCode = 0) : base()
        {
            CustomCode = Convert.ToBoolean(customCode) ? customCode : 401001;
            CustomMessage = message ?? CustomCode.ToString();
        }
        public UnauthorizedException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 401001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}