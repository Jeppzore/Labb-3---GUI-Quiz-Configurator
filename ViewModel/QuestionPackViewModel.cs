using Labb_3___GUI_Quiz.Model;
using System.Collections.ObjectModel;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        // Fields
        private readonly QuestionPack model;


        // Properties
        public ObservableCollection<Question> Questions { get; }

        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
            }
        }

        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
            }
        }

        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged();
            }
        }

        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
        }

    }
}
