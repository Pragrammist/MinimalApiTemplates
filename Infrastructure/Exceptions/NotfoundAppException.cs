
public class NotfoundAppException : AppException
{
    public NotfoundAppException(string message) : base(404, message) {}
    
}