using System.Collections.Generic;
using System.Windows.Forms;
using Lab1.Attribute;
using Lab1.Context;
using Lab1.Models;
using Lab1.Repository;
using Lab1.Repository.Exception;

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
            try
            {
                _userRepository.Add(user);
            }
            catch (UserExistsException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void Login(string login, string password)
        {
            var user = _userRepository.FindByLoginAndPassword(login, password);
            SecurityContext.Instance.CurrentUser = user;
        }

        [Security(UserRole.ADMIN)]
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        [Security(UserRole.ADMIN)]
        public void Delete(User user)
        {
            _userRepository.DeleteByLogin(user.Login);
        }
    }
}