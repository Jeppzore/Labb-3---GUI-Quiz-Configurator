using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Labb_3___GUI_Quiz.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();

        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var viewModel = (DataContext as MainWindowViewModel).ConfigurationViewModel;
            viewModel.SaveQuestions();

        }
    }
}
