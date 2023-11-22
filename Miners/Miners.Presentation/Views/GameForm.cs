using Autofac;
using Miners.Presentation.Converters;
using Miners.Presentation.Models;
using Miners.Presentation.Render;
using Miners.Shared;
using Miners.Shared.Objects.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class GameForm : Form
    {
        private Game _game;
        private Timer _timer;

        public GameForm()
        {
            InitializeComponent();

            //using (var scope = Program.Container.BeginLifetimeScope())
            //{
            //    _game = scope.Resolve<Game>();
            //}
        }

        protected override void OnLoad(EventArgs e)
        {

            //var data = ReadDataFromSocket(Program.ClientSocket);

            var response = ReadDataFromSocket(Program.ClientSocket).Split('\n');

            var minerIndex = Convert.ToInt32(response[0]);

            var mapJson = response[1];

            if (!mapJson.StartsWith(nameof(CommandType.MAP)))
            {
                return;
            }

            var spaceIndex = response[1].IndexOf(' ');
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new GameObjectConverter() },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var map = response[1].Substring(spaceIndex);
            var level = JsonConvert.DeserializeObject<IGameObject[,]>(map, settings);
            _game = new Game(level, minerIndex);

            base.OnLoad(e);
            glControl.VSync = true;
            glControl.Paint += RenderFrame;

            _timer = new Timer();
            _timer.Interval = 16; // примерно 60 FPS
            _timer.Tick += UpdateFrame;
            _timer.Start();

            if (User.Instance.Id == 1) firstNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
            else secondNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));

        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            _game.Update(_timer.Interval / 3000.0);

            glControl.Invalidate();
        }

        private void RenderFrame(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();

            TextureRenderer.Begin(glControl.Width, glControl.Height);

            _game.Render(_timer.Interval / 3000.0);

            glControl.SwapBuffers();
        }

        static string ReadDataFromSocket(Socket socket)
        {
            var buffer = new byte[1024 * 64 * 100];
            int bytesRead = socket.Receive(buffer);

            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }
    }
}