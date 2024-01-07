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
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool InPreview { get; set; }
    public List<string> Screenwriters { get; set; }
    public List<string> Directors { get; set; }
    public List<string> Actors { get; set; }
}

public class Person
{
    public int Id { get; set; }
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
    private readonly string _dbConnectionString;

    public PeopleRepository(string connectionString)
    {
        _dbConnectionString = connectionString;
    }
    
    public IEnumerable<Person> GetPeople()
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        var sql = "SELECT * FROM People";
        return dbConnection.Query<Person>(sql);
    }

    public void AddPerson(Person person)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        var sql = "INSERT INTO People (Name, Surname) VALUES (@Name, @Surname)";
        dbConnection.Execute(sql, person);
    }

    public void RemovePerson(int personId)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        var sql = "DELETE FROM People WHERE Id = @PersonId";
        dbConnection.Execute(sql, new { PersonId = personId });
    }
}

public class MoviesRepository
{
    private readonly string _dbConnectionString;

    public MoviesRepository(string connectionString)
    {
        _dbConnectionString = connectionString;
    }
    
    public IEnumerable<MovieDto> GetAllMovies()
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();

        var people = dbConnection.Query<Person>("SELECT * FROM People").ToList();
        
        var sql = @"
                SELECT
                    M.Id AS Id,
                    M.Title,
                    M.Category,
                    M.InPreview,
                    MS.ScreenwriterId,
                    MD.DirectorId,
                    MA.ActorId
                FROM Movies M
                LEFT JOIN MovieScreenwriters MS ON M.Id = MS.MovieId
                LEFT JOIN MovieDirectors MD ON M.Id = MD.MovieId
                LEFT JOIN MovieActors MA ON M.Id = MA.MovieId";
        
        var movies = new Dictionary<int, Movie>();

        dbConnection.Query<Movie, int, int, int, Movie>(
            sql,
            (movie, screenwriterId, directorId, actorId) =>
            {
                if (!movies.TryGetValue(movie.Id, out var movieEntity))
                {
                    movieEntity = movie;
                    movieEntity.ScreenwritersIds = new List<int>();
                    movieEntity.DirectorsIds = new List<int>();
                    movieEntity.ActorsIds = new List<int>();
                    movies.Add(movie.Id, movieEntity);
                }

                if (screenwriterId != 0)
                    movieEntity.ScreenwritersIds.Add(screenwriterId);

                if (directorId != 0)
                    movieEntity.DirectorsIds.Add(directorId);

                if (actorId != 0)
                    movieEntity.ActorsIds.Add(actorId);

                return movieEntity;
            },
            splitOn: "ScreenwriterId,DirectorId,ActorId"
        );

