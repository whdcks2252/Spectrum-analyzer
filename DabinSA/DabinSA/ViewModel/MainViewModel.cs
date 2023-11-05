using DabinSA.Model;
using DabinSA.ViewModel.Commands;
using DabinSA.ViewModel.ViewSouce;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace DabinSA.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private ICollectionView sourceCollection;
        private string time;
        private string subMenuTxt;
        
        private string centerFre;
        private string startFre;
        private string stopFre;
        private string attenuator;
        private string offset;
        private string span;

        private string centerFreIn;
        private string ampIn;
        private string offsetIn;
        private string sGmode;
        private string sGFrequency;
        private string sGAmp;
        private string spanIn;

        //modal
        private bool isCalculatorModalOpen;
        private bool isSGModalOpen;


        private ICommand sgBT;
        private ICommand chageSASubMenu;
        private PlotModel plotModelImp;
        private SubViewSouce subViewSouce = new SubViewSouce();
        private ChartRepository chartRepository;

      

        public MainViewModel(ChartRepository chartRepository)
        { this.chartRepository= chartRepository;
           
            //data set
            CenterFre = "3650";
            StartFre = "3575";
            StopFre = "3725";
            Attenuator= "0";
            Offset = "0";
            Span = "150";

            PlotModelmp = new PlotModel();
            SetPlotModel();            
            task();

            CalViewModel = new CalViewModel(this);
            SGViewModel = new SGViewModel(this);

            SourceCollection = subViewSouce.fre.View;
            SubMenuTxt = "Frequency";

            //Inforset
            SetInfoCenterFre(CenterFre);
            SetInfoAmp(Attenuator);
            SetInfoOffset(Offset);
            SetInfoSpan(Span);
            SetInfoSGmode("--");
            SetInfoSGFrequency("--");
            SetInfSGAmp("--");

            SubButtonCommand = new SubMenuBTCommand(this);

            this.chartRepository = chartRepository;
        }

        public void SetPlotModel()
        {
            
            //x축 생성
            PlotModelmp.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.White,
                IsZoomEnabled = false,
            }) ;
            //y축 생성
            PlotModelmp.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                Title = "value(dBm)",
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.White,
                IsZoomEnabled = false,
                Maximum = 10,

            }) ;

            PlotModelmp.PlotAreaBorderColor = OxyColors.White;
        }
        private async Task task()
        {

            await Task.Run(async () =>
            {

                while (true)
                {
                    
                    await Task.Delay(100);
                    chartRepository.save(RangeStart,RangeStop);
                    Setchart();
                    ChageTime();
                }

            });
        }
        private void Setchart()
        {

            PlotModelmp.Series.Clear();
           
            var lineSeries = new LineSeries
            {
                Color = OxyColors.Yellow,

            };

           

            var data = chartRepository.getDatas();
            int start = Convert.ToInt32(StartFre);
            int stop = Convert.ToInt32(StopFre);


            for (int i = start; i < stop + 1; i++)
                {

                    lineSeries.Points.Add(new DataPoint(data[i].Frequency, data[i].Value));

                }
            
   
            //foreach (var data in chartRepository.getDatas())
            //     lineSeries.Points.Add(new DataPoint(data.Frequency, data.Value));

            PlotModelmp.Series.Add(lineSeries);
            PlotModelmp.InvalidatePlot(true);// 바인딩 즉시업데이트 트리거
        }

        private void ChageTime()
        {
            DateTime currentDateTime = DateTime.Now;
            Time = currentDateTime.ToString();

        }

        public PlotModel PlotModelmp { get; set; }
        public CalViewModel CalViewModel { get; set; }
        public SGViewModel SGViewModel { get; set; }

        public string CurrentBT { get; set; }

        //modal
        public bool IsSGModalOpen
        {
            get => isSGModalOpen;
            set
            {
                isSGModalOpen = value;
                OpPropertyChanged("IsSGModalOpen");

            }
        }
        public bool IsCalculatorModalOpen
        {
            get => isCalculatorModalOpen;
            set
            {
                isCalculatorModalOpen = value;
                OpPropertyChanged("IsCalculatorModalOpen");

            }
        }

        public ICommand SubButtonCommand { get;set; }

        public ICommand SgBT
        {
            get
            {
                if (sgBT == null)
                {
                    sgBT = new RelayCommand<object>(param => ShowSGMode(), null);
                }
                return sgBT;
            }
        }

        public ICommand ChageSASubMenu
        {
            get
            {
                if (chageSASubMenu == null)
                {
                    chageSASubMenu = new RelayCommand<object>(param => SwitchViews(param), null);
                }
                return chageSASubMenu;
            }
        }
        //속성값들 
        public string SubMenuTxt
        {
            get { return subMenuTxt; }
            set
            {
                subMenuTxt = value;
                OpPropertyChanged("SubMenuTxt");
            }
        }

        public string CenterFre
        {
            get { return centerFre; }
            set
            {
                centerFre = value;
                OpPropertyChanged("CenterFre");
            }
        }

        public string StartFre
        {
            get { return startFre; }
            set
            {
                startFre = value;
                OpPropertyChanged("StartFre");
            }
        }

        public string StopFre
        {
            get { return stopFre; }
            set
            {
                stopFre = value;
                OpPropertyChanged("StopFre");
            }
        }

        public string Attenuator
        {
            get { return attenuator; }
            set
            {
                attenuator = value;
                OpPropertyChanged("Attenuator");
            }
        }

        public string Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                OpPropertyChanged("Offset");
            }
        }

        public string Span
        {
            get { return span; }
            set
            {
                span = value;
                OpPropertyChanged("Span");
            }
        }
        //차트 변경값
        public int RangeStart { get; set; } = -100;
        public int RangeStop { get; set; } = -80;




        //submenu
        public string CenterFreIn
        {
            get { return centerFreIn; }
            set
            {
                centerFreIn = value;
                OpPropertyChanged("CenterFreIn");
            }
        }

        public string AmpIn
        {
            get { return ampIn; }
            set
            {
                ampIn = value;
                OpPropertyChanged("AmpIn");
            }
        }

        public string OffsetIn
        {
            get { return offsetIn; }
            set
            {
                offsetIn = value;
                OpPropertyChanged("OffsetIn");
            }
        }
        public string SpanIn
        {
            get { return spanIn; }
            set
            {
                spanIn = value;
                OpPropertyChanged("SpanIn");
            }
        }

        public string SGmode
        {
            get { return sGmode; }
            set
            {
                sGmode = value;
                OpPropertyChanged("SGmode");
            }
        }

        public string SGFrequency
        {
            get { return sGFrequency; }
            set
            {
                sGFrequency = value;
                OpPropertyChanged("SGFrequency");
            }
        }

        public string SGAmp
        {
            get { return sGAmp; }
            set
            {
                sGAmp = value;
                OpPropertyChanged("SGAmp");
            }
        }


        //서브메뉴 체인지

        public ICollectionView SourceCollection
        {
            get { return sourceCollection; }
            set
            {
                sourceCollection = value;
                OpPropertyChanged("SourceCollection");
            }
        }

        public string Time
        {
            get => time;
            set
            {
                time = value;
                OpPropertyChanged("Time");
            }
        }

        public void ShowSGMode()
        {
            IsSGModalOpen = true;
        }

        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Frequency":
                    SourceCollection = subViewSouce.fre.View;
                    SubMenuTxt = parameter.ToString();
                    break;
                case "Span":
                    SourceCollection = subViewSouce.span.View;
                    SubMenuTxt = parameter.ToString();
                    break;
                case "Amplitude":
                    SourceCollection = subViewSouce.amp.View;
                    SubMenuTxt = parameter.ToString();
                    break;
                case "Marker":
                    SourceCollection = subViewSouce.marker.View;
                    SubMenuTxt = parameter.ToString();
                    break;
                case "Trace":
                    SourceCollection = subViewSouce.trace.View;
                    SubMenuTxt = parameter.ToString();
                    break;

            }

        }



        public void SetInfoCenterFre(string st)
        {
            CenterFreIn = "Center : " + st + " (MHz)";

        }
        public void SetInfoAmp(string st)
        {
            AmpIn = "Attenuator : " + st + " (dB)";

        }
        public void SetInfoOffset(string st)
        {
            OffsetIn = "Offset : " + st + " (dB)";

        }
        public void SetInfoSpan(string st)
        {
            SpanIn = "Span : " + st + " (MHz)";

        }
        public void SetInfoSGmode(string st)
        {
            SGmode = "SGmode : " + st ;

        }
        public void SetInfoSGFrequency(string st)
        {
            SGFrequency = "SGFrequency : " + st + " (MHz)";

        }
        public void SetInfSGAmp(string st)
        {
            SGAmp = "SGAttenuator : " + st + " (dB)";

        }
      

    }
}
