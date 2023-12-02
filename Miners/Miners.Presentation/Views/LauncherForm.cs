using Miners.Presentation.Models;
using Miners.Shared;
using System;
using System.Text;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class LauncherForm : Form
    {
        public LauncherForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            startButton.Click += (sender, eventArgs) => OpenGameForm();
            nameTextBox.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
        }

        private void OpenGameForm()
        {
            var nameRequest = $"{nameof(CommandType.NAME)} {nameTextBox.Text}";
            Program.ClientSocket.Send(Encoding.UTF8.GetBytes(nameRequest));

            var buffer = new byte[1024];
            Program.ClientSocket.Receive(buffer);

            var nameResponse = Encoding.UTF8.GetString(buffer);
            if (nameResponse.StartsWith(nameof(CommandType.OK)))
            {
                new GameForm().Show();
            }
        }
    }
}
