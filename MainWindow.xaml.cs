using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;
using Labb_3___GUI_Quiz.Services;
using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;

namespace Labb_3___GUI_Quiz
{
    public partial class MainWindow : Window
    {

        private MainWindowViewModel _mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModelPacks = _mainWindowViewModel.Packs.ToList();
            var packs = new List<QuestionPack>();
            foreach (var viewModelPack in viewModelPacks)
            {
                var pack = new QuestionPack
                {
                    Difficulty = viewModelPack.Difficulty,
                    Name = viewModelPack.Name,
                    TimeLimitInSeconds = viewModelPack.TimeLimitInSeconds,
                    Questions = viewModelPack.Questions.ToList(),
                };
                packs.Add(pack);
            }

            QuizManagerService.SaveQuestionPacks(packs);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var loadedPacks = await QuizManagerService.LoadQuestionPacks("QuestionPacks.json");
            var viewModelPacks = _mainWindowViewModel.Packs;
            foreach (var loadedPack in loadedPacks)
            {
                var questionPackViewModel = new QuestionPackViewModel(loadedPack);
                viewModelPacks.Add(questionPackViewModel);
            }
            _mainWindowViewModel.ActivePack = _mainWindowViewModel.Packs.FirstOrDefault();

        }
    }
}