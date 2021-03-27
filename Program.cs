using Lab1.Controllers;
using Lab1.Repository;
using Lab1.View;
using System;
using System.Windows.Forms;

namespace Lab1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IEmployeeController controller = new EmployeeController(new EmployeeRepository());
            Application.Run(new AuthorizationView());
        }
    }
}
