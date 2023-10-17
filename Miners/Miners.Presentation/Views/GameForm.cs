using Miners.Engine;
using Miners.Presentation.Models;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
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
            glControl.Invalidate(); // Вызов отрисовки
        }

        private void Resize()
        {
            glControl.MakeCurrent();
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void Paint()
        {
            glControl.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            _game.Render();


            glControl.SwapBuffers();
        }
    }
}