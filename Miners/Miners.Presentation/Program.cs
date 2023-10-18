using Autofac;
using Miners.Presentation.ContainerDI;
using Miners.Presentation.Views;
using System;
using System.Windows.Forms;

namespace Miners.Presentation
{
    internal static class Program
    {
        public static IContainer Container { get; private set; }
        [STAThread]
        static void Main()
        {
            Container = ContainerConfig.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LauncherForm());
        }
    }
}
