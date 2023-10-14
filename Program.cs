using Infrastrucutre.StartConfiguration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InitServices();
var app = builder.Build();
app.InitMiddlewares();






app.MapGet("/hello", () => "Hello World");
app.MapGet("/notfound", handler: () => {
    throw new BadRequestAppException("some went wrong");
});

app.MapPost("/create", async (IRepository<EntityBase> c) => {
    var res = await c.Insert(new EntityBase {});

    return Results.Ok(res);
});

app.MapGet("/get", async (IRepository<EntityBase> c, string id) => {
    var res = await c.GetById(id);

    return Results.Ok(res);
});

app.MapGet("/delete", async (IRepository<EntityBase> c, string id) => {
    await c.Delete(id);

    return Results.Ok();
});

app.MapGet("/set", (IRepository<EntityBase> c) => {
    

    return c.Set();
});

app.MapPut("/update", async (IRepository<EntityBase> c, [FromBody]EntityBase ent) => {
    await c.Update(ent);

    return Results.Ok();
});

app.Run();

