using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace STTestApp.ViewModel.Commands
{
    /// <summary>
    /// Команда, запускающая эвент и имеет 
    /// запрет/разрешение на этот запуск в зависимости от canExecuteCheck
    /// </summary>
    class RelayCommand : ICommand
    {
        private Action action;
        private Func<bool> canExecuteCheck;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action action, Func<bool> canExecuteMethod)
        {
            this.action = action;
            canExecuteCheck = canExecuteMethod;

        }
        public bool CanExecute(object parameter)
        {
            if (canExecuteCheck == null)
                return true;
            return canExecuteCheck.Invoke();

        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
