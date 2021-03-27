namespace Lab1.Controllers
{
    public interface IUserController
    {
        void Register(string login, string password);
        void Login(string login, string password);
    }
}