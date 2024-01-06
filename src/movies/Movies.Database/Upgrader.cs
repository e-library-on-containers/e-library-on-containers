using DbUp;

namespace Movies.Database;

public static class Upgrader
{
    public static int Upgrade(string connectionString)
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
            .WithScriptsAndCodeEmbeddedInAssembly(typeof(Upgrader).Assembly)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}