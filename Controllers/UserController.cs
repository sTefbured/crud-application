using Lab1.Models;
using Lab1.Repository;
using Lab1.Repository.Exception;

namespace Lab1.Controllers
{
    public class UserController
    {
        private readonly IUserRepository _userRepository;
        public User CurrentUser { get; private set; }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            CurrentUser = null;
        }

        public string Register(User user)
        {
            try
            {
                _userRepository.Add(user);
                return "Registration successful.";
            }
            catch (UserExistsException exception)
            {
                return exception.Message;
            }
        }

        public string Login(string login, string password)
        {
            try
            {
                var user = _userRepository.FindByLoginAndPassword(login, password);
                CurrentUser = user;
                return "Hello, " + login;
            }
            catch (UserNotFoundException exception)
            {
                return exception.Message;
            }
        }
    }
}