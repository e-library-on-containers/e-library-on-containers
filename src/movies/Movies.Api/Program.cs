using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Movies.Api.Consul;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConsulOptions>(builder.Configuration.GetConsulSection())
    .AddSingleton<IOptions<IConsulOptions>>(provider => provider.GetRequiredService<IOptions<ConsulOptions>>())
    .AddConsulServices(builder.Configuration);


var connectionString = builder.Configuration.GetConnectionString("DapperConnection")!;
var moviesRepository = new MoviesRepository(connectionString);
var peopleRepository = new PeopleRepository(connectionString);

builder.Services.AddSingleton(moviesRepository);
builder.Services.AddSingleton(peopleRepository);

var app = builder.Build();

app.MapPost("/movies", (MoviesRepository repository, Movie movie) =>
{
    repository.AddMovie(movie);
    return Results.Created("/movies", movie);
});

app.MapGet("/movies", (MoviesRepository repository) =>
{
    var movies = repository.GetAllMovies();
    return Results.Ok(movies);
});

app.MapGet("/movies/{id}", (MoviesRepository repository, int id) =>
{
    var movie = repository.GetByIdWithNames(id);
    
    return movie == null ? Results.NotFound() : Results.Ok(movie);
});

app.MapPut("/movies/{id}/publish", (MoviesRepository repository, int id) =>
{
    var movie = repository.GetById(id);
    movie.InPreview = false;
    repository.UpdateMovie(movie);
    return Results.NoContent();
});

app.MapDelete("/movies/{id}", (MoviesRepository repository, int id) =>
{
    repository.RemoveMovie(id);
    return Results.NoContent();
});

app.MapGet("/people", (PeopleRepository repository) =>
{
    var people = repository.GetPeople();
    return Results.Ok(people);
});


app.MapPost("/people", (PeopleRepository repository, Person person) =>
{
    repository.AddPerson(person);
    return Results.Created("/people", person);
});

app.MapDelete("/people/{id}", (PeopleRepository repository, int id) =>
{
    repository.RemovePerson(id);
    return Results.NoContent();
});

app.Run();

public class MovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool InPreview { get; set; }
    public List<string> Screenwriters { get; set; }
    public List<string> Directors { get; set; }
    public List<string> Actors { get; set; }
}

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool InPreview { get; set; }
    public List<int> ScreenwritersIds { get; set; }
    public List<int> DirectorsIds { get; set; }
    public List<int> ActorsIds { get; set; }
}

public class PeopleRepository
{
    private readonly IDbConnection _dbConnection;

    public PeopleRepository(string connectionString)
    {
        _dbConnection = new NpgsqlConnection(connectionString);
    }
    
    public IEnumerable<Person> GetPeople()
    {
        var sql = "SELECT * FROM People";
        return _dbConnection.Query<Person>(sql);
    }

    public void AddPerson(Person person)
    {
        var sql = "INSERT INTO People (Name, Surname) VALUES (@Name, @Surname)";
        _dbConnection.Execute(sql, person);
    }

    public void RemovePerson(int personId)
    {
        var sql = "DELETE FROM People WHERE PersonId = @PersonId";
        _dbConnection.Execute(sql, new { PersonId = personId });
    }
}

public class MoviesRepository
{
    private readonly IDbConnection _dbConnection;

    public MoviesRepository(string connectionString)
    {
        _dbConnection = new NpgsqlConnection(connectionString);
    }
    
    public IEnumerable<MovieDto> GetAllMovies()
    {
        var sql = @"
        SELECT
            m.Id,
            m.Title,
            m.Category,
            m.InPreview,
            s.Name + ' ' + s.Surname AS Screenwriter,
            d.Name + ' ' + d.Surname AS Director,
            a.Name + ' ' + a.Surname AS Actor
        FROM Movies m
        LEFT JOIN MovieScreenwriters ms ON m.Id = ms.MovieId
        LEFT JOIN Screenwriters s ON ms.ScreenwriterId = s.PersonId
        LEFT JOIN MovieDirectors md ON m.Id = md.MovieId
        LEFT JOIN Directors d ON md.DirectorId = d.PersonId
        LEFT JOIN MovieActors ma ON m.Id = ma.MovieId
        LEFT JOIN Actors a ON ma.ActorId = a.PersonId";
        
        var movies = _dbConnection.Query<MovieDto>(sql);
        return movies;
    }
    
