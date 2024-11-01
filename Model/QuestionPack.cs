using System.ComponentModel;

namespace Labb_3___GUI_Quiz.Model
{
    enum Difficulty { Easy, Medium, Hard }

    internal class QuestionPack : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public Difficulty Difficulty { get; set; }

        public int TimeLimitInSeconds { get; set; }

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
    }
}
