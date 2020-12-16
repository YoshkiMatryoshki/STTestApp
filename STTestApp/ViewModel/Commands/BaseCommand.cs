using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace STTestApp.ViewModel.Commands
{
    /// <summary>
    /// Стандартная комманда, запускающая Action
    /// закинутый в конструктор (всгеда может выполняться)
    /// </summary>
    class BaseCommand : ICommand
    {
        private Action action;
        public event EventHandler CanExecuteChanged = (sender,e)=> { };
        //Всегда активна
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
        /// <summary>
        /// конструктор получает экшон
        /// </summary>
        public BaseCommand(Action action)
        {
            this.action = action;
        }
    }
}
