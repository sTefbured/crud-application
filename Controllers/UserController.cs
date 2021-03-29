using System.Collections.Generic;
using System.Linq;
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
            if (!IsValidLoginOrPassword(login) || !IsValidLoginOrPassword(password))
            {
                return;
            }

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

        [Security(UserRole.ADMIN, UserRole.USER)]
        public void Edit(User oldUser, string newLogin, string newPassword)
        {
            if (IsValidLoginOrPassword(newLogin)
                && IsValidLoginOrPassword(newPassword))
            {
                var newUser = new User(newLogin, newPassword, oldUser.Role);
                _userRepository.EditByLogin(oldUser.Login, newUser);
            }
        }

        private bool IsValidLoginOrPassword(string line)
        {
            if ((line.Length < 4) || (line.Length > 15))
            {
                MessageBox.Show(@"Login and password length must be more" 
                    + @" than 4 and less than 15 characters.", @"Error");
                return false;
            }

            if (line.All(char.IsLetterOrDigit))
            {
                return true;
            }
            MessageBox.Show(@"Login and password must consist of letters" 
                            + @" and/or numbers.", @"Error");
            return false;
        }
    }
}