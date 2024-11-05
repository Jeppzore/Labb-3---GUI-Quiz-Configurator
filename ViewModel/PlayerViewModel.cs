﻿using Labb_3___GUI_Quiz.Command;
using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Labb_3___GUI_Quiz.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        public MainWindowViewModel MainWindowViewModel { get; }

        private readonly MainWindowViewModel? _mainWindowViewModel;
        private readonly Random _random = new Random();

        private DispatcherTimer _timer;

        private ObservableCollection<Question> _randomizedQuestions;

        private Question _currentQuestion;

        private string[] _currentAnswers;

        private int _currentQuestionCount;
        private int _totalQuestions;

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

        public Question ?CurrentQuestion
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

        private void OnSelectedAnswer(string selectedAnswer)
        {
            if (selectedAnswer == CurrentQuestion!.CorrectAnswer)
            {
                MessageBox.Show("Correct!");
                LoadNextQuestion();
            }
            else
            {
                MessageBox.Show("WROOOONG!");
                LoadNextQuestion();
            }
        }

        private void QuestionTimer(object? sender, EventArgs e)
        {
            if (TimePerQuestion > 0)
            {
                TimePerQuestion--;
            }
            else
            {
                LoadNextQuestion();
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
            if (RandomizedQuestions.Count > 0)
            {
                CurrentQuestion = RandomizedQuestions[0];
                RandomizedQuestions.RemoveAt(0);
                CurrentQuestionCount = TotalQuestions - RandomizedQuestions.Count;

                TimePerQuestion = _mainWindowViewModel!.ActivePack!.TimeLimitInSeconds;
            }
            else
            {
                _timer.Stop();
            }
        }
        private string[] GetShuffledAnswers(Question question)
        {
            var answers = question.IncorrectAnswers!.Append(question.CorrectAnswer).ToList();
            return answers.OrderBy(a => _random.Next()).ToArray();
        }

        public void StopQuiz()
        {
            _timer.Stop();
            RandomizedQuestions.Clear();
            
        }
    }
}
