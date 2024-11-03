using System.ComponentModel;

namespace Labb_3___GUI_Quiz.Model
{
    enum Difficulty { Easy, Medium, Hard }

    internal class QuestionPack : INotifyPropertyChanged
    {
        private string ?_name;
        private Difficulty _difficulty;
        private int _timeLimitInSeconds;

        public string Name
        {
            get => _name!;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public Difficulty Difficulty
        {
            get => _difficulty;
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnPropertyChanged(nameof(Difficulty));
                }
            }
        }

        public int TimeLimitInSeconds
        {
            get => _timeLimitInSeconds;
            set
            {
                if (_timeLimitInSeconds != value)
                {
                    _timeLimitInSeconds = value;
                    OnPropertyChanged(nameof(TimeLimitInSeconds));
                }
            }
        }

        public List<Question> Questions { get; set; }


        // When creating a new objekt of QuestionPack, the default value of difficulty and timelimit are set.
        public QuestionPack(string name = "New Pack", Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
