using System;
using System.Diagnostics;
using System.Windows.Input;

namespace XLargeImage.SourceCode.Common.Mvvm
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute, Func<bool> canexecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canexecute ?? (() => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p = null)
        {
            try { return _canExecute(); }
            catch { return false; }
        }

        public void Execute(object p = null)
        {
            if (!CanExecute(p))
                return;
            try { _execute(); }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error: {exception.Message}");
                Debugger.Break();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canexecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canexecute ?? (e => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p)
        {
            try
            {
                var value = (T)Convert.ChangeType(p, typeof(T));
                return _canExecute?.Invoke(value) ?? true;
            }
            catch { return false; }
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
                return;
            var value = (T)Convert.ChangeType(p, typeof(T));
            _execute(value);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
