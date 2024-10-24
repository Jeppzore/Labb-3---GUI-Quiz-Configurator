namespace Labb_3___GUI_Quiz.Model
{
    enum Difficulty { Easy, Medium, Hard }

    internal class QuestionPack
    {
        // When creating a new objekt of QuestionPack, the default value of difficulty and timelimit are set.
        public QuestionPack(string name, Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

        public string Name { get; set; }

        public Difficulty Difficulty { get; set; }

        public int TimeLimitInSeconds { get; set; }

        public List<Question> Questions { get; set; }
    }
}
