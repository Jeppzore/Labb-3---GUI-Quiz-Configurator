
using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel { get; }
        public MenuViewModel MenuViewModel { get; }
        public LocalDataService LocalDataService { get; }
        public WindowStyle WindowStyle { get; set; }


        public ICommand ShowPlayerCommand { get; }
        public ICommand ShowConfigurationCommand { get; }
        public ICommand ShowPackDialogCommand { get; }
        public ICommand ShowPackOptionsDialogCommand { get; }


        private QuestionPackViewModel? _activePack;
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }

        public MainWindowViewModel()
        {
            ConfigurationViewModel = new ConfigurationViewModel(this, MenuViewModel, LocalDataService);
            PlayerViewModel = new PlayerViewModel(this);
            MenuViewModel = new MenuViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));

            ShowPlayerCommand = new DelegateCommand(_ => ShowPlayerView());
            ShowConfigurationCommand = new DelegateCommand(param => ShowConfigurationView());
            ShowPackDialogCommand = new DelegateCommand(param => ShowPackDialog());
            ShowPackOptionsDialogCommand = new DelegateCommand(param => ShowPackOptionsDialog());

        }
    }
}
