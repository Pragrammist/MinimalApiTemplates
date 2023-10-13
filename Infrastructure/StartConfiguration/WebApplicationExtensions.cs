

public static class WebApplicationExtensions{
    public static WebApplication InitMiddlewares(this WebApplication app)
    {
        app.UseMyCorsPolicy();
        
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        //app.UseAuthentication();
        //app.UseAuthorization();         
        app.UseMiddleware<AppExceptionHandlerMiddleware>();
        //app.MapControllers();
        
        return app;
    }

    public static WebApplication UseMyCorsPolicy(this WebApplication app)
    {
        var allowedHosts = app.Configuration["allowedHosts"] ?? "*";

        if(string.IsNullOrEmpty(allowedHosts))
            throw new AppConfigurationException("allowedHosts is null");
            
        app.UseCors(c => c.AllowAnyMethod().AllowAnyHeader().WithOrigins(allowedHosts));

        return app;
    }
}