using eLibraryOnContainers.Identity.Application.Authentication;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Application.Services;
using eLibraryOnContainers.Identity.Infrastructure.Common;
using eLibraryOnContainers.Identity.Infrastructure.Repositories;
using eLibraryOnContainers.Identity.Infrastructure.Services;
using FunctionalValidation.Extensions;
using MediatR;

namespace eLibraryOnContainers.Identity.Api.Infrastructure;

public static class ServicesConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services.AddFunctionalValidation(typeof(Program).Assembly)
            .AddMediatR(typeof(SignInQuery).Assembly)
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddScoped<IRolesService, RolesService>()
            .AddScoped<ISqlConnectionFactory, NpgsqlConnectionFactory>();
}