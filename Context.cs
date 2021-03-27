using System;
using System.Windows.Forms;
using Lab1.Controllers;
using Lab1.Repository;
using Lab1.View;

namespace Lab1
{
    public class Context : ApplicationContext
    {
        private readonly EmployeeView _employeeForm;
        private readonly AuthorizationView _authorizationForm;
        
        public Context()
        {
            var employeeController = new EmployeeController(new EmployeeRepository());
            _employeeForm = new EmployeeView(employeeController);
            var userController = new UserController(new UserRepository());
            _authorizationForm = new AuthorizationView(userController);
            MainForm = _authorizationForm;
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            if (MainForm == _authorizationForm)
            {
                MainForm = _employeeForm;
                MainForm.Show();
            }
            else
            {
                base.OnMainFormClosed(sender, e);
            }
        }
    }
}