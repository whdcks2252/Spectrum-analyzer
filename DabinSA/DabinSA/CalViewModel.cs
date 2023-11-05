using DabinSA.ViewModel;
using DabinSA.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DabinSA
{
    public  class CalViewModel:ViewModelBase
    {
        private MainViewModel mainViewModel;
        public CalViewModel(  MainViewModel mainViewModel) {
        
        this.mainViewModel = mainViewModel;

            Txt = "";

        }
        private ICommand commandBT;
        

          public  ICommand CommandBT {
            get
            {
                if (commandBT == null)
                {
                    commandBT = new RelayCommand<object>(param => button(), null);
                    
                }
                return commandBT;
            }
        }

        private string txt;
        public string Txt
        {

            get { return txt; }
            set
            {
                txt = value;
                OpPropertyChanged("Txt");
            }
        }

        ICommand calBT;
        public ICommand CalBT
        {
            get
            {
                if (calBT == null)
                {
                    calBT = new RelayCommand<object>(param => button2(param), null);

                }
                return calBT;
            }
        }
        private void button2(object param)
        {
           
            if (param.ToString() == "Cancel")
            {
                if (Txt.Length < 1)
                {
                    return;
                }
                    int lengthToKeep = Txt.Length - 1; // 맨 뒤 문자 하나를 제거하려면 현재 길이에서 1을 뺍니다.
                    Txt = Txt.Substring(0, lengthToKeep);
            }
            else if(Convert.ToInt32(param) > -1&& Convert.ToInt32(param) < 10)
                Txt += param.ToString();


        }

        private void button()
        {

            if (mainViewModel.CurrentBT == "CenterFre")
            {
                if (Txt.Length < 1)
                {
                    MessageBox.Show("값을 입력 해 주세요");
                    return;
                }
                int tx = int.Parse(Txt);

                mainViewModel.IsCalculatorModalOpen = false;
                mainViewModel.CenterFre = tx.ToString();
                mainViewModel.SetInfoCenterFre(tx.ToString());
                calFre(tx);

            }
            if (mainViewModel.CurrentBT == "Span")
            {
                if (Txt.Length < 1)
                {
                    MessageBox.Show("값을 입력 해 주세요");
                    return;
                }
                int tx = int.Parse(Txt);

                mainViewModel.IsCalculatorModalOpen = false;
                mainViewModel.Span = tx.ToString();
                mainViewModel.SetInfoSpan(tx.ToString());
                calSapn(tx);
            }

            if(mainViewModel.CurrentBT=="Attenuator")
            {
                if (Txt.Length < 1)
                {
                    MessageBox.Show("값을 입력 해 주세요");
                    return;
                }
                int amp = int.Parse(Txt);

                int offset = int.Parse(mainViewModel.Offset);
                mainViewModel.IsCalculatorModalOpen = false;
                mainViewModel.Attenuator = amp.ToString();
                mainViewModel.SetInfoAmp(amp.ToString());
                mainViewModel.RangeStart = -100+ amp + offset;
                mainViewModel.RangeStop = -80+ amp + offset;
                
            }

            if(mainViewModel.CurrentBT == "Offset")
            {
                if (Txt.Length < 1)
                {
                    MessageBox.Show("값을 입력 해 주세요");
                    return;
                }

                int offset = int.Parse(Txt);
               int amp = int.Parse(mainViewModel.Attenuator);

                mainViewModel.IsCalculatorModalOpen = false;
                mainViewModel.RangeStart = -100 + amp + offset;
                mainViewModel.RangeStop = -80 + amp + offset;
                mainViewModel.Offset = offset.ToString();
                mainViewModel.SetInfoOffset(offset.ToString());



            }


            Txt = "";
        }


        private void calFre(int tx)
        {
            int span = int.Parse(mainViewModel.Span);
            mainViewModel.StartFre = (tx - (span / 2)).ToString();
            mainViewModel.StopFre = (tx + (span / 2)).ToString();
            
        }

        private void calSapn(int tx)
        {
            int span = int.Parse(mainViewModel.Span);
            int centerFre= Convert.ToInt32(mainViewModel.CenterFre);
            mainViewModel.StartFre = (centerFre - (span / 2)).ToString();
            mainViewModel.StopFre = (centerFre + (span / 2)).ToString();


        }
    }
}
