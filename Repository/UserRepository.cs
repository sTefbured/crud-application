using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lab1.Models;
using Lab1.Repository.Exception;

namespace Lab1.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string _usersFilePath = "userinfo.bin";

        public void Add(User user)
        {
            var users = GetAll();
            if (users.Exists(e => e.Login.Equals(user.Login)))
            {
                throw new UserExistsException();
            }

            users.Add(user);
            WriteAll(users);
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
            users.RemoveAll(e => e.Login.Equals(login));
            WriteAll(users);
        }

        public void EditByLogin(string login, User newUser)
        {
            if (newUser == null)
            {
                return;
            }

            var users = GetAll();
            var user = users.Find(e => e.Login == login);
            if (user != null)
            {
                user.Login = newUser.Login;
                user.Password = newUser.Password;
                WriteAll(users);
            }
        }

        public User FindByLoginAndPassword(string login, string password)
        {
            var user = GetAll()
                .Find(e => (e.Login.Equals(login)) && (e.Password == password));
            if (user == null)
            {
                throw new UserNotFoundException("Wrong login or/and password.");
            }

            return user;
        }

        public User FindByLogin(string login)
        {
            var user = GetAll()
                .Find(e => e.Login.Equals(login));
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        private List<User> GetAll()
        {
            var formatter = new BinaryFormatter();
            if (File.Exists(_usersFilePath))
            {
                var stream = File.OpenRead(_usersFilePath);
                var users = (List<User>) formatter.Deserialize(stream);
                stream.Close();
                return users;
            }
            return new List<User>();
        }
    }
}