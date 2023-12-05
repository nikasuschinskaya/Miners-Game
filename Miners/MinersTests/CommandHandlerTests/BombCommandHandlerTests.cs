//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Miners.Server;
//using Miners.Server.CommandHandlers;
//using Miners.Shared;
//using Miners.Shared.Objects.Bombs;
//using Moq;
//using Newtonsoft.Json;
//using OpenTK;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace MinersTests.CommandHandlerTests
//{
//    [TestClass]
//    public class BombCommandHandlerTests
//    {
//        private BombCommandHandler _bombCommandHandler;
//        private Mock<Socket> _userSocket;
//        private Mock<Socket> _otherSocket;

//        [TestInitialize]
//        public void TestInitialize()
//        {
//            _userSocket = new Mock<Socket>();
//            _otherSocket = new Mock<Socket>();

//            // Инициализация Mock-объекта для класса Game
//            var gameMock = new Mock<Game>();
//            Game.Instance = gameMock.Object;

//            _bombCommandHandler = new BombCommandHandler(_userSocket.Object, _otherSocket.Object, 0);
//        }

//        [TestMethod]
//        public void Handle_ValidRequest_SendsSpawnBombCommandToBothPlayers()
//        {
//            // Arrange

//            // Act
//            _bombCommandHandler.Handle($"{nameof(CommandType.SPAWN_BOMB)} {{\"X\": 1, \"Y\": 2}}");

//            // Assert
//            _userSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//            _otherSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//        }

//        [TestMethod]
//        public void OnTimerElapsed_ExplodeBomb_DrawCommandSentToBothPlayers()
//        {
//            // Arrange

//            // Simulate bomb spawn
//            _bombCommandHandler.Handle($"{nameof(CommandType.SPAWN_BOMB)} {{\"X\": 1, \"Y\": 2}}");

//            // Act
//            _bombCommandHandler.Handle($"{nameof(CommandType.EXPLORE_BOMB)} {JsonConvert.SerializeObject(new Bomb(new Vector2(1, 2), "path"))}");

//            // Assert
//            _userSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//            _otherSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//        }

//        [TestMethod]
//        public void OnTimerElapsed_ExplodeBomb_DeathCommandSentToCorrectPlayer()
//        {
//            // Arrange

//            // Simulate bomb spawn
//            _bombCommandHandler.Handle($"{nameof(CommandType.SPAWN_BOMB)} {{\"X\": 1, \"Y\": 2}}");

//            // Simulate miner position
//            Game.Instance.GetMiner(0).Position = new Vector2(1, 2);
//            Game.Instance.GetMiner(1).Position = new Vector2(5, 5);

//            // Act
//            _bombCommandHandler.OnTimerElapsed(null, null);

//            // Assert
//            _userSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//            _otherSocket.Verify(socket => socket.Send(It.IsAny<byte[]>()), Times.Once);
//        }
//    }
//}
