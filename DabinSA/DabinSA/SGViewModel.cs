using DabinSA.ViewModel;
using DabinSA.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace DabinSA
{
    public class SGViewModel:ViewModelBase

    {
        private MainViewModel mainViewModel;
        public SGViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel; 
        }



        private string freTxt;
        private string ampTxt;
        private ICommand generateBT;

        public string FreTxt {
            get { return freTxt; }
            set
            {
                freTxt = value;
                OpPropertyChanged("FreTxt");
            }
        }

        public string AmpTxt {
            get { return ampTxt; }
            set
            {
                ampTxt = value;
                OpPropertyChanged("AmpTxt");
            }
        }

       public ICommand GenerateBT
        {
            get
            {
                if (generateBT == null)
                {
                    generateBT = new RelayCommand<object>(param => Generate(), null);
                }
                return generateBT;
            }
        }

        public List<string> ModeList { get; set; } = new List<string>()
        {
            "SG",
            "LTE",
            "5G"

        };

        public string SelectCombo { get; set; }

        private void Generate()
        {

            if (SelectCombo == "SG")
            {
                MessageBox.Show( SelectCombo);

            }
            if (SelectCombo=="LTE")
            {
                MessageBox.Show( SelectCombo);

            }
            if (SelectCombo == "5G")
            {
                MessageBox.Show( SelectCombo);

            }


            MessageBox.Show(FreTxt + "Test"+ AmpTxt+ SelectCombo);


        }



    }
}
