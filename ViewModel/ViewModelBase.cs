using Labb_3___GUI_Quiz.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {

        private bool _isPlayerViewVisible;
        private bool _isConfigurationViewVisible;

        public bool IsPlayerViewVisible
        {
            get { return _isPlayerViewVisible; }
            set
            {
                _isPlayerViewVisible = value;
                RaisePropertyChanged(nameof(IsPlayerViewVisible));
            }
        }

        public bool IsConfigurationViewVisible
        {
            get { return _isConfigurationViewVisible; }
            set
            {
                _isConfigurationViewVisible = value;
                RaisePropertyChanged(nameof(IsConfigurationViewVisible));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Methods to show the correct view

        public void ShowPlayerView(object? parameter = null)
        {
            _isPlayerViewVisible = true;
            _isConfigurationViewVisible = false;
        }
        public void ShowConfigurationView(object? parameter = null)
        {
            IsPlayerViewVisible = false;
            IsConfigurationViewVisible = true;
        }

        public void ShowPackDialog()
        {
            // Skapa och visa det nya fönstret
            var packDialogWindow = new CreateNewPackDialog();
            packDialogWindow.Show();
        }
    }
}
