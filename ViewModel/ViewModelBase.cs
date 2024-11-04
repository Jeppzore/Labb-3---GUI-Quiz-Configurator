using Labb_3___GUI_Quiz.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Labb_3___GUI_Quiz.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
