using Miners.Presentation.Commands;
using Miners.Presentation.ViewModels.Base;
using Miners.Presentation.Views;
using System.Windows.Input;

namespace Miners.Presentation.ViewModels
{
    public class LauncherViewModel : BaseViewModel
    {
        public ICommand StartButtonClick { get; set; }

        public LauncherViewModel()
        {
            StartButtonClick = new RelayCommand(OnStartButtonClicked);
        }

        private void OnStartButtonClicked(object parameter) => new GameForm().Show();
    }
}
