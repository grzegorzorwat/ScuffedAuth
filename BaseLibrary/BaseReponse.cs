namespace BaseLibrary
{
    public abstract class BaseResponse
    {
        protected BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private init; }

        public string Message { get; private init; }
    }
}