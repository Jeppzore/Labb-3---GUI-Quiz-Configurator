using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;

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
            //DataContext = new QuestionPack("New Question Pack", Difficulty.Medium, 30);
            DataContext = (App.Current.MainWindow as MainWindow).DataContext;
        }   

        private void createPackButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        //private void createPackButtonCreate_Click(object sender, RoutedEventArgs e)
        //{

        //    var packDialogWindow = (QuestionPackViewModel)DataContext;

        //    var newPack = new QuestionPack(packDialogWindow.Name, packDialogWindow.Difficulty, packDialogWindow.TimeLimitInSeconds);

        //    DialogResult = true;
        //    Close();
        //}
    }
}
