using eLibraryOnContainers.Identity.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerWithJwt()
    .AddOptions(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddApplicationServices()
    .AddControllers();

builder.Build()
    .UsePipeline()
    .Run();


