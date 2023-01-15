using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class UserAlreadyExists : ValidationError
{
    public UserAlreadyExists() : base("User with this e-mail address already exists.")
    {
    }
}