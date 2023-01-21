using System.ComponentModel.DataAnnotations.Schema;

namespace eLibraryOnContainers.Identity.Infrastructure.Models;

public class ReadUser
{
    public Guid Id { get; }
    public string Email { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public IList<ReadRole> Roles { get; }

    public ReadUser(Guid id, string email, string password, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Roles = new List<ReadRole>();
    }
}