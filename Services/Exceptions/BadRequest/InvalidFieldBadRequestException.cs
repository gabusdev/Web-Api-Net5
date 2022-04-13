using System;

namespace Services.Exceptions
{
    public class InvalidFieldBadRequestException : BaseBadRequestException
    {
        public InvalidFieldBadRequestException(string message = null, int customCode = 0) : base()
        {
            CustomCode = Convert.ToBoolean(customCode) ? customCode : 400000;
            CustomMessage = message ?? CustomCode.ToString();
        }

    }
}