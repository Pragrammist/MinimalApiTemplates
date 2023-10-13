
public static class DefaultServicesConfigurationExtensions{
    public static IServiceCollection InitServices(this IServiceCollection services)
    {   
        //services.AddControllers();
        services.AddSwaggerServices();
        services.AddCors();
        return services;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => {
            var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
        });
        services.AddSwaggerGen();
        return services;
    }
}