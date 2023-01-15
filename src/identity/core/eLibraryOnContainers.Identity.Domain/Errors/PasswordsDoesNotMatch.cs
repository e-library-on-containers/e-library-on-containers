﻿using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class PasswordsDoesNotMatch : ValidationError
{
    public PasswordsDoesNotMatch() : base("Passwords does not match.")
    {
    }
}