using Miners.Presentation.Views;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Miners.Presentation
{
    internal static class Program
    {
        /// <summary>Gets the client socket.</summary>
        /// <value>The client socket.</value>
        public static Socket ClientSocket { get; private set; }

        /// <summary>Defines the entry point of the application.</summary>
        [STAThread]
        static void Main()
        {
            var host = "192.168.228.103";
            //var host = "127.0.0.1";
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
        }
    }
}
