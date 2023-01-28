using System.Security.Cryptography;
using System.Text;

namespace Identity.Tests;

public static class Functions
{
    public static string HashPassword(string password) =>
        Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
}