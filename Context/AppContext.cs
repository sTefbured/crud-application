using System;
using System.Windows.Forms;
using Lab1.Controllers;
using Lab1.Repository;
using Lab1.View;

namespace Lab1.Context
{
    public class AppContext : ApplicationContext
    {
        private const string usersDatabasePath = "userinfo.dat";
        
        private static AppContext _instance;
        public static AppContext Instance => _instance ??= new AppContext();

        private readonly EmployeeView _employeeForm;
        private readonly AuthorizationView _authorizationForm;

        private AppContext()
        {
            var employeeRepo = new EmployeeRepository();
            var employeeController = new EmployeeController(employeeRepo);
            _employeeForm = new EmployeeView(employeeController);
            var userRepo = new UserRepository(usersDatabasePath);
            var userController = new UserController(userRepo);
            _authorizationForm = new AuthorizationView(userController);
            MainForm = _authorizationForm;
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            if (MainForm == _authorizationForm)
            {
                MainForm = _employeeForm;
                MainForm.Show();
                MainForm.Focus();
            }
            else
            {
                base.OnMainFormClosed(sender, e);
            }
        }
    }
}