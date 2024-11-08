using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;
using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly QuizManagerService? _quizManagerService;

        public DelegateCommand RemoveQuestion { get; }
        public DelegateCommand AddQuestion { get; }
        public DelegateCommand ShowOptionDialog { get; }

        public DelegateCommand PlayerAnswerCorrect { get; }
        public DelegateCommand PlayerAnswerWrong { get; }

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

        private Random _random = new Random();

        public ObservableCollection<string> ShuffledAnswers { get; set; } = new ObservableCollection<string>();

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel, QuizManagerService? quizManagerService)
        {
            this._mainWindowViewModel = mainWindowViewModel;

            AddQuestion = new DelegateCommand(AddQuestionHandler); //(CanAddQuestion)
            RemoveQuestion = new DelegateCommand(RemoveQuestionHandler, CanRemoveQuestion);
            ShowOptionDialog = new DelegateCommand(ShowPackOptionsDialog);
        }
        public void ShowPackOptionsDialog(object? obj)
        {
            var packOptionsDialogWindow = new PackOptionsDialog(_mainWindowViewModel!);
            packOptionsDialogWindow.ShowDialog();
        }

        private void RemoveQuestionHandler(object? obj)
        {
            if (SelectedQuestion != null)
            {
                ActivePack?.Questions.Remove(SelectedQuestion);
                SelectedQuestion = null;
            }
        }
        private bool CanRemoveQuestion(object? obj) => SelectedQuestion != null;

        private void AddQuestionHandler(object? obj)
        {
            if (ActivePack != null)
            {
                var newQuestion = new Question("<New Question>", "", "", "", "");
                ActivePack?.Questions.Add(newQuestion);
                SelectedQuestion = newQuestion;
            }
        }
    }
}
