using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DabinSA.ViewModel.Commands
{
    public class SubMenuBTCommand : CommandBase
    {
        private MainViewModel mainViewModel;
        private SubBtMethods subBtMethods;
        public SubMenuBTCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

            subBtMethods = new SubBtMethods(mainViewModel);
        }

        private void Method(string param)
        {
            if (param == "CenterFre")
            {
                mainViewModel.CurrentBT = param;
                subBtMethods.CenterFre();

            }
            if (param == "Span")
            {
                mainViewModel.CurrentBT = param;
                subBtMethods.CenterFre();
            }
            if (param == "FullSpan")
            {

            }
            if (param == "Attenuator")
            {
                mainViewModel.CurrentBT = param;
                subBtMethods.CenterFre();
            }
            if (param == "Offset")
            {
                mainViewModel.CurrentBT = param;
                subBtMethods.CenterFre();
            }
            if (param == "Select Marker")
            {

            }
            if (param == "Select Trace")
            {

            }
            if (param == "Trace Type")
            {

            }


        }



        public override bool CanExecute(object parameter)
        {

            return true;
        }

        public override void Execute(object parameter)
        {
            Method(parameter.ToString());

        }

    }
}
