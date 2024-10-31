using Labb_3___GUI_Quiz.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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



    }
}
