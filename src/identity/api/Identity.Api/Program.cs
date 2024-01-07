using Identity.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerWithJwt()
    .AddOptions(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddConsulServices(builder.Configuration)
    .AddApplicationServices()
    .AddCors()
    .AddControllers();

builder.Build()
    .UsePipeline()
    .Run();


