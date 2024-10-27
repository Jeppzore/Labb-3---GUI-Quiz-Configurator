using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;

namespace Labb_3___GUI_Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            var pack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            pack.TimeLimitInSeconds = 5;


        }
    }
}