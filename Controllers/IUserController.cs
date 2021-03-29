using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Controllers
{
    public interface IUserController
    {
        void Register(string login, string password);
        void Login(string login, string password);
        List<User> GetAllUsers();
        void Delete(User user);
        void Edit(User oldUser, string newLogin, string newPassword);
    }
}