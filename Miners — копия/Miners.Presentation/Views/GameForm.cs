using Autofac;
using Miners.Presentation.Models;
using Miners.Presentation.Render;
using System;
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
            base.OnLoad(e);

            glControl.VSync = true;
            glControl.Paint += RenderFrame;

            _game = new Game();

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
    }
}