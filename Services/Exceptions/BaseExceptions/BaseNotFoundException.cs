using System.Net;

namespace Services.Exceptions
{
    public class BaseNotFoundException : CustomBaseException
    {
        public BaseNotFoundException() : base()
        {
            HttpCode = (int)HttpStatusCode.NotFound;
        }
    }
}