using System.Net;

namespace CustomExceptions
{
    public class BaseUnauthorizedException : CustomBaseException
    {
        public BaseUnauthorizedException() : base()
        {
            HttpCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}