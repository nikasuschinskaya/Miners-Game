using Miners.Server.Level;
using Miners.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Miners.Server
{
    internal class Program
    {
        private static readonly object _LOCK = new object();
        private const int _CLIENTCOUNT = 2;

        private static List<Socket> _clients = new List<Socket>();

        static void Main(string[] args)
        {
            var host = "127.0.0.1";
            var port = 12345;
            Socket serverSocket = null;

            try
            {
                var endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(endpoint);
                serverSocket.Listen(10);

                Console.WriteLine("Server is running");

                while (_clients.Count < _CLIENTCOUNT)
                {
                    var clientSocket = serverSocket.Accept();
                    _clients.Add(clientSocket);

                    Console.WriteLine("+ чел");

                    ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptUser), clientSocket);
                }

                //Listen first user
                ThreadPool.QueueUserWorkItem(new WaitCallback(ListenUser), 0);

                //Listen second user
                ThreadPool.QueueUserWorkItem(new WaitCallback(ListenUser), 1);

                Console.ReadKey();
            }
            //catch (Exception)
            //{
            //    throw;
            //}
            finally
            {
                serverSocket?.Close();
            }
        }

        static void ListenUser(object state)
        {
            if (!(state is int userIndex))
            {
                return;
            }

            Socket userSocket;

            lock (_LOCK)
            {
                userSocket = _clients[userIndex];
            }

            var request = ReadDataFromSocket(userSocket);
            Console.WriteLine(request);
            if (request.StartsWith(nameof(CommandType.NAME)))
            {
                int spaceIndex = request.IndexOf(' ');
                Console.WriteLine($"Наш любимый игрок: {request.Substring(spaceIndex)}");
            }

            //Handle request
            var response = nameof(CommandType.OK);

            userSocket.Send(Encoding.UTF8.GetBytes(response));

            LevelLoader levelLoader = new LevelLoader();
            var map = levelLoader.LoadLevel();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                //Converters = new List<JsonConverter> { new GameObjectConverter() },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonMap = JsonConvert.SerializeObject(map, settings);

            response = $"{userIndex}\n{nameof(CommandType.MAP)} {jsonMap}";
            userSocket.Send(Encoding.UTF8.GetBytes(response));


            while (true)
            {
                request = ReadDataFromSocket(userSocket);
                Console.WriteLine(request);

                //Handle request
                response = nameof(CommandType.OK);

                userSocket.Send(Encoding.UTF8.GetBytes(response));
            }
        }

        static string ReadDataFromSocket(Socket socket)
        {
            var buffer = new byte[1024 * 16];
            int bytesRead = socket.Receive(buffer);

            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        static void AcceptUser(object state)
        {
            if (!(state is Socket socket))
            {
                return;
            }

            Console.WriteLine("Connected");
        }
    }
}
