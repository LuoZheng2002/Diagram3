using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diagram3
{
    internal class Command : ICommand
    {
        private WrappedEvent _event;
        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _event?.Invoke(parameter);
        }
        public Command(WrappedEvent _event)
        {
            this._event = _event;
        }
    }
}
