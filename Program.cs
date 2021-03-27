using System;
using System.Windows.Forms;

namespace Lab1
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Context.AppContext.Instance);
        }
    }
}
