using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Labb_3___GUI_Quiz.Views
{

    public partial class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var viewModel = (ConfigurationViewModel)DataContext;
            viewModel.SaveQuestions();

        }
    }
}
