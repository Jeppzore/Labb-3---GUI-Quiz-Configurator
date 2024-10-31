
using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;
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
        public LocalDataService LocalDataService { get; }


        public ICommand ShowPlayerCommand { get; }
        public ICommand ShowConfigurationCommand { get; }
        public ICommand ShowPackDialogCommand { get; }

        public DelegateCommand ConfirmAndCreateQuestionPackCommand { get; }
        public DelegateCommand AddQuestionPackCommand { get; }

        private QuestionPackViewModel? _activePack;

        private CreateNewPackDialog _newPackDialog;

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

        public QuestionPackViewModel? NewPack { get; set; }

        public MainWindowViewModel()
        {
            ConfigurationViewModel = new ConfigurationViewModel(this, LocalDataService);
            PlayerViewModel = new PlayerViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));

            ShowPlayerCommand = new DelegateCommand(_ => ShowPlayerView());
            ShowConfigurationCommand = new DelegateCommand(param => ShowConfigurationView());
            ShowPackDialogCommand = new DelegateCommand(param => ShowPackDialog());

            ConfirmAndCreateQuestionPackCommand = new DelegateCommand(CreateQuestionPack);
            AddQuestionPackCommand = new DelegateCommand(AddQuestionPack, CanAddQuestionPack);
        }
        private void AddQuestionPack(object obj)
        {
            NewPack = new QuestionPackViewModel(new QuestionPack("New Pack"));

            _newPackDialog = new CreateNewPackDialog();
            _newPackDialog.ShowDialog();
        }

        private void CreateQuestionPack(object obj)
        {
            Packs.Add(NewPack!);
            ActivePack = NewPack;
            _newPackDialog.Close();
        }


        private bool CanAddQuestionPack(object? arg)
        {
            return true;
        }


        // Methods to show the correct view

        public void ShowPlayerView(object? parameter = null)
        {
            IsPlayerViewVisible = true;
            IsConfigurationViewVisible = false;
        }
        public void ShowConfigurationView(object? parameter = null)
        {
            IsPlayerViewVisible = false;
            IsConfigurationViewVisible = true;
        }

        // DENNA KODEN BEHÖVER TAS BORT?! SKALL KÖRAS I METODEN "ADD QUESTION PACK"
        public void ShowPackDialog()
        {
            // Skapa och visa det nya fönstret
            var packDialogWindow = new CreateNewPackDialog();
            packDialogWindow.ShowDialog();
        }
    }
}
