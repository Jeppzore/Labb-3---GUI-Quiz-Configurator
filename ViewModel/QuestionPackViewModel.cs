using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;

        public ObservableCollection<Question> Questions { get; }

        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
                RaisePropertyChanged(nameof(Difficulty));
            }
        }

        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged(nameof(TimeLimitInSeconds));
            }
        }

        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
        }
        public override string ToString()
        {
            return $"{Name} ({Difficulty}) - {TimeLimitInSeconds} seconds";
        }

    }
}
