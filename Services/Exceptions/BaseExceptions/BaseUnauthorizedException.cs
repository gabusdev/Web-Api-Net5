using System.Net;

namespace Services.Exceptions
{
    public class BaseUnauthorizedException : CustomBaseException
    {
        public BaseUnauthorizedException() : base()
        {
            HttpCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}