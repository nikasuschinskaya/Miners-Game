using Miners.Presentation.Models;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
            startButton.Click += (sender, e) => OpenGameForm();
            
            nameTextBox.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
        }

        private void OpenGameForm() => new GameForm().Show();
    }
}
