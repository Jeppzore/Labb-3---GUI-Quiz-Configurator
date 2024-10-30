using Labb_3___GUI_Quiz.Model;
using System.Windows;

namespace Labb_3___GUI_Quiz.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewPackDialog.xaml
    /// </summary>
    public partial class CreateNewPackDialog : Window
    {
        public CreateNewPackDialog()
        {
            InitializeComponent();

            DataContext = new QuestionPack("New Question Pack", Difficulty.Medium, 30);
        }

        private void createPackButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
