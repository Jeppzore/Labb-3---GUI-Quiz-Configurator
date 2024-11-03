using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;
using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.Services;
using System.Windows.Input;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly LocalDataService _localDataService;

        // Command Properties
        public DelegateCommand RemoveQuestion { get; }
        public DelegateCommand AddQuestion { get; }
        public DelegateCommand ShowOptionDialog { get; }

        // Question Properties
        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }

        private Question? _selectedQuestion;

        public Question? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != value)
                {
                    _selectedQuestion = value;
                    RaisePropertyChanged();
                    RemoveQuestion.RaiseCanExecuteChanged();
                }
            }
        }


        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel, LocalDataService? localDataService)
        {
            this._mainWindowViewModel = mainWindowViewModel;
            _localDataService = localDataService ?? new LocalDataService();

            var loadedQuestions = _localDataService?.LoadQuestions();

            if (loadedQuestions != null)
            {
                foreach (var question in loadedQuestions)
                {
                    ActivePack?.Questions.Add(question);
                }
            }

            AddQuestion = new DelegateCommand(AddQuestionHandler);
            RemoveQuestion = new DelegateCommand(RemoveQuestionHandler, CanRemoveQuestion);
            ShowOptionDialog = new DelegateCommand(ShowPackOptionsDialog);           
        }

        public void ShowPackOptionsDialog(object? obj)
        {
            // Skapa och visa det nya fönstret
            var packOptionsDialogWindow = new PackOptionsDialog(_mainWindowViewModel!);
            packOptionsDialogWindow.ShowDialog();
        }

        private void RemoveQuestionHandler(object? obj)
        {
            if (SelectedQuestion != null)
            {
                ActivePack?.Questions.Remove(SelectedQuestion);
                SelectedQuestion = null;
                _localDataService?.SaveQuestions(ActivePack?.Questions);
            }
        }
        private bool CanRemoveQuestion(object? obj) => SelectedQuestion != null;

        private void AddQuestionHandler(object? obj)
        {
            if (ActivePack  != null)
            {
                var newQuestion = new Question("New Question", "", "", "", "");
                ActivePack?.Questions.Add(newQuestion);
                SelectedQuestion = newQuestion;
                _localDataService?.SaveQuestions(ActivePack?.Questions);
            }

        }
        public void SaveQuestions()
        {
            if (ActivePack?.Questions != null)
            {
                _localDataService?.SaveQuestions(ActivePack.Questions);
            }
        }

    }
}
