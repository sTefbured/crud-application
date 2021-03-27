using Lab1.Models;
using Lab1.Repository;

namespace Lab1.Controllers
{
    public class UserController : IUserController
    {
        private readonly IUserRepository _userRepository;
        public User CurrentUser { get; private set; }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            CurrentUser = null;
        }

        public void Register(string login, string password)
        {
            var user = new User(login, password, UserRole.USER);
            _userRepository.Add(user);
        }

        public void Login(string login, string password)
        {
            var user = _userRepository.FindByLoginAndPassword(login, password);
            CurrentUser = user;
        }
    }
}