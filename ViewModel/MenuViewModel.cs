using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;

namespace Labb_3___GUI_Quiz.ViewModel
{
    class MenuViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public DelegateCommand OpenCreateNewPack { get; }
        public DelegateCommand OpenPackOptions { get; }


        public MenuViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            OpenCreateNewPack = new DelegateCommand(CreateNewPackHandler);
            OpenPackOptions = new DelegateCommand(PackOptionsHandler);
        }
        private void CreateNewPackHandler(object? obj)
        {
            CreateNewPackDialog createNewPackDialog = new();

            createNewPackDialog.ShowDialog();

        }
        private void PackOptionsHandler(object? obj)
        {
            PackOptionsDialog packOptionsDialog = new();

            packOptionsDialog.ShowDialog();
        }
    }
}