    public MovieDto GetByIdWithNames(int id)
    {
        var sql = @"
        SELECT
            m.Id,
            m.Title,
            m.Category,
            m.InPreview,
            s.Name + ' ' + s.Surname AS Screenwriter,
            d.Name + ' ' + d.Surname AS Director,
            a.Name + ' ' + a.Surname AS Actor
        FROM Movies m
        LEFT JOIN MovieScreenwriters ms ON m.Id = ms.MovieId
        LEFT JOIN Screenwriters s ON ms.ScreenwriterId = s.PersonId
        LEFT JOIN MovieDirectors md ON m.Id = md.MovieId
        LEFT JOIN Directors d ON md.DirectorId = d.PersonId
        LEFT JOIN MovieActors ma ON m.Id = ma.MovieId
        LEFT JOIN Actors a ON ma.ActorId = a.PersonId
        WHERE m.Id = @MovieId";
        
        var movie = _dbConnection.QuerySingleOrDefault<MovieDto>(sql, new { MovieId = id });
        return movie;
    }



    public Movie GetById(int id)
    {
        using (var multipleResults = _dbConnection.QueryMultiple(
                   @"SELECT * FROM Movies WHERE Id = @MovieId;
              SELECT ScreenwriterId FROM MovieScreenwriters WHERE MovieId = @MovieId;
              SELECT ActorId FROM MovieActors WHERE MovieId = @MovieId;
              SELECT DirectorId FROM MovieDirectors WHERE MovieId = @MovieId;",
                   new { MovieId = id }))
        {
            var movie = multipleResults.ReadSingleOrDefault<Movie>();
            if (movie == null)
            {
                return null;
            }

            movie.ScreenwritersIds = multipleResults.Read<int>().ToList();
            movie.ActorsIds = multipleResults.Read<int>().ToList();
            movie.DirectorsIds = multipleResults.Read<int>().ToList();

            return movie;
        }
    }

    public void AddMovie(Movie movie)
    {
        using (var transaction = _dbConnection.BeginTransaction())
        {
            var sql = "INSERT INTO Movies (Title, Duration, Category) VALUES (@Title, @ReleaseYear, @Category)";
            var movieId = _dbConnection.ExecuteScalar<int>(sql, movie, transaction);

            foreach (var actorId in movie.ActorsIds)
            {
                var insertSql = "INSERT INTO Actors (PersonId) VALUES (@PersonId)";
                _dbConnection.Execute(insertSql, new { PersonId = actorId }, transaction);

                insertSql = "INSERT INTO MovieActors (ActorId, MovieId) VALUES (@ActorId, @MovieId)";
                _dbConnection.Execute(insertSql, new { ActorId = actorId, MovieId = movieId }, transaction);
            }
            
            foreach (var actorId in movie.ScreenwritersIds)
            {
                var insertSql = "INSERT INTO Screenwriters (PersonId) VALUES (@PersonId)";
                _dbConnection.Execute(insertSql, new { PersonId = actorId }, transaction);
                
                insertSql = "INSERT INTO MovieDirectors (DirectorId, MovieId) VALUES (@DirectorId, @MovieId)";
                _dbConnection.Execute(insertSql, new { DirectorId = actorId, MovieId = movieId }, transaction);
            }
            
            foreach (var actorId in movie.DirectorsIds)
            {
                var insertSql = "INSERT INTO Directors (PersonId) VALUES (@PersonId)";
                _dbConnection.Execute(insertSql, new { PersonId = actorId }, transaction);
                
                insertSql = "INSERT INTO MovieScreenwriters (ScreenwriterId, MovieId) VALUES (@ScreenwriterId, @MovieId)";
                _dbConnection.Execute(insertSql, new { ScreenwriterId = actorId, MovieId = movieId }, transaction);
            }
            
            transaction.Commit();
        }
    }

    public void UpdateMovie(Movie movie)
    {
        using (var transaction = _dbConnection.BeginTransaction())
        {
            _dbConnection.Execute("UPDATE Movies SET InPreview = @InPreview", movie, transaction);

        }
    }

    public void RemoveMovie(int movieId)
    {
        using (var transaction = _dbConnection.BeginTransaction())
        {
            _dbConnection.Execute("DELETE FROM MovieScreenwriters WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);
            _dbConnection.Execute("DELETE FROM MovieDirectors WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);
            _dbConnection.Execute("DELETE FROM MovieActors WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);

            _dbConnection.Execute("DELETE FROM Movies WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);

            transaction.Commit();
        }
    }
}
