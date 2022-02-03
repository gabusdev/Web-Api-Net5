using Microsoft.Extensions.Localization;
using System;

namespace Services.Exceptions
{
    public class WrongForeignKeyBadRequestException : BaseBadRequestException
    {
        public WrongForeignKeyBadRequestException(string message = null, int customCode = 0) : base()
        {
            CustomCode = Convert.ToBoolean(customCode) ? customCode : 400001;
            CustomMessage = message ?? CustomCode.ToString();
        }
        public WrongForeignKeyBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}