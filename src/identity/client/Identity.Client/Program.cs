using Identity.Api.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(AccountController).Assembly;
builder.Services.AddControllers()
    .PartManager.ApplicationParts.Add(new AssemblyPart(assembly) );

builder.Services.AddSwaggerDocument();

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi3();
app.MapGet("/", () => "Hello World!");

app.Run();