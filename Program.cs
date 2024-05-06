using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://localhost:4200");
        });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var todoItems = new List<string>
            {
                "Complete assignment",
                "Buy groceries",
                "Finish project"
            };

app.MapGet("/gettasks", () =>
{
    return todoItems;
})
.WithName("GetTask")
.WithOpenApi();

app.MapDelete("/removetask", (string taskName) =>
{
    todoItems.Remove(taskName);
    return "ok";
})
.WithName("DeleteTask")
.WithOpenApi();

app.MapPut("/addtask", (string taskName) =>
{
    todoItems.Add(taskName);
    return "ok";
})
.WithName("AddTask")
.WithOpenApi();

app.Run();
