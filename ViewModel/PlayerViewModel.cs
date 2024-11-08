using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Labb_3___GUI_Quiz.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        public MainWindowViewModel ?MainWindowViewModel { get; }

        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly Random _random = new Random();

        private DispatcherTimer _timer;

        private ObservableCollection<Question> _randomizedQuestions;

        private Question ?_currentQuestion;

        private string[] ?_currentAnswers;

        private int _currentQuestionCount;
        private int _totalQuestions;

        private int _correctAnswersCount = 0;

        private int _timePerQuestion;

        public int TimePerQuestion
        {
            get => _timePerQuestion;
            set
            {
                _timePerQuestion = value;
                RaisePropertyChanged(nameof(TimePerQuestion));
            }
        }

        public int CurrentQuestionCount
        {
            get => _currentQuestionCount;
            set
            {
                _currentQuestionCount = value;
                RaisePropertyChanged(nameof(CurrentQuestionCount));
                RaisePropertyChanged(nameof(QuestionProgress));
            }
        }

        public int TotalQuestions
        {
            get => _totalQuestions;
            set
            {
                _totalQuestions = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(QuestionProgress));
            }
        }
        public ObservableCollection<Question> RandomizedQuestions
        {
            get => _randomizedQuestions;
            set
            {
                _randomizedQuestions = value;
                RaisePropertyChanged(nameof(RandomizedQuestions));
            }
        }
        public string QuestionProgress
        {
            get
            {
                return $"Question {CurrentQuestionCount} out of {TotalQuestions}";
            }
        }

        public Question? CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value!;
                RaisePropertyChanged(nameof(CurrentQuestion));
                _currentAnswers = GetShuffledAnswers(CurrentQuestion!);

                RaisePropertyChanged(nameof(Answer1));
                RaisePropertyChanged(nameof(Answer2));
                RaisePropertyChanged(nameof(Answer3));
                RaisePropertyChanged(nameof(Answer4));
            }
        }

        private Dictionary<string, Brush> _buttonBackgrounds = new Dictionary<string, Brush>();
        public Dictionary<string, Brush> ButtonBackgrounds
        {
            get => _buttonBackgrounds;
            set
            {
                _buttonBackgrounds = value;
                RaisePropertyChanged(nameof(ButtonBackgrounds));
            }
        }

        private bool _areButtonsEnabled = true;
        public bool AreButtonsEnabled
        {
            get => _areButtonsEnabled;
            set
            {
                _areButtonsEnabled = value;
                RaisePropertyChanged(nameof(AreButtonsEnabled));
            }
        }

        private bool _isAnsweringEnabled = true;
        public bool IsAnsweringEnabled
        {
            get => _isAnsweringEnabled;
            set
            {
                _isAnsweringEnabled = value;
                RaisePropertyChanged(nameof(IsAnsweringEnabled));
            }
        }

        private bool _isAnswerSelected;
        public bool IsAnswerSelected
        {
            get => _isAnswerSelected;
            set
            {
                _isAnswerSelected = value;
                RaisePropertyChanged(nameof(IsAnswerSelected));
            }
        }

        // Keeps track of every buttons respective color

        private Brush ?_answer1Background;
        public Brush Answer1Background
        {
            get => _answer1Background!;
            set { _answer1Background = value; RaisePropertyChanged(nameof(Answer1Background)); }
        }

        private Brush ?_answer2Background;
        public Brush Answer2Background
        {
            get => _answer2Background!;
            set { _answer2Background = value; RaisePropertyChanged(nameof(Answer2Background)); }
        }

        private Brush ?_answer3Background;
        public Brush Answer3Background
        {
            get => _answer3Background!;
            set { _answer3Background = value; RaisePropertyChanged(nameof(Answer3Background)); }
        }

        private Brush ?_answer4Background;
        public Brush Answer4Background
        {
            get => _answer4Background!;
            set { _answer4Background = value; RaisePropertyChanged(nameof(Answer4Background)); }
        }

        public string Answer1 => _currentAnswers?.Length > 0 ? _currentAnswers[0] : string.Empty;
        public string Answer2 => _currentAnswers?.Length > 1 ? _currentAnswers[1] : string.Empty;
        public string Answer3 => _currentAnswers?.Length > 2 ? _currentAnswers[2] : string.Empty;
        public string Answer4 => _currentAnswers?.Length > 3 ? _currentAnswers[3] : string.Empty;

        public DelegateCommand SelectedAnswer { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _randomizedQuestions = new ObservableCollection<Question>();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += QuestionTimer;

            SelectedAnswer = new DelegateCommand(HandleSelectedAnswer);
        }

        private void HandleSelectedAnswer(object? selectedAnswer)
        {
            if (selectedAnswer is string answer)
            {
                OnSelectedAnswer(answer);
            }
        }

        private async void OnSelectedAnswer(string selectedAnswer)
        {
            if (!IsAnsweringEnabled) return;
            IsAnsweringEnabled = false;

            SelectedAnswer.RaiseCanExecuteChanged();

            if (selectedAnswer == CurrentQuestion!.CorrectAnswer)
            {
                _correctAnswersCount++;
                UpdateAnswerBackground(selectedAnswer, Brushes.Green);
            }
            else
            {
                UpdateAnswerBackground(selectedAnswer, Brushes.Red);
                SelectedAnswer.RaiseCanExecuteChanged();
                await Task.Delay(300);
                UpdateAnswerBackground(CurrentQuestion!.CorrectAnswer!, Brushes.Yellow);
            }

            _timer.Stop();
            await Task.Delay(1500);

            SelectedAnswer.RaiseCanExecuteChanged();
            _timer.Start();

            IsAnsweringEnabled = true;
            LoadNextQuestion();
        }

        // When player fails to answer in time
        private async void OnSelectedAnswerTimeout(string selectedAnswer)
        {

            if (!IsAnsweringEnabled) return;
            IsAnsweringEnabled = false;
            SelectedAnswer.RaiseCanExecuteChanged();

            // Shows the correct answer while not giving the player any score

            if (selectedAnswer == CurrentQuestion!.CorrectAnswer)
            {
                UpdateAnswerBackground(selectedAnswer, Brushes.Yellow);
            }

            _timer.Stop();
            await Task.Delay(1500);

            ResetAnswerBackgrounds();

            SelectedAnswer.RaiseCanExecuteChanged();
            _timer.Start();

            IsAnsweringEnabled = true;
            LoadNextQuestion();
        }

        private void UpdateAnswerBackground(string selectedAnswer, Brush color)
        {
            if (selectedAnswer == Answer1) Answer1Background = color;
            else if (selectedAnswer == Answer2) Answer2Background = color;
            else if (selectedAnswer == Answer3) Answer3Background = color;
            else if (selectedAnswer == Answer4) Answer4Background = color;
        }

        private void ResetAnswerBackgrounds()
        {
            Answer1Background = Brushes.Transparent;
            Answer2Background = Brushes.Transparent;
            Answer3Background = Brushes.Transparent;
            Answer4Background = Brushes.Transparent;
        }

        private void QuestionTimer(object? sender, EventArgs e)
        {
            if (TimePerQuestion > 0)
            {
                TimePerQuestion--;
            }
            else
            {
                OnSelectedAnswerTimeout(CurrentQuestion!.CorrectAnswer!);
                TimePerQuestion = _mainWindowViewModel!.ActivePack!.TimeLimitInSeconds;
            }
        }

        public void StartQuiz(ObservableCollection<Question> questions, int timeLimitInSeconds)
        {
            if (RandomizedQuestions == null || questions.Count == 0)
            {
                return;
            }
            RandomizedQuestions = new ObservableCollection<Question>(questions.OrderBy(q => _random.Next()).ToList());

            if (RandomizedQuestions.Any())
            {
                TotalQuestions = RandomizedQuestions.Count;
                CurrentQuestionCount = 1;
                RaisePropertyChanged(nameof(QuestionProgress));
            }

            TimePerQuestion = _mainWindowViewModel!.ActivePack!.TimeLimitInSeconds;
            LoadNextQuestion();
            _timer.Start();
        }

        private void LoadNextQuestion()
        {
            ResetAnswerBackgrounds();

            if (RandomizedQuestions.Count > 0)
            {
                CurrentQuestion = RandomizedQuestions[0];
                RandomizedQuestions.RemoveAt(0);
                CurrentQuestionCount = TotalQuestions - RandomizedQuestions.Count;

                TimePerQuestion = _mainWindowViewModel!.ActivePack!.TimeLimitInSeconds;
            }
            else
            {
                // If there are no more questions to load, stop the quiz.
                _timer.Stop();
                StopQuiz();
            }
        }
        private string[] GetShuffledAnswers(Question question)
        {
            // Get the incorrect answers and add the correct answer
            var answers = question.IncorrectAnswers!
                                  .Append(question.CorrectAnswer)
                                  .ToList();

            // Shuffle the order of the answers and return them as an array.
            var shuffledAnswers = answers
                                  .OrderBy(_ => _random.Next())
                                  .ToArray();

            return shuffledAnswers!;
        }

        public void ResetButtonBackgrounds()
        {
            foreach (var key in ButtonBackgrounds.Keys.ToList())
            {
                ButtonBackgrounds[key] = Brushes.Transparent;
            }
            RaisePropertyChanged(nameof(ButtonBackgrounds));
        }

        public void StopQuiz()
        {
            _timer.Stop();
            RandomizedQuestions.Clear();

            var Result = MessageBox.Show($"You scored a '{_correctAnswersCount}' of a total of {TotalQuestions}.\nTry again?", $"Restart ?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                //Load StartQuiz
                _correctAnswersCount = 0;
                MessageBox.Show($"Restarting {_mainWindowViewModel!.ActivePack}");
                StartQuiz(_mainWindowViewModel!.ActivePack!.Questions, _mainWindowViewModel!.ActivePack.TimeLimitInSeconds);
            }
            else if (Result == MessageBoxResult.No)
            {
                _correctAnswersCount = 0;
                _mainWindowViewModel!.ShowConfigurationView(this);
            }
        }
    }
}
