using Lab1.Models;

namespace Lab1.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        void DeleteByLogin(string login);
        void EditByLogin(string login, User newUser);
        User FindByLogin(string login);
    }
}