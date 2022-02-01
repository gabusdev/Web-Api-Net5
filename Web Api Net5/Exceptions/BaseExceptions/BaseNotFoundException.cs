using System.Net;

namespace CustomExceptions
{
    public class BaseNotFoundException : CustomBaseException
    {
        public BaseNotFoundException() : base()
        {
            HttpCode = (int)HttpStatusCode.NotFound;
        }
    }
}