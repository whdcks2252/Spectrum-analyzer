using DabinSA.Model;
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
            SelectCombo = ModeList[0];
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

        private bool sGFreTrue;
        public bool SGFreTrue {
            get { return sGFreTrue; }
            set
            {
                sGFreTrue = value;
                OpPropertyChanged("SGFreTrue");
            }
        }

        private bool sGAmpTrue;

        public bool SGAmpTrue {
            get { return sGAmpTrue; }
            set
            {
                sGAmpTrue = value;
                OpPropertyChanged("SGAmpTrue");
            }
        }

        public List<string> ModeList { get; set; } = new List<string>()
        {
            "SG",
            "LTE",
            "5G"

        };

        private string selectCombo;
        public string SelectCombo {
            get { return selectCombo; }
            set
            {
                selectCombo = value;
                if (value == "SG")
                {
                    SGFreTrue = true;
                    SGAmpTrue = true;

                }
                else
                {
                    SGFreTrue = false;
                    SGAmpTrue = false;

                }
                OpPropertyChanged("SelectCombo");
            }
        } 

        private void Generate()
        {

            if (SelectCombo == "SG")
            {
                try
                {
                    if(int.Parse(FreTxt)>5998|| int.Parse(FreTxt)<1|| int.Parse(AmpTxt)>-10)
                    {
                        FreTxt = "";
                        AmpTxt = "";
                        throw new Exception();
                    }
                    ChartModel chartModel = new ChartModel
                    {
                        Frequency = double.Parse(FreTxt),
                        Value = int.Parse(AmpTxt)
                    };
                    mainViewModel.GetChartRepository().Mode = "SG";
                    mainViewModel.GetChartRepository().GeneratedSignal(chartModel);
                    mainViewModel.SetInfoSGmode(SelectCombo);
                    mainViewModel.SetInfoSGFrequency(FreTxt);
                    mainViewModel.SetInfSGAmp(AmpTxt);

                    FreTxt = "";
                    AmpTxt = "";
                    mainViewModel.IsSGModalOpen = false;
                }
                catch (Exception ex) { MessageBox.Show("범위 1~5998(MHz)"); }
            }
            if (SelectCombo=="LTE")
            {

                mainViewModel.GetChartRepository().Mode = "LTE";
                mainViewModel.GetChartRepository().GeneratedSignal(null);
                mainViewModel.SetInfoSGmode(SelectCombo);
                mainViewModel.SetInfoSGFrequency("--");
                mainViewModel.SetInfSGAmp("--");
                FreTxt = "";
                AmpTxt = "";
                mainViewModel.IsSGModalOpen = false;


            }
            if (SelectCombo == "5G")
            {
                mainViewModel.GetChartRepository().Mode = "5G";
                mainViewModel.GetChartRepository().GeneratedSignal(null);
                mainViewModel.SetInfoSGmode(SelectCombo);
                mainViewModel.SetInfoSGFrequency("--");
                mainViewModel.SetInfSGAmp("--");
                FreTxt = "";
                AmpTxt = "";
                mainViewModel.IsSGModalOpen = false;


            }


        }



    }
}
