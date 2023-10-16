//using OpenTK;
//using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;
//using System;
//using System.Drawing;

//namespace Miners.Presentation.GameWindows
//{
//    public class Game : GameWindow
//    {
//        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
//        {
//            // Инициализация OpenGL
//            VSync = VSyncMode.On; // Включаем вертикальную синхронизацию (VSync) по умолчанию
//        }

//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//            GL.ClearColor(Color.CornflowerBlue);

//            // Вызов метода Initialize при загрузке окна
//            Initialize();
//        }

//        protected override void OnUpdateFrame(FrameEventArgs e)
//        {
//            base.OnUpdateFrame(e);
//            // Логика обновления игры

//            // Вызов метода Render на каждом обновлении
//            Render();
//        }

//        protected override void OnRenderFrame(FrameEventArgs e)
//        {
//            base.OnRenderFrame(e);
//            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
//            // Отрисовка игры
//            GL.Flush();
//            SwapBuffers();
//        }

//        protected override void OnResize(EventArgs e)
//        {
//            base.OnResize(e);
//            // Обработка изменения размера окна
//        }

//        protected override void OnClosed(EventArgs e)
//        {
//            base.OnClosed(e);
//            // Освобождение ресурсов при закрытии окна
//        }

//        // Метод инициализации игры
//        public void Initialize()
//        {
//            // Здесь вы можете выполнять все необходимые настройки и инициализацию игры.
//        }

//        // Метод отрисовки игры
//        public void Render()
//        {
//            // Здесь вы можете выполнять все операции рендеринга игры.
//        }
//    }
//}
