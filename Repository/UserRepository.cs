using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lab1.Models;
using Lab1.Repository.Exception;

namespace Lab1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _usersFilePath;

        public UserRepository(string filePath)
        {
            _usersFilePath = filePath;
            var users = GetAll();
            if (!users.Exists(u => u.Role == UserRole.ADMIN))
            {
                Add(new User("admin", "admin", UserRole.ADMIN));
            }
        }

        public void Add(User user)
        {
            var users = GetAll();
            ValidateUser(users, user);
            users.Add(user);
            WriteAll(users);
        }

        private void ValidateUser(List<User> users, User user)
        {
            if (users.Exists(u => u.Equals(user.Login)))
            {
                throw new UserExistsException();
            }
        }

        private void WriteAll(List<User> users)
        {
            File.Delete(_usersFilePath);
            var stream = File.OpenWrite(_usersFilePath);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, users);
            stream.Close();
        }

        public void DeleteByLogin(string login)
        {
            var users = GetAll();
            users.RemoveAll(u => u.Equals(login));
            WriteAll(users);
        }

        public void EditByLogin(string login, User newUser)
        {
            if (newUser == null)
            {
                return;
            }
            
            var users = GetAll();
            var user = users.Find(u => u.Equals(login));
            if (user != null)
            {
                if (login != newUser.Login)
                {
                    ValidateUser(users, newUser);
                }
                int index = users.IndexOf(user);
                users[index] = newUser;
                WriteAll(users);
            }
        }

        public User FindByLoginAndPassword(string login, string password)
        {
            var user = GetAll().Find(u => u.Equals(login, password));
            if (user == null)
            {
                throw new UserNotFoundException("Wrong login or/and password.");
            }

            return user;
        }

        public User FindByLogin(string login)
        {
            var user = GetAll().Find(u => u.Equals(login));
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public List<User> GetAll()
        {
            if (File.Exists(_usersFilePath))
            {
                var formatter = new BinaryFormatter();
                var stream = File.OpenRead(_usersFilePath);
                var users = (List<User>) formatter.Deserialize(stream);
                stream.Close();
                return users;
            }

            return new List<User>();
        }
    }
}