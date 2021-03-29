using System;
using System.Windows.Forms;
using Lab1.Context;
using Lab1.Controllers;
using Lab1.Models;

namespace Lab1.View
{
    public partial class UserSettingsView : Form
    {
        private readonly IUserController _userController;
        
        public UserSettingsView(IUserController userController)
        {
            InitializeComponent();
            _userController = userController;
            loginTextBox.Text = SecurityContext.Instance.CurrentUser.Login;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = newPasswordTextBox.Text;
            if (password != confirmPasswordTextBox.Text)
            {
                MessageBox.Show(@"Password and password confirmation don't match!");
                return;
            }
            SecurityContext.Instance.Invoke(_userController,
                nameof(_userController.Edit), SecurityContext.Instance.CurrentUser, 
                login, password);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}