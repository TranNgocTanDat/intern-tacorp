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

        // Success responses
        public static APIResponse<T> Success(T data, string message = "Success")
        {
            return new APIResponse<T>(200, message, data);
        }

        public static APIResponse<T> Created(T data, string message = "Created")
        {
            return new APIResponse<T>(201, message, data);
        }

        public static APIResponse<T> NoContent(string message = "No Content")
        {
            return new APIResponse<T>(204, message);
        }

        // Client error responses
        public static APIResponse<T> BadRequest(string message = "Bad Request")
        {
            return new APIResponse<T>(400, message);
        }

        public static APIResponse<T> Unauthorized(string message = "Unauthorized")
        {
            return new APIResponse<T>(401, message);
        }

        public static APIResponse<T> Forbidden(string message = "Forbidden")
        {
            return new APIResponse<T>(403, message);
        }

        public static APIResponse<T> NotFound(string message = "Not Found")
        {
            return new APIResponse<T>(404, message);
        }

        // Server error responses
        public static APIResponse<T> InternalServerError(string message = "Internal Server Error")
        {
            return new APIResponse<T>(500, message);
        }

        // General Fail method (tuỳ biến code)
        public static APIResponse<T> Fail(string message, int statusCode = 400)
        {
            return new APIResponse<T>(statusCode, message);
        }
    }
}
