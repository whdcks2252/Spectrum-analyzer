using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabinSA.ViewModel.Commands
{
    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T> _execute;
        private Func<object, bool> canExecute;


        public RelayCommand(Action<T> execute, Func<object, bool> canExecute)
        {

            this._execute = execute;
            this.canExecute = canExecute;

        }


        public override bool CanExecute(object parameter)
        {
            return canExecute?.Invoke((T)parameter) ?? true;

        }

        public override void Execute(object parameter)
        {
            _execute?.Invoke((T)parameter);

        }
    }
}
