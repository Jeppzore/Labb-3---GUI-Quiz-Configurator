using Labb_3___GUI_Quiz.Command;
using System.Windows.Threading;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        private DispatcherTimer ?_timer;

        private int _timeRemaining;
        public int TimeRemaining
        {
            get => _timeRemaining;
            private set
            {
                _timeRemaining = value;
                RaisePropertyChanged(nameof(TimeRemaining));
            }
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            if (this.mainWindowViewModel?.ActivePack != null)
            {
                TimeRemaining = this.mainWindowViewModel.ActivePack.TimeLimitInSeconds;
            }

            // Starta timern
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
            }
            else
            {
                _timer!.Stop();
            }
        }
    }
}
        //private string ?_testData;

        //public string TestData
        //{
        //    get => _testData!;
        //    private set
        //    {
        //        _testData = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public EventHandler? Timer_Tick { get; }

        //public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        //{
        //    this.mainWindowViewModel = mainWindowViewModel;

        //    TestData = "Start value: ";

        //    _timer = new DispatcherTimer();
        //    _timer.Interval = TimeSpan.FromSeconds(1);
        //    _timer.Tick += Timer_Tick;
        //    _timer.Start();

        //}

        //private bool CanUpdateButton(object? arg)
        //{
        //    return TestData.Length < 20;
        //}

        //private void UpdateButton(object obj)
        //{
        //    TestData += "x";
        //    UpdateButtonCommand.RaiseCanExecuteChanged();
        //}

        //private void Timer_Tick(object? sender, EventArgs e)
        //{
        //    TestData += "x";          
        //}
