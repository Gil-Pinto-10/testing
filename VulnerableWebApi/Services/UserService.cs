using System;
using System.IO;
using VulnerableWebApi.Models;

namespace VulnerableWebApi.Services
{
    public class UserService
    {
        private readonly DataAccess _dataAccess;

        // Constructor for dependency injection if you register DataAccess as a service
        public UserService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public User RetrieveUser(string username)
        {
            return _dataAccess.GetUserByUsername_Vulnerable(username);
        }

        public User GetUserById(int id)
        {
            return _dataAccess.GetUserById(id);
        }


        public bool CheckAdminAccess(string apiKey)
        {
            return !string.IsNullOrEmpty(apiKey) && apiKey == SensitiveConfig.AdminApiKey;
        }

        public string GetUserEmail(User user)
        {
            // VULNERABILITY: Potential Null Dereference
            return user.Email; // Will throw if 'user' is null
        }

        public void LogActivity_Vulnerable(string message)
        {
            // VULNERABILITY: Resource Leak
            StreamWriter logFile = new StreamWriter("api_activity.log", append: true);
            logFile.WriteLine($"{DateTime.UtcNow}: {message}");
            // Missing: logFile.Close(); logFile.Dispose(); or using statement
            Console.WriteLine($"Logged to api_activity.log (resource leak): {message}");
        }
    }
}