using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;
using Labb_3___GUI_Quiz.Services;

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
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}