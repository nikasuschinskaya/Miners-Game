using Miners.Presentation.Models;
using System;
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

        private void OpenGameForm() => new GameForm().Show();
    }
}
