using System;
using System.Windows.Forms;
using Lab1.Controllers;
using Lab1.Repository;

namespace Lab1.View
{
    public partial class AuthorizationView : Form
    {
        private readonly UserController _userController;
        public AuthorizationView()
        {
            InitializeComponent();
            _userController = new UserController(new UserRepository());
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordTextBox.Text;
            _userController.Login(login, password);
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
    }
}