using System.Windows.Forms;
using Lab1.Controllers;
using Lab1.Repository;

namespace Lab1.View
{
    public partial class AdministrationView : Form
    {
        private readonly IUserController _userController;
        
        public AdministrationView(IUserController userController)
        {
            InitializeComponent();
            _userController = userController;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            usersGridView.DataSource = _userController.GetAllUsers();
        }
    }
}