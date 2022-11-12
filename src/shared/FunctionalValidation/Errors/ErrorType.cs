namespace FunctionalValidation.Errors
{
    /// <summary>
    /// Type of <see cref="ApplicationError"/> error type.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Validation error.
        /// </summary>
        Validation,
        /// <summary>
        /// Restricted access error.
        /// </summary>
        RestrictedAccess,
        /// <summary>
        /// Resource not found error.
        /// </summary>
        NotFound
    }
}