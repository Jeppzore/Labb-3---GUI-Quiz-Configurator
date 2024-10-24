using Labb_3___GUI_Quiz.Command;
using System.Windows.Threading;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;

        private string _testData;

        public string TestData
        {
            get => _testData;
            private set
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            TestData = "Start value: ";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();

            UpdateButtonCommand = new DelegateCommand(UpdateButton, CanUpdateButton);
            //AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
        }

        private bool CanUpdateButton(object? arg)
        {
            return TestData.Length < 20;
        }

        private void UpdateButton(object obj)
        {
            TestData += "x";
            UpdateButtonCommand.RaiseCanExecuteChanged();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TestData += "x";
        }
    }
}
