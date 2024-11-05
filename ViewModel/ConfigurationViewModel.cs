using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Dialogs;
using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly LocalDataService? _localDataService;

        // Command Properties
        public DelegateCommand RemoveQuestion { get; }
        public DelegateCommand AddQuestion { get; }
        public DelegateCommand ShowOptionDialog { get; }

        public DelegateCommand PlayerAnswerCorrect { get; }
        public DelegateCommand PlayerAnswerWrong { get; }

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
                    //ShuffleAnswers();
                    RemoveQuestion.RaiseCanExecuteChanged();
                }
            }
        }

        private Random _random = new Random();

        public ObservableCollection<string> ShuffledAnswers { get; set; } = new ObservableCollection<string>();


        // TODO: Fixa så att Add´Question inte går att klicka på när QUestionPack inte finns.
        //private QuestionPack? _selectedQuestionPack;

        //public QuestionPack? SelectedQuestionPack
        //{
        //    get => _selectedQuestionPack;
        //    set
        //    {
        //        if (_selectedQuestionPack != value)
        //        {
        //            _selectedQuestionPack = value;
        //            RaisePropertyChanged();
        //            AddQuestion.RaiseCanExecuteChanged();
        //        }
        //    }
        //}



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

            AddQuestion = new DelegateCommand(AddQuestionHandler); //(CanAddQuestion)
            RemoveQuestion = new DelegateCommand(RemoveQuestionHandler, CanRemoveQuestion);
            ShowOptionDialog = new DelegateCommand(ShowPackOptionsDialog);

            PlayerAnswerCorrect = new DelegateCommand(CorrectAnswer);
            PlayerAnswerWrong = new DelegateCommand(WrongAnswer);
        }

        private void WrongAnswer(object obj)
        {
            MessageBox.Show("Wrong!");
        }

        private void CorrectAnswer(object obj)
        {
            MessageBox.Show("Correct!");
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
                _localDataService?.SaveQuestions(ActivePack?.Questions);
            }
        }
        private bool CanRemoveQuestion(object? obj) => SelectedQuestion != null;
        //private bool CanAddQuestion(object? obj) => SelectedQuestionPack != null;


        private void AddQuestionHandler(object? obj)
        {
            if (ActivePack  != null)
            {
                var newQuestion = new Question("<New Question>", "", "", "", "");
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
