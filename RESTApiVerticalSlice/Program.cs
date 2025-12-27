using RESTApiVerticalSlice.Common.Logging;
using RESTApiVerticalSlice.Features.Products.Data;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddControllers();

var app = builder.Build();

app.UseRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => Results.Ok(new { Message = "REST API - Vertical Slice" }))
    .WithMetadata(new LogAttribute("Root", LogLevel.Debug));

app.MapControllers();

app.Run();

