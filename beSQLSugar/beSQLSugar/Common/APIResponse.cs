namespace beSQLSugar.Common
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public APIResponse(int statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static APIResponse<T> Success(T data, string message = "Success")
        {
            return new APIResponse<T>(200, message, data);
        }

        public static APIResponse<T> Fail(string message, int statusCode = 400)
        {
            return new APIResponse<T>(statusCode, message, default);
        }
    }
}
