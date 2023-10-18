using Miners.Presentation.Models;
using Miners.Presentation.Render;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;

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
            //    //_logger = scope.Resolve<ILogger<MainViewModel>>();
            //    _game = scope.ResolveOptional<Game>();
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            glControl.Resize += (sender, eventArgs) => Resize();
            glControl.Paint += (sender, eventArgs) => Paint();


            _game = new Game();
            _timer = new Timer();
            _timer.Interval = 16; // Примерно 60 FPS
            _timer.Tick += (sender, eventArgs) => TimerTick();
            _timer.Start();
            if (User.Instance.Id == 1) firstNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
            else secondNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));

        }

        private void TimerTick()
        {
            _game.Update();
            _game.AddRandomMine();
            glControl.Invalidate();
        }

        private void Resize()
        {
            glControl.MakeCurrent();
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void Paint()
        {
            glControl.MakeCurrent();

            TextureRenderer.Begin(glControl.Width, glControl.Height);
            _game.Render();


            glControl.SwapBuffers();
        }
    }
}