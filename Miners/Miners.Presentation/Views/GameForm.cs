using Miners.Presentation.GameWindows;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class GameForm : Form
    {
        public GameForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            glControl.Resize += (sender, eventArgs) => Resize();
            glControl.Paint += (sender, eventArgs) => Paint();

        }

        private void Resize()
        {
            glControl.MakeCurrent();   
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void Paint()
        {
            glControl.MakeCurrent();
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            glControl.SwapBuffers();
        }
    }
}