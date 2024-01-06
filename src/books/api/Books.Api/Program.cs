using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Books.Infrastructure.Repositories;
using MediatR;
using System.Reflection;
using Books.Core.RabitMQ;
using Books.Core.GetAll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(GetAllBooksQuery).GetTypeInfo().Assembly);
builder.Services.AddScoped<IRepository<Audiobook>, AudiobooksRepository>();
builder.Services.AddScoped<IRepository<Person>, PeopleRepository>();
builder.Services.AddTransient<IBookRepository<Book>, BooksRepository>();
builder.Services.AddTransient<IBookRepository<BookRead>, BooksReadRepository>();
builder.Services.AddTransient<IBookInstancesRepository<BookInstance>, BookInstancesRepository>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddHostedService<RabbitMQWorker>();

builder.Services.AddCors();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