        return movies.Values.Select(m => new MovieDto
        {
            Id = m.Id,
            InPreview = m.InPreview,
            Category = m.Category,
            Title = m.Title,
            Actors = people.Where(p => m.ActorsIds.Contains(p.Id)).Select(p => $"{p.Name} {p.Surname}").ToList(),
            Screenwriters = people.Where(p => m.ScreenwritersIds.Contains(p.Id)).Select(p => $"{p.Name} {p.Surname}")
                .ToList(),
            Directors = people.Where(p => m.DirectorsIds.Contains(p.Id)).Select(p => $"{p.Name} {p.Surname}").ToList(),
        });
    }
    
    public MovieDto GetByIdWithNames(int id)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        var sql = @"
                SELECT
                    M.Id AS Id,
                    M.Title,
                    M.Category,
                    M.InPreview,
                    MS.ScreenwriterId,
                    MD.DirectorId,
                    MA.ActorId
                FROM Movies M
                LEFT JOIN MovieScreenwriters MS ON M.Id = MS.MovieId
                LEFT JOIN MovieDirectors MD ON M.Id = MD.MovieId
                LEFT JOIN MovieActors MA ON M.Id = MA.MovieId
                WHERE m.Id = @MovieId";
        
        var result = dbConnection.Query<Movie, int, int, int, Movie>(
            sql,
            (movie, screenwriterId, directorId, actorId) =>
            {
                movie.ScreenwritersIds ??= new List<int>();
                movie.DirectorsIds ??= new List<int>();
                movie.ActorsIds ??= new List<int>();
                
                if (screenwriterId != 0)
                    movie.ScreenwritersIds.Add(screenwriterId);

                if (directorId != 0)
                    movie.DirectorsIds.Add(directorId);

                if (actorId != 0)
                    movie.ActorsIds.Add(actorId);

                return movie;
            },
            new { MovieId = id },
            splitOn: "ScreenwriterId,DirectorId,ActorId"
        );

        var movie = result?.FirstOrDefault();

        if (movie == null)
        {
            return null;
        }
        
        var ids = movie.ActorsIds.Concat(movie.DirectorsIds).Concat(movie.ScreenwritersIds).Where(x => x != null).ToList();

        var parameters = new DynamicParameters();
        parameters.Add("Ids", ids);

        var people = dbConnection.Query<Person>("SELECT * FROM People WHERE Id = ANY(@Ids)", parameters).ToList();

        return new MovieDto
        {
            Id = movie.Id,
            InPreview = movie.InPreview,
            Category = movie.Category,
            Title = movie.Title,
            Actors = people.Where(p => movie.ActorsIds.Contains(p.Id)).Select(p => $"{p.Name} {p.Surname}").ToList(),
            Screenwriters = people.Where(p => movie.ScreenwritersIds.Contains(p.Id))
                .Select(p => $"{p.Name} {p.Surname}")
                .ToList(),
            Directors = people.Where(p => movie.DirectorsIds.Contains(p.Id)).Select(p => $"{p.Name} {p.Surname}")
                .ToList(),
        };
    }

    public Movie GetById(int id)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        using (var multipleResults = dbConnection.QueryMultiple(
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
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        using (var transaction = dbConnection.BeginTransaction())
        {
            var sql = "INSERT INTO Movies (Title, Duration, Category) VALUES (@Title, @Duration, @Category)";
            var movieId = dbConnection.ExecuteScalar<int>(sql, movie, transaction);

            foreach (var actorId in movie.ActorsIds)
            {
                var insertSql = "INSERT INTO Actors (PersonId) VALUES (@PersonId)";
                dbConnection.Execute(insertSql, new { PersonId = actorId }, transaction);

                insertSql = "INSERT INTO MovieActors (ActorId, MovieId) VALUES (@ActorId, @MovieId)";
                dbConnection.Execute(insertSql, new { ActorId = actorId, MovieId = movieId }, transaction);
            }
            
            foreach (var screenwriterId in movie.ScreenwritersIds)
            {
                var insertSql = "INSERT INTO Screenwriters (PersonId) VALUES (@PersonId)";
                dbConnection.Execute(insertSql, new { PersonId = screenwriterId }, transaction);
                
                insertSql = "INSERT INTO MovieDirectors (DirectorId, MovieId) VALUES (@DirectorId, @MovieId)";
                dbConnection.Execute(insertSql, new { DirectorId = screenwriterId, MovieId = movieId }, transaction);
            }
            
            foreach (var directorId in movie.DirectorsIds)
            {
                var insertSql = "INSERT INTO Directors (PersonId) VALUES (@PersonId)";
                dbConnection.Execute(insertSql, new { PersonId = directorId }, transaction);
                
                insertSql = "INSERT INTO MovieScreenwriters (ScreenwriterId, MovieId) VALUES (@ScreenwriterId, @MovieId)";
                dbConnection.Execute(insertSql, new { ScreenwriterId = directorId, MovieId = movieId }, transaction);
            }
            
            transaction.Commit();
        }
    }

    public void UpdateMovie(Movie movie)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        using (var transaction = dbConnection.BeginTransaction())
        {
            dbConnection.Execute("UPDATE Movies SET InPreview = @InPreview", movie, transaction);

        }
    }

    public void RemoveMovie(int movieId)
    {
        using var dbConnection = new NpgsqlConnection(_dbConnectionString);
        dbConnection.Open();
        
        using (var transaction = dbConnection.BeginTransaction())
        {
            dbConnection.Execute("DELETE FROM MovieScreenwriters WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);
            dbConnection.Execute("DELETE FROM MovieDirectors WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);
            dbConnection.Execute("DELETE FROM MovieActors WHERE MovieId = @MovieId", new { MovieId = movieId }, transaction);

            dbConnection.Execute("DELETE FROM Movies WHERE Id = @MovieId", new { MovieId = movieId }, transaction);

            transaction.Commit();
        }
    }
}
