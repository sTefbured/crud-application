using System;

namespace Lab1.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User(int id, string login, string password, UserRole role)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
        }
        
        public User() : this(0, "", "", UserRole.USER) {}
    }
}