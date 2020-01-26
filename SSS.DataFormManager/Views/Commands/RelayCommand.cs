using System;
using System.Windows.Input;

namespace SSS.DataFormManager.Views.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> executeMethod;
        private Func<object, bool> canExecuteMethod;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteMethod != null)
            {
                return canExecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}