namespace TrayTimer.Helpers
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        readonly Action _execute;
        readonly Action<object> _executeWithParameter;
        readonly Predicate<object> _canExecute;
        private Action<string> mY;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _executeWithParameter = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute();
            else
                _executeWithParameter(parameter);
        }

        #endregion
    }
}