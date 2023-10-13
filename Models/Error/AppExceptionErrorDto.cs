public class AppExceptionErrorDto
{
    public string Message { get; set; }

    public AppExceptionErrorDto(string message)
    {
        Message = message;
    }
}