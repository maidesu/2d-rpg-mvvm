using System;
using System.Windows.Input;

namespace Lopakodo.ViewModel
{
    class Delegate : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, Boolean> _canExecute;

        public Delegate(Action<Object> execute) : this(null, execute) { }

        public Delegate(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
