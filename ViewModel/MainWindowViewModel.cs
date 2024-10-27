
using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel { get; }

        public ICommand ShowPlayerCommand { get; }
        public ICommand ShowConfigurationCommand { get; }
        public ICommand ShowPackDialogCommand {  get; }

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
            ConfigurationViewModel = new ConfigurationViewModel(this);
            PlayerViewModel = new PlayerViewModel(this);
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));

            ShowPlayerCommand = new DelegateCommand(param => ShowPlayerView());
            ShowConfigurationCommand = new DelegateCommand(param => ShowConfigurationView());
            ShowPackDialogCommand = new DelegateCommand(param => ShowPackDialog());
        }
    }
}
