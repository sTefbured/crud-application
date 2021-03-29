using System.Windows.Forms;
using Lab1.Context;

namespace Lab1.View
{
    public partial class UserInfoView : Form
    {
        public UserInfoView()
        {
            InitializeComponent();
            loginTextBox.Text = SecurityContext.Instance.CurrentUser.Login;
            roleTextBox.Text = SecurityContext.Instance.CurrentUser.Role.ToString();
        }
    }
}