
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
        public DelegateCommand SelectQuestionPackCommand { get; }
        public DelegateCommand RemoveQuestionPackCommand { get; }
        public DelegateCommand TestCommand { get; }
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
            Packs = new ObservableCollection<QuestionPackViewModel>();

            ConfigurationViewModel = new ConfigurationViewModel(this, LocalDataService);
            PlayerViewModel = new PlayerViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));      
            Packs.Add(ActivePack);

            ShowPlayerCommand = new DelegateCommand(_ => ShowPlayerView());
            ShowConfigurationCommand = new DelegateCommand(param => ShowConfigurationView());

            AddQuestionPackCommand = new DelegateCommand(AddQuestionPack, CanAddQuestionPack);
            ConfirmAndCreateQuestionPackCommand = new DelegateCommand(CreateQuestionPack);
            SelectQuestionPackCommand = new DelegateCommand(SelectQuestionPack);
            RemoveQuestionPackCommand = new DelegateCommand(RemoveQuestionPack);

            TestCommand = new DelegateCommand(Test);
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
                    // Logik för att sätta ny ActivePack till det senast tillagda QuestionPack
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
                MessageBox.Show($"Selected {ActivePack}");
                // Här kan du lägga till mer logik om du vill göra något specifikt med det valda question packet.
            }
        }

        private void Test(object obj)
        {
            MessageBox.Show("Testing");
        }

        private void UpdateActivePack()
        {
            if (Packs.Contains(ActivePack))
            {
                int index = Packs.IndexOf(ActivePack);
                Packs[index] = ActivePack; // Uppdaterar och triggar notifiering i gränssnittet
            }
            else
            {
                Packs.Add(ActivePack);
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
    }
}
