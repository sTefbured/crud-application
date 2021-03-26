using System;

namespace Lab1.Models
{
    [Serializable]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
        
        public User() : this("", "", UserRole.USER) {}
    }
}