using Miners.Presentation.Models;
using Miners.Presentation.Render;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class GameForm : Form
    {
        private Game _game;
        private Timer _timer;
        private FrameEventArgs _frameEventArgs;

        public GameForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _frameEventArgs = new FrameEventArgs();
            glControl.VSync = true;
            glControl.Paint += RenderFrame;


            _game = new Game();

            // Создание таймера для обновления кадров
            _timer = new Timer();
            _timer.Interval = 16; // Устанавливаем желаемый интервал обновления (примерно 60 FPS)
            _timer.Tick += UpdateFrame;
            _timer.Start();
            //_timer = new Timer();
            //_timer.Interval = 16; // Примерно 60 FPS
            //_timer.Tick += (sender, eventArgs) => TimerTick();
            //_timer.Start();


            if (User.Instance.Id == 1) firstNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
            else secondNameLabel.DataBindings.Add(new Binding("Text", User.Instance, "Name"));

        }

        //private void TimerTick()
        //{
        ////    var frameEventArgs = new FrameEventArgs();
        ////    _game.Update(frameEventArgs);
        //    //_game.AddRandomMine();
        //    glControl.Invalidate();
        //}

        ////private void Resize()
        ////{
        ////    glControl.MakeCurrent();
        ////    GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        ////}

        //private void Paint()
        //{
        //    glControl.MakeCurrent();

        //    TextureRenderer.Begin(glControl.Width, glControl.Height);

        //    _game.Render(_frameEventArgs);

        //    glControl.SwapBuffers();
        //}


        private void UpdateFrame(object sender, EventArgs e)
        {
            //if (sender is Timer timer)
            //{
            //    // Вызываем обновление логики игры с передачей времени, прошедшего с предыдущего кадра
            //    _game.Update(timer.Interval / 1000.0);

            //    // Запрашиваем перерисовку GLControl
            //    glControl.Invalidate();
            //}

            _game.Update(_timer.Interval / 1000.0);

            glControl.Invalidate();
        }


        private void RenderFrame(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();

            TextureRenderer.Begin(glControl.Width, glControl.Height);
            // Здесь производится отрисовка игровой сцены

            //if (sender is Timer timer)
            //{
            //    _game.Render(timer.Interval / 1000.0);
            //}

            _game.Render(_timer.Interval / 3000.0);
            // Вызов SwapBuffers, чтобы отобразить новый кадр
            glControl.SwapBuffers();
        }


    }
}