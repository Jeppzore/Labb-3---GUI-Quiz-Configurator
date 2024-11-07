
using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;
using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel { get; }
        public LocalDataService LocalDataService { get; }

        public DelegateCommand ShowPlayerCommand { get; }
        public DelegateCommand ShowConfigurationCommand { get; }

        public DelegateCommand ConfirmAndCreateQuestionPackCommand { get; }
        public DelegateCommand SelectQuestionPackCommand { get; }
        public DelegateCommand RemoveQuestionPackCommand { get; }
        public DelegateCommand AddQuestionPackCommand { get; }

        private bool _isPlayerViewVisible;
        private bool _isConfigurationViewVisible;

        public bool IsPlayerViewVisible
        {
            get { return _isPlayerViewVisible; }
            set
            {
                _isPlayerViewVisible = value;
                RaisePropertyChanged(nameof(IsPlayerViewVisible));
            }
        }

        public bool IsConfigurationViewVisible
        {
            get { return _isConfigurationViewVisible; }
            set
            {
                _isConfigurationViewVisible = value;
                RaisePropertyChanged(nameof(IsConfigurationViewVisible));
            }
        }


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
            IsConfigurationViewVisible = true;
            Packs = new ObservableCollection<QuestionPackViewModel>();

            ConfigurationViewModel = new ConfigurationViewModel(this, LocalDataService);
            PlayerViewModel = new PlayerViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            Packs.Add(ActivePack);

            ShowPlayerCommand = new DelegateCommand(ShowPlayerView);
            ShowConfigurationCommand = new DelegateCommand(ShowConfigurationView);

            AddQuestionPackCommand = new DelegateCommand(AddQuestionPack, CanAddQuestionPack);
            ConfirmAndCreateQuestionPackCommand = new DelegateCommand(CreateQuestionPack);
            SelectQuestionPackCommand = new DelegateCommand(SelectQuestionPack);
            RemoveQuestionPackCommand = new DelegateCommand(RemoveQuestionPack);
        }

        private void RemoveQuestionPack(object obj)
        {
            if (ActivePack != null)
            {
                var Result = MessageBox.Show($"Do you want to delete: '{ActivePack}'?", $"Delete: '{ActivePack}'?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Deleted {ActivePack}");
                    Packs.Remove(ActivePack);

                    if (Packs.Count > 0)
                    {
                        ActivePack = Packs[^1]; // Sätter ActivePack till det sista objektet i listan
                        SelectQuestionPack(ActivePack);
                    }
                    else
                    {
                        ActivePack = null; // Om listan är tom
                    }

                }
                else if (Result == MessageBoxResult.No)
                {
                    return;
                }
            }
        }

        private void SelectQuestionPack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
            }
        }

        private void AddQuestionPack(object obj)
        {
            NewPack = new QuestionPackViewModel(new QuestionPack());

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

        public void ShowPlayerView(object? obj)
        {
            if (ActivePack != null && ActivePack.Questions.Count > 0)
            {
                var Result = MessageBox.Show($"Do you want to Play: '{ActivePack}'?", $"Play: '{ActivePack}'?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    PlayerViewModel.StartQuiz(ActivePack.Questions, ActivePack.TimeLimitInSeconds);
                }
                else if (Result == MessageBoxResult.No)
                {
                    return;
                }

                IsPlayerViewVisible = true;
                IsConfigurationViewVisible = false;
            }

        }
        public void ShowConfigurationView(object? obj)
        {
            IsPlayerViewVisible = false;
            IsConfigurationViewVisible = true;
        }
    }
}
