using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
            startButton.Click += (sender, e) => OpenGameForm();
        }

        private void OpenGameForm() => new GameForm().Show();
    }
}
