using DabinSA.ViewModel;
using DabinSA.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DabinSA
{
    public class SelectMakerViewModel
    {
        private MainViewModel mainViewModel;
        public SelectMakerViewModel(MainViewModel mainViewModel) {
        
        this.mainViewModel = mainViewModel;
        }

        private ICommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new RelayCommand<object>(param => show(), null);
                }
                return command;
            }
        }

        private void show() {

            mainViewModel.SelectMakerUI = System.Windows.Visibility.Collapsed;
            mainViewModel.Stack = System.Windows.Visibility.Visible;
        }
    }
}
