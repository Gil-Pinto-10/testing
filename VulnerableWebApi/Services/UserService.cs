using VulnerableWebApi.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;

public class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Password = "1234" },
        new User { Id = 2, Username = "user", Password = "password" }
    };

    public User? GetByUsernameUnsafe(string username)
    {
        // SQL Injection
        string query = $"SELECT * FROM Users WHERE Username = '{username}'";
        Console.WriteLine("Executing: " + query); // log sensitive input
        return _users.FirstOrDefault(u => u.Username == username);
    }

    public void LogCredentials(string username, string password)
    {
        // Insecure logging
        Console.WriteLine($"LOGIN => User: {username}, Password: {password}");
    }

    public string WeakHash(string input)
    {
        // Weak cryptography
        using var md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }

    public void SwallowException()
    {
        try
        {
            int.Parse("not-an-int");
        }
        catch { } // bad: exception swallowed
    }

    public void UnusedMethod()
    {
        // should be flagged as dead code
        Console.WriteLine("I do nothing.");
    }
}
