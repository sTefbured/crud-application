using Lab1.Attribute;
using Lab1.Context;
using Lab1.Models;
using Lab1.Repository;

namespace Lab1.Controllers
{
    public class UserController : IUserController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public void Register(string login, string password)
        {
            var user = new User(login, password, UserRole.USER);
            _userRepository.Add(user);
        }

        public void Login(string login, string password)
        {
            var user = _userRepository.FindByLoginAndPassword(login, password);
            SecurityContext.Instance.CurrentUser = user;
        }
    }
}