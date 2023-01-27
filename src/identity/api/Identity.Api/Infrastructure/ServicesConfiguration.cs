using FunctionalValidation.Extensions;
using Identity.Application.Authentication;
using Identity.Application.Repositories;
using Identity.Application.Services;
using Identity.Infrastructure.Common;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using MediatR;

namespace Identity.Api.Infrastructure;

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