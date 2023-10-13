public class AppException : Exception
{

    
    public AppException(int statusCode, string message) : base($"Some error: {message}")
    {
        HttpStatusCode = statusCode;
    }
    public int HttpStatusCode { get; set; }
    
}
