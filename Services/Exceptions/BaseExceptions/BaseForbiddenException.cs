using System.Net;

namespace Services.Exceptions
{
    public class BaseForbiddenException : CustomBaseException
    {
        public BaseForbiddenException() : base()
        {
            HttpCode = (int)HttpStatusCode.Forbidden;
        }
    }
}