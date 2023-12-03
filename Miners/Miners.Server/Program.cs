using Miners.Server.CommandHandlers;
using Miners.Server.CommandHandlers.Base;
using Miners.Server.CommandHandlers.Extensions;
using Miners.Server.Level;
using Miners.Server.ObjectsFactories.PrizesFactories;
using Miners.Server.ObjectsFactories.PrizesFactories.Base;
using Miners.Shared;
using Miners.Shared.Objects.Prizes.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private static int _randomLevelNumber;

        static void Main(string[] args)
        {
            var host = "192.168.0.119";
            //var host = "127.0.0.1";
            var port = 12345;
            Socket serverSocket = null;

            try
            {
                var endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(endpoint);
                serverSocket.Listen(10);

                Console.WriteLine("Server is running");

                var random = new Random();
                _randomLevelNumber = random.Next(1, 3);

                while (_clients.Count < _CLIENTCOUNT)
                {
                    var clientSocket = serverSocket.Accept();
                    _clients.Add(clientSocket);

                    Console.WriteLine("+ чел");

                    ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptUser), clientSocket);
                }

                LevelLoader levelLoader = new LevelLoader();
                var map = levelLoader.LoadLevel(_randomLevelNumber);
                Game.SetGameSettings(map);

                //Listen first user
                ThreadPool.QueueUserWorkItem(new WaitCallback(ListenUser), 0);

                //Listen second user
                ThreadPool.QueueUserWorkItem(new WaitCallback(ListenUser), 1);

                ThreadPool.QueueUserWorkItem(new WaitCallback(BonusGenerator));

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

        private static void BonusGenerator(object state)
        {
            var factories = new Dictionary<string, PrizeFactory>()
            {
                { "letup", new LetupFactory(0, 0) },
                { "powerup", new PowerupFactory(0, 0) },
            };

            while (true)
            {
                Thread.Sleep(15_000);

                if(Game.Instance.AllPrizes.Count >= 8)
                {
                    Thread.Sleep(5_000);
                    continue;
                }  

                var freeSpaceToSpawn = Game.Instance.GetFreeCells().ToArray();

                var randomPosition = freeSpaceToSpawn[new Random().Next(freeSpaceToSpawn.Length)];
                var randomFactory = factories.ElementAt(new Random().Next(factories.Count)).Value;
                randomFactory.X = (int)randomPosition.X;
                randomFactory.Y = (int)randomPosition.Y;

                var randomPrize = (Prize)randomFactory.CreateObject();
                Game.Instance.AllPrizes.Add(randomPrize);

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(randomPrize, settings);

                foreach (var socket in _clients)
                {
                    socket.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.SPAWN_BONUS)} {json}"));
                }
            }
        }

        static void ListenUser(object state)
        {
            if (!(state is int userIndex))
            {
                return;
            }

            Socket userSocket;
            Socket other;

            lock (_LOCK)
            {
                userSocket = _clients[userIndex];
                other = _clients[userIndex == 0 ? 1 : 0];
            }

            var request = ReadDataFromSocket(userSocket);
            Console.WriteLine(request);
            if (request.StartsWith(nameof(CommandType.NAME)))
            {
                int spaceIndex = request.IndexOf(' ');
                Console.WriteLine($"Наш любимый игрок: {request.Substring(spaceIndex)}");
            }

            var response = nameof(CommandType.OK);

            userSocket.Send(Encoding.UTF8.GetBytes(response));

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonMap = JsonConvert.SerializeObject(Game.Instance.Level, settings);

            response = $"{userIndex}\n{nameof(CommandType.MAP)} {jsonMap}";
            userSocket.Send(Encoding.UTF8.GetBytes(response));


            ICommandHandler commandHandler =
                new MoveCommandHandler(userSocket, other, userIndex)
                    .Or(new BombCommandHandler(userSocket, other, userIndex));

            while (true)
            {
                request = ReadDataFromSocket(userSocket);
                commandHandler.Handle(request);
            }
        }

        static string ReadDataFromSocket(Socket socket)
        {
            try
            {
                var buffer = new byte[1024 * 64 * 100];
                int bytesRead = socket.Receive(buffer);

                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (SocketException)
            {
                Environment.Exit(0);
                throw;
            }
        }

        static void AcceptUser(object state)
        {
            if (!(state is Socket))
            {
                return;
            }

            Console.WriteLine("Connected");
        }
    }
}
