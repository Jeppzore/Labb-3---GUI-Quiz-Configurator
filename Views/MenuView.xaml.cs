using System.Windows;
using System.Windows.Controls;

namespace Labb_3___GUI_Quiz.Views
{

    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void viewMenuFullscreen_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Application.Current.MainWindow;

            if (mainWindow.WindowState != WindowState.Maximized || mainWindow.WindowStyle != WindowStyle.None)
            {
                // Fullscreen
                mainWindow.WindowStyle = WindowStyle.None;
                mainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                // Reset to normal screen
                mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                mainWindow.WindowState = WindowState.Normal;
            }
        }

        private void fileMenuExit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow;
            mainWindow?.Close();
        }
    }
}
