using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;

namespace Labb_3___GUI_Quiz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}