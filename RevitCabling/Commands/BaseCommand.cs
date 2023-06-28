using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitCabling.Commands
{
    internal abstract class BaseCommand : ICommand
    {
        Func<object, bool> _canExecute;

        public BaseCommand(Func<object, bool> canExecute = null)
        {
            this._canExecute = canExecute;
        }

        public virtual bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
