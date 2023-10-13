public class BadRequestAppException : AppException
{
    public BadRequestAppException(string message) : base(400, message) {}
    
}
