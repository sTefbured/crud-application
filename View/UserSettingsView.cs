using System;
using System.Windows.Forms;
using Lab1.Controllers;

namespace Lab1.View
{
    public partial class UserSettingsView : Form
    {
        private readonly IUserController _userController;
        
        public UserSettingsView(IUserController userController)
        {
            InitializeComponent();
            _userController = userController;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}