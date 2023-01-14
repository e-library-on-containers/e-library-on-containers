using System.IdentityModel.Tokens.Jwt;
using System.Text;
using eLibraryOnContainers.Identity.Application.Authentication;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Application.Services;
using eLibraryOnContainers.Identity.Infrastructure.Common;
using eLibraryOnContainers.Identity.Infrastructure.Options;
using eLibraryOnContainers.Identity.Infrastructure.Repositories;
using eLibraryOnContainers.Identity.Infrastructure.Services;
using FunctionalValidation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "eLibraryOnContainers.Identity", Version = "v1" });

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            BearerFormat = "JWT",
            Scheme = "Bearer",
            Description = "Authorization token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
        });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
});

var authSection = builder.Configuration.GetSection("AuthOptions");
var authOptions = authSection.Get<AuthOptions>();
builder.Services.Configure<IAuthOptions>(authSection);
builder.Services.Configure<SqlOptions>(options =>
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton<IOptions<ISqlOptions>>(provider => provider.GetRequiredService<IOptions<SqlOptions>>());

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<ISqlConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddFunctionalValidation(typeof(Program).Assembly);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(authOptions.Key);

        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddMediatR(typeof(SignInQuery).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
