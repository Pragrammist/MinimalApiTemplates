public class AppExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;

    public AppExceptionHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = ex.HttpStatusCode;
            var error = new AppExceptionErrorDto(ex.Message);
            await context.Response.WriteAsJsonAsync(error);
        }
        catch
        {
            throw;
        }
    }
}