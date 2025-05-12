namespace VulnerableWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileInfo { get; set; }

        public User(int id, string username, string email = null, string profileInfo = "Default Profile")
        {
            Id = id;
            Username = username;
            Email = email;
            ProfileInfo = profileInfo;
        }
    }
}