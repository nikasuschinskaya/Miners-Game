using Autofac;
using Miners.Presentation.ContainerDI;
using Miners.Presentation.Views;
using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Security.RightsManagement;

namespace Miners.Presentation
{
    internal static class Program
    {
        //public static IContainer Container { get; private set; }

        public static Socket ClientSocket { get; private set; }

        [STAThread]
        static void Main()
        {
            var host = "127.0.0.1";
            var port = 12345;

            try
            {
                var endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(endpoint);
            }
            catch (Exception)
            {

            }
            finally
            {
                //ClientSocket?.Close();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LauncherForm());
            //Container = ContainerConfig.Configure();
        }
    }
}
