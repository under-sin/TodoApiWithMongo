using TodoApiWithMongo;
using TodoApiWithMongo.Repositories;
using TodoApiWithMongo.Data;

var builder = WebApplication.CreateBuilder(args);

// configura o swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoConfiguration>(builder.Configuration.GetSection("MongoConfiguration"));

builder.Services.AddSingleton<IMongoContext,  MongoContext>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

var app = builder.Build();
app.MapTodoModelEndpoints();
app.MapPersonModelEndpoints();

// Use o middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api With Mongodb");
    });
}

app.Run();
