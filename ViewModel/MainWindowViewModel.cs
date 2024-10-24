
using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel { get; }

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
        }
    }
}
