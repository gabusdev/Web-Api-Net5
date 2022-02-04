namespace BasicResponses
{
    public class ApiNotFoundResponse : ApiResponse

    {
        public ApiNotFoundResponse(object result)
            : base(404)
        {
            Result = result;
        }

        public object Result { get; }
    }
}