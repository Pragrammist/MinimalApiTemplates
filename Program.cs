using Infrastrucutre.StartConfiguration;


var builder = WebApplication.CreateBuilder(args);
builder.Services.InitServices();
var app = builder.Build();
app.InitMiddlewares();






app.MapGet("/hello", () => "Hello World");
app.MapGet("/notfound", handler: () => {
    throw new BadRequestAppException("some went wrong");
});

app.Run();

