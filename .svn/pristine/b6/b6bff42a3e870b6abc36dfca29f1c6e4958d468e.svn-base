using System;
using System.Windows.Input;

namespace StudentsList.Commands
{
    public class ButtonCommand : ICommand
    {
        private Action WhattoExecute;
        private Func<bool> WhentoExecute;

        public ButtonCommand(Action What, Func<bool> When)
        {
            WhattoExecute = What;
            WhentoExecute = When;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return WhentoExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        } 

        public void Execute(object parameter)
        {
            WhattoExecute();
        }
        #endregion
    }
}
