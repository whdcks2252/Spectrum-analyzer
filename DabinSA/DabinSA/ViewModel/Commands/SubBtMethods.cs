using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabinSA.ViewModel.Commands
{
   public  class SubBtMethods
    {
        private MainViewModel mainViewModel;
        public SubBtMethods(MainViewModel mainViewModel) { 
        
            this.mainViewModel = mainViewModel; 

        }

        public void CenterFre() { 
        
        mainViewModel.IsCalculatorModalOpen = true;

        
        }

    }
}
