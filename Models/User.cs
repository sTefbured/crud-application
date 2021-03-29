using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab1.Models
{
    [Serializable]
    public class User
    {
        public string Login { get; set; }
        private string _password;
        public string Password
        {
            get => _password;
            set => _password = ComputeHash(value);
        }

        public UserRole Role { get; set; }

        public User(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
        
        public User() : this("", "", UserRole.USER) {}

        public bool Equals(string login)
        {
            return login.Equals(Login);
        }
        
        public bool Equals(string login, string password)
        {
            return Equals(login) && ComputeHash(password).Equals(Password);
        }

        private string ComputeHash(string line)
        {
            var bytes = SHA512.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(line));
            return Encoding.UTF8.GetString(bytes);
        }
    }
}