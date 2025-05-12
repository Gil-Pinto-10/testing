using System;
using System.Collections.Generic;
using System.Linq;
using VulnerableWebApi.Models;

namespace VulnerableWebApi.Services
{
    public class DataAccess
    {
        private static readonly List<User> UsersTable = new List<User>
        {
            new User(1, "alice", "alice@example.com", "Alice's Profile"),
            new User(2, "bob", "bob@example.com", "Bob's original profile"),
            new User(3, "charlie_admin", "charlie@example.com", "Charlie's Admin Profile")
        };

        public User GetUserById(int id)
        {
            return UsersTable.FirstOrDefault(u => u.Id == id);
        }

        // MODIFIED METHOD
        public User GetUserByUsername_Vulnerable(string username)
        {
            // VULNERABILITY: SQL Injection - unsafe concatenation is still present and logged
            string queryForLogging = $"SELECT * FROM Users WHERE Username = '{username}'";
            Console.WriteLine($"Simulating SQL query: {queryForLogging}"); // Log the vulnerable query

            // --- Enhanced Mock Exploitation Logic ---
            // Check for a common, simple SQL injection pattern like ' OR '1'='1'
            // This is a basic simulation of the bypass effect.
            // We make it case-insensitive for robustness.
            if (username != null && username.IndexOf("' or '1'='1'", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine("SQL Injection pattern 'OR '1'='1' detected in mock! Simulating filter bypass by returning the first user in the database (Alice).");
                // To simulate returning "more data" or bypassing the filter, we return a user.
                // A real injection might return many users. Here, we'll return the first one.
                return UsersTable.FirstOrDefault(); // This will return Alice
            }
            // --- End of Enhanced Mock Exploitation Logic ---

            // Original mock logic if no "exploitable" pattern (for this simple mock) is found.
            // This part will now only be hit if the input doesn't contain the ' or '1'='1' pattern.
            var user = UsersTable.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null && username != null && (username.Contains("'") || username.IndexOf(" or ", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                 Console.WriteLine("Input contains special characters, but no specific mock exploitation rule ('OR '1'='1') was hit by the mock. Standard lookup failed.");
            }
            else if (user == null)
            {
                Console.WriteLine($"Standard lookup for username '{username}' failed.");
            }

            return user;
        }

        public void UpdateUserProfile_Vulnerable(int userId, string profileData)
        {
            // ... (rest of this method remains the same as before)
            User userToUpdate = null;
            try
            {
                userToUpdate = UsersTable.FirstOrDefault(u => u.Id == userId);
                if (userToUpdate == null)
                {
                    Console.WriteLine($"User with ID {userId} not found for update.");
                    object tempObj = null;
                    tempObj.ToString();
                    return;
                }

                if (profileData != null && profileData.Contains("error_trigger"))
                {
                    throw new InvalidOperationException("Simulated error during profile update logic based on input.");
                }

                userToUpdate.ProfileInfo = profileData;
                Console.WriteLine($"User {userId} profile supposedly updated to: {profileData}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"DataAccess: Handled specific ArgumentException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccess.UpdateUserProfile_Vulnerable: An unspecified error occurred and was silently ignored. Details: {ex.GetType().Name}");
            }
        }
    }
}