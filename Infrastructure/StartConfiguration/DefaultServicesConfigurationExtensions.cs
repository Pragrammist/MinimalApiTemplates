using EFCore;
using Infrastrucutre.MappingConfiguration;
using Microsoft.EntityFrameworkCore;



namespace Infrastrucutre.StartConfiguration;

public static class DefaultServicesConfigurationExtensions{
    public static IServiceCollection InitServices(this IServiceCollection services)
    {   
        InitMappster();
        //services.AddControllers();
        services.AddSwaggerServices();
        services.AddCors();
        services.AddEfCore();
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
    public static void InitMappster()
    {
        MapsterConfiguration.SetMapping();
    }

    public static IServiceCollection AddEfCore(this IServiceCollection services)
    {
        var conf = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connenction = conf["POSTGRESQL"] ?? @"Host=localhost;Database=postgres;Username=postgres;Password=postgres";
        if(string.IsNullOrEmpty(connenction))
            throw new AppConfigurationException("Connection string is null or empty");
        services.AddDbContextPool<OverallDbContext>(options => options.UseNpgsql(connenction));
        services.AddTransient<IRepository<int>, RepositoryIntIdImpl>();
        return services;
    }
}