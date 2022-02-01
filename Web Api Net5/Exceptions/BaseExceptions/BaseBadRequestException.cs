using System.Net;

namespace CustomExceptions
{
    public class BaseBadRequestException : CustomBaseException
    {
        public BaseBadRequestException() : base()
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
        }
    }
}