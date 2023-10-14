using Infrastrucutre.MappingConfiguration;
using MongoDB.Driver;

namespace Infrastrucutre.StartConfiguration;

public static class DefaultServicesConfigurationExtensions{
    public static IServiceCollection InitServices(this IServiceCollection services)
    {   
        InitMappster();
        //services.AddControllers();
        services.AddSwaggerServices();
        services.AddCors();
        services.AddMongoDb();
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
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        var service = services.BuildServiceProvider().GetRequiredService<IConfiguration>() 
            ?? throw new AppConfigurationException("Config service is null");

        var connection = service["MONGO"] ?? "mongodb://localhost:27017";

        if(string.IsNullOrEmpty(connection))
            throw new AppConfigurationException("connection conf is empty");

        var dbName = service["DB_NAME"] ?? "mymongodb";

        if(string.IsNullOrEmpty(dbName))
            throw new AppConfigurationException("dbName conf is empty");

        services.AddSingleton<IMongoClient>(p =>
        {
            var mongoClient = new MongoClient(connection);
            return mongoClient;
        });
        services.AddSingleton(p =>
        {
            var mongo = p.GetRequiredService<IMongoClient>();
            var db = mongo.GetDatabase(dbName);
            MongoDbBuilder.Build();
            return db;
        });
        
        
        
        //example how add new collection with repository
        //collection per repository
        services.AddSingleton<IMongoCollection<EntityBase>>(p =>
        {
            var db = p.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<EntityBase>(nameof(EntityBase) + "_collention");
        });
        services.AddTransient<IRepository<EntityBase>, Repository<EntityBase>>();
        return services;
    }
}