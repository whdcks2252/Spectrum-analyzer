using DabinSA.ViewModel;
using Microsoft.Xaml.Behaviors;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DabinSA
{
    public class OxyPlotBehavior : Behavior<PlotView>
    {
        
        protected override void OnAttached()
        {
            //AssoicatedObject의 이벤트 핸들러 추가 - 삭제는 반드시 쌍으로 처리합니다.
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }


        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (MainViewModel.markers[0].SelectMarker)
            {
            var plot = sender as PlotView;
            if (plot == null) return;
            var xAxis = plot.ActualModel.Axes[0];
            var yAxis = plot.ActualModel.Axes[1];

            var dataPoint = new ScreenPoint((int)e.GetPosition(null).X, (int)e.GetPosition(null).Y);
            double x = xAxis.InverseTransform(dataPoint.X);
            double y = yAxis.InverseTransform(dataPoint.Y);

            var mousePoint = e.GetPosition(null);
            MainViewModel.markers[0].X = (int)x;
            MainViewModel.markers[0].Y = (int)y;

            }
            if (MainViewModel.markers[1].SelectMarker)
            {
                var plot = sender as PlotView;
                if (plot == null) return;
                var xAxis = plot.ActualModel.Axes[0];
                var yAxis = plot.ActualModel.Axes[1];

                var dataPoint = new ScreenPoint((int)e.GetPosition(null).X, (int)e.GetPosition(null).Y);
                double x = xAxis.InverseTransform(dataPoint.X);
                double y = yAxis.InverseTransform(dataPoint.Y);

                var mousePoint = e.GetPosition(null);
                MainViewModel.markers[1].X = (int)x;
                MainViewModel.markers[1].Y = (int)y;

            }
            if (MainViewModel.markers[2].SelectMarker)
            {
                var plot = sender as PlotView;
                if (plot == null) return;
                var xAxis = plot.ActualModel.Axes[0];
                var yAxis = plot.ActualModel.Axes[1];

                var dataPoint = new ScreenPoint((int)e.GetPosition(null).X, (int)e.GetPosition(null).Y);
                double x = xAxis.InverseTransform(dataPoint.X);
                double y = yAxis.InverseTransform(dataPoint.Y);

                var mousePoint = e.GetPosition(null);
                MainViewModel.markers[2].X = (int)x;
                MainViewModel.markers[2].Y = (int)y;

            }
            if (MainViewModel.markers[3].SelectMarker)
            {
                var plot = sender as PlotView;
                if (plot == null) return;
                var xAxis = plot.ActualModel.Axes[0];
                var yAxis = plot.ActualModel.Axes[1];

                var dataPoint = new ScreenPoint((int)e.GetPosition(null).X, (int)e.GetPosition(null).Y);
                double x = xAxis.InverseTransform(dataPoint.X);
                double y = yAxis.InverseTransform(dataPoint.Y);

                var mousePoint = e.GetPosition(null);
                MainViewModel.markers[3].X = (int)x;
                MainViewModel.markers[3].Y = (int)y;


            }

        }

     

        public ICommand OxyCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(OxyCommand), typeof(ICommand), typeof(OxyPlotBehavior), new PropertyMetadata(null));
    }


}

