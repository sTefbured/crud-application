using System;
using System.Windows.Forms;
using Lab1.Controllers;
using Lab1.Repository.Exception;

namespace Lab1.View
{
    public partial class AuthorizationView : Form
    {
        private readonly IUserController _userController;
        
        public AuthorizationView(IUserController userController)
        {
            InitializeComponent();
            _userController = userController;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordTextBox.Text;
            try
            {
                _userController.Login(login, password);
                Close();
            }
            catch (UserNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordTextBox.Text;
            _userController.Register(login, password);
        }
        
        private void guestButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}