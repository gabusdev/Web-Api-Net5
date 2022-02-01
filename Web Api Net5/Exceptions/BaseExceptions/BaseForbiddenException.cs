using System.Net;

namespace CustomExceptions
{
    public class BaseForbiddenException : CustomBaseException
    {
        public BaseForbiddenException() : base()
        {
            HttpCode = (int)HttpStatusCode.Forbidden;
        }
    }
}