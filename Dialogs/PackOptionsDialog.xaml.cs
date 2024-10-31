using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;

namespace Labb_3___GUI_Quiz.Dialogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PackOptionsDialog : Window
    {
        internal PackOptionsDialog(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            //DataContext = new QuestionPack("<PackName>", Difficulty.Medium, 30);
        }
    }
}
