using System;
using System.Windows.Forms;
using Lab1.Context;
using Lab1.Controllers;
using Lab1.Models;

namespace Lab1.View
{
    public partial class AdministrationView : Form
    {
        private readonly IUserController _userController;
        private readonly User _currentUser;
        
        public AdministrationView(IUserController userController)
        {
            InitializeComponent();
            _userController = userController;
            _currentUser = new User();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            usersGridView.DataSource = _userController.GetAllUsers();
        }
        
        private void ShowUserInfo(DataGridViewRow userInfoRow)
        {
            usernameLabel.Text = userInfoRow.Cells[0].Value.ToString();
            roleLabel.Text = userInfoRow.Cells[2].Value.ToString();
        }

        private void UpdateCurrentUser(DataGridViewRow userInfoRow)
        {
            _currentUser.Login = userInfoRow.Cells[0].Value.ToString();
            _currentUser.Password = userInfoRow.Cells[1].Value.ToString();
            var roleString = userInfoRow.Cells[2].Value.ToString();
            _currentUser.Role = (UserRole) Enum.Parse(typeof(UserRole), roleString!);
        }

        private void usersGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            
            var row = usersGridView.Rows[e.RowIndex];
            ShowUserInfo(row);
            UpdateCurrentUser(row);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (SecurityContext.Instance.IsAccessible(_userController, nameof(_userController.Delete)))
            {
                _userController.Delete(_currentUser);
                UpdateGrid();
            }
        }

        private void AdministrationView_Activated(object sender, EventArgs e)
        {
            UpdateGrid();
        }
    }
}