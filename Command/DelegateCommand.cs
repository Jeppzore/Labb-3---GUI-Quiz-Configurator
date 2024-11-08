using System.Windows.Input;

namespace Labb_3___GUI_Quiz.Command
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object?, bool> canExecute;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<Object> execute, Func<object?, bool> canExecute = null!)
        {
            ArgumentNullException.ThrowIfNull(execute);
            this.execute = execute;
            this.canExecute = canExecute ?? (_ => true);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute is null ? true : canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter!);
        }
    }
}
