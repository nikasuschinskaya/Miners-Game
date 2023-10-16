using Miners.Presentation.Models;
using Miners.Presentation.ViewModels;
using System.Windows.Forms;

namespace Miners.Presentation.Views
{
    public partial class LauncherForm : Form
    {
        //private LauncherViewModel _launcherViewModel;
        public LauncherForm()
        {
            InitializeComponent();
            startButton.Click += (sender, e) => OpenGameForm();
            //_launcherViewModel = new LauncherViewModel();   
            
            nameTextBox.DataBindings.Add(new Binding("Text", User.Instance, "Name"));
        }

        private void OpenGameForm()
        {
            //MessageBox.Show(_gameViewModel.UserName);
            new GameForm().Show();
        }
    }
}
