using DabinSA.Model;
using DabinSA.Mqtts;
using DabinSA.ViewModel.Commands;
using DabinSA.ViewModel.ViewSouce;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Schema;
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
        private ICommand markerCommand;
        private PlotModel plotModelImp;
        private PlotModel plotModelImp2;

        private SubViewSouce subViewSouce = new SubViewSouce();
        private ChartRepository chartRepository;
        public static List<Marker> markers= new List<Marker>();

        private bool m1;
        private bool m2;
        private bool m3;
        private bool m4;
        private bool t1;

        public Mqtt mqtt;

        public MainViewModel(ChartRepository chartRepository)
        { 
            this.chartRepository= chartRepository;
            mqtt = new Mqtt(this);
            //data set
            CenterFre = "3650";
            StartFre = "3575";
            StopFre = "3725";
            Attenuator= "0";
            Offset = "0";
            Span = "150";

            PlotModelmp = new PlotModel();
            PlotModelmp2 = new PlotModel();

            for (int i=0; i < 4; i++)
            {
                Marker marker1 = new Marker() { X=  int.Parse(CenterFre),Select=1};
                Marker marker2 = new Marker() { X = int.Parse(CenterFre), Select = 2 };
                Marker marker3 = new Marker() { X = int.Parse(CenterFre),Select = 3 };
                Marker marker4 = new Marker() { X = int.Parse(CenterFre),Select = 4 };
                markers.Add(marker1);
                markers.Add(marker2);
                markers.Add(marker3);
                markers.Add(marker4);
            }

            Chart2 = Visibility.Collapsed;
            SetPlotModel();
            SetPlotModel2();

            task();
            //mqtt연결
            task2();

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

            SelectMakerUI = Visibility.Collapsed;
            MakerViewModel = new SelectMakerViewModel(this);
            MarkerName = "--";
            MarkerNum = "--";

           
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
            }); 
            //y축 생성
            PlotModelmp.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                Title = "value(dBm)",
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.White,
                IsZoomEnabled = false,
                FontSize = 15,
                Maximum = 10,
            }) ;

            PlotModelmp.PlotAreaBorderColor = OxyColors.White;
        }

        public void SetPlotModel2()
        {

            //x축 생성
            PlotModelmp2.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.White,
                IsZoomEnabled = false,
                Maximum = 1000,
                Minimum = 0,
               
            });

            //y축 생성
            PlotModelmp2.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                Title = "value(dBm)",
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.White,
                IsZoomEnabled = false,
                FontSize = 15,
                Maximum = 0,
                Minimum = -120

            });

            PlotModelmp2.PlotAreaBorderColor = OxyColors.White;
        }

        private async Task task()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(100);
                    SetMarker();
                    chartRepository.save(RangeStart,RangeStop);
                    Setchart();
                    ChageTime();
                }
            });
        }


        private async Task task2()
        {
            await mqtt.recive();
            await mqtt.recive2();
            await mqtt.recive3();
        }


        private void SetMarker()
        {
            List<ChartModel> chartModels = chartRepository.getDatas();
            PlotModelmp.Annotations.Clear();
            foreach (Marker marker in markers)
            {
                if (marker.CheckMarker == true)
                {
                    marker.Y = chartModels[marker.X].Value;
                    var annotation = new LineAnnotation();
                    annotation.Color = OxyColors.Red;
                    annotation.LineStyle = LineStyle.Solid;
                    annotation.StrokeThickness = 1;
                    annotation.X = marker.X;
                    annotation.Y = marker.Y;
                    annotation.Type = LineAnnotationType.Vertical;
                    annotation.TextColor = OxyColors.Yellow;

                    var pointanotation = new PointAnnotation();
                    pointanotation.Size = 10;
                    pointanotation.Shape = MarkerType.Diamond;
                    pointanotation.TextColor= OxyColors.Yellow;
                    pointanotation.TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle;
                    pointanotation.Text = marker.Y.ToString();
                    pointanotation.X = marker.X;
                    pointanotation.Y = marker.Y;
                    pointanotation.Fill = OxyColors.Red;

                    var pointanotation2 = new PointAnnotation();
                    pointanotation2.TextMargin = -80;
                    pointanotation2.Shape = MarkerType.None;
                    pointanotation2.Size = 50;
                    pointanotation2.TextColor = OxyColors.White;
                   // pointanotation2.TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle;
                    pointanotation2.Text = "M" + (markers.IndexOf(marker) + 1).ToString();
                    pointanotation2.X = marker.X;
                    pointanotation2.Y = marker.Y;

                    if (marker.SelectMarker)
                    {
                        MarkerName = "M"+(markers.IndexOf(marker) + 1).ToString()+" : ";
                        MarkerNum = marker.Y.ToString() ;
                    }
                    PlotModelmp.Annotations.Add(annotation);
                    PlotModelmp.Annotations.Add(pointanotation);
                    PlotModelmp.Annotations.Add(pointanotation2);

                }
            }
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
        public void Setchart2(ref List<float> recvSpectrumList)
        {

            PlotModelmp2.Series.Clear();

            var lineSeries = new LineSeries
            {
                Color = OxyColors.DeepPink,

            };


            Console.WriteLine(recvSpectrumList[0]);   
            for (int i = 0; i < recvSpectrumList.Count; i++)
            {

                lineSeries.Points.Add(new DataPoint(i, recvSpectrumList[i]));

            }


            PlotModelmp2.Series.Add(lineSeries);
            PlotModelmp2.InvalidatePlot(true);
        }

        private void ChageTime()
        {
            DateTime currentDateTime = DateTime.Now;
            Time = currentDateTime.ToString();

        }

        public ChartRepository GetChartRepository()
        {
            return chartRepository;
        }

        public PlotModel PlotModelmp { get; set; }
        public PlotModel PlotModelmp2 { get; set; }

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
        //마커and 트레이스
        public ICommand MarkerCommand
        {
            get
            {
                if (markerCommand == null)
                {
                    markerCommand = new RelayCommand<object>(param => CreateMarkerTrace(param), null);
                }
                return markerCommand;
            }
        }
        //submenuBT
        public ICommand SubButtonCommand { get;set; }

        //SG
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

        public bool M1
        {
            get => m1;
            set
            {
                m1 = value;
                OpPropertyChanged("M1");
            }
        }
        public bool M2
        {
            get => m2;
            set
            {
                m2 = value;
                OpPropertyChanged("M2");
            }
        }
        public bool M3
        {
            get => m3;
            set
            {
                m3 = value;
                OpPropertyChanged("M3");
            }
        }

        public bool M4
        {
            get => m4;
            set
            {
                m4 = value;
                OpPropertyChanged("M4");
            }
        }
        public bool T1
        {
            get => t1;
            set
            {
                t1 = value;
                OpPropertyChanged("T1");
            }
        }

        private int total;
        private int markFlag1;
        private int markFlag2;
        private int markFlag3;
        private int markFlag4;
        public void CreateMarkerTrace(object parameter)
        {
            List<ChartModel> chartModels = chartRepository.getDatas();

            if (parameter.ToString() == "M1")
            {
                if (M1 == true)
                {
                    markers[0].CheckMarker = true;
                    markers[0].Select = 1;
                    markers[0].X = int.Parse(CenterFre);
                    markers[0].Y = chartModels[markers[0].X].Value;

                }
                else if (M1 == false)
                {
                    markers[0].CheckMarker = false;
                    markers[0].X = 0;
                    MarkerName = "--";
                    MarkerNum = "--";

                }

            }
            if (parameter.ToString() == "M2")
            {
                if (M2 == true)
                {
                    markers[1].CheckMarker = true;
                    markers[1].Select = 2;
                    markers[1].X = int.Parse(CenterFre);
                    markers[1].Y = chartModels[markers[0].X].Value;


                }
                else if (M2 == false)
                {

                    markers[1].CheckMarker = false;
                    markers[1].X = 0;
                    MarkerName = "--";
                    MarkerNum = "--";

                }
            }
            if (parameter.ToString() == "M3")
            {
                if (M3 == true)
                {
                    markers[2].CheckMarker = true;
                    markers[2].Select = 3;
                    markers[2].X = int.Parse(CenterFre);
                    markers[2].Y = chartModels[markers[0].X].Value;


                }
                else if (M3 == false)
                {

                    markers[2].CheckMarker = false;
                    markers[2].X = 0;
                    MarkerName = "--";
                    MarkerNum = "--";

                }
            }
            if (parameter.ToString() == "M4")
            {
                if (M4 == true)
                {
                    markers[3].CheckMarker = true;
                    markers[3].Select = 4;
                    markers[3].X = int.Parse(CenterFre);
                    markers[3].Y = chartModels[markers[0].X].Value;

                }
                else if (M4 == false)
                {

                    markers[3].CheckMarker = false;
                    markers[3].X = 0;
                    MarkerName = "--";
                    MarkerNum = "--";

                }
            }
            if (parameter.ToString() == "T1")
            {

                if (T1 == true)
                {
                    mqtt.RequestSpectrum();
                    Chart1 = Visibility.Collapsed; Chart2 = Visibility.Visible;
                }
                else if (T1 == false)
                {
                    Chart1 = Visibility.Visible; Chart2 = Visibility.Collapsed;
                }
            }

           
            // Create a PointAnnotation for a specific point
            //var annotation2 = new PointAnnotation
            //{
            //    X = int.Parse(centerFre), // X-coordinate of the annotation
            //    Y = -20, // Y-coordinate of the annotation
            //    Text = "1" // Annotation text
            //};

            //annotation2.Shape = MarkerType.Star;
            //annotation2.TextColor = OxyColors.Yellow;
            //// Add the annotation to the PlotModel's Annotations collection


            //PlotModelmp.Annotations.Add(annotation2);

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
        private Visibility stack { get; set; }
        public Visibility Stack
        {
            get { return stack; }
            set
            {
                if (stack != value)
                {
                    stack = value;
                    OpPropertyChanged("Stack");
                }
            }
        }
        private SelectMakerViewModel makerViewModel;
        public SelectMakerViewModel MakerViewModel {
            get { return makerViewModel; }
            set
            {
                makerViewModel = value;
                OpPropertyChanged("MakerViewModel");
            }
        }
        private string markerName;
        public string MarkerName
        {
            get { return markerName; }
            set
            {
                markerName = value;
                OpPropertyChanged("MarkerName");
            }
        }
        private string markerNum;

        public string MarkerNum
        {
            get { return markerNum; }
            set
            {
                markerNum = value;
                OpPropertyChanged("MarkerNum");
            }
        }
        private Visibility selectMakerUI { get; set; }
        public Visibility SelectMakerUI
        {
            get { return selectMakerUI; }
            set
            {
                if (selectMakerUI != value)
                {
                    selectMakerUI = value;
                    OpPropertyChanged("SelectMakerUI");
                }
            }
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

        private void show()
        {

            SelectMakerUI = System.Windows.Visibility.Collapsed;
            Stack = System.Windows.Visibility.Visible;
        }

        private Visibility chart1;

        public Visibility Chart1
        {
            get { return chart1; }
            set
            {
                if (chart1 != value)
                {
                    chart1 = value;
                    OpPropertyChanged("Chart1");
                }
            }
        }

        private Visibility chart2;

        public Visibility Chart2
        {
            get { return chart2; }
            set
            {
                if (chart2 != value)
                {
                    chart2 = value;
                    OpPropertyChanged("Chart2");
                }
            }
        }
    }
}
