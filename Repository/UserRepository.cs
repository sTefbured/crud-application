using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lab1.Models;

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
                return;
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

        public User FindByLogin(string login)
        {
            return GetAll()
                .Find(e => e.Login == login);
        }

        public List<User> GetAll()
        {
            var formatter = new BinaryFormatter();
            var stream = File.OpenRead(_usersFilePath);
            var users = (List<User>)formatter.Deserialize(stream);
            stream.Close();
            return users;
        }
    }
}