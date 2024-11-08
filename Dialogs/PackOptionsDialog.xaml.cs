using Labb_3___GUI_Quiz.ViewModel;
using System.Windows;

namespace Labb_3___GUI_Quiz.Dialogs
{
    public partial class PackOptionsDialog : Window
    {
        internal PackOptionsDialog(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}
