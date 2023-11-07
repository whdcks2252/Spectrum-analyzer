using DabinSA.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DabinSA
{
    /// <summary>
    /// SelectMarker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectMarker : UserControl
    {

        private MainViewModel mainViewModel;
        public SelectMarker()
        {
            InitializeComponent();
            
            mainViewModel= App.Current.Services.GetRequiredService<MainViewModel>();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            MainViewModel.markers[0].SelectMarker = true;
            MainViewModel.markers[1].SelectMarker = false;
            MainViewModel.markers[2].SelectMarker = false;
            MainViewModel.markers[3].SelectMarker = false;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainViewModel.markers[0].SelectMarker = false;
            MainViewModel.markers[1].SelectMarker = true;
            MainViewModel.markers[2].SelectMarker = false;
            MainViewModel.markers[3].SelectMarker = false;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            MainViewModel.markers[0].SelectMarker = false;
            MainViewModel.markers[1].SelectMarker = false;
            MainViewModel.markers[2].SelectMarker = true;
            MainViewModel.markers[3].SelectMarker = false;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            MainViewModel.markers[0].SelectMarker = false;
            MainViewModel.markers[1].SelectMarker = false;
            MainViewModel.markers[2].SelectMarker = false;
            MainViewModel.markers[3].SelectMarker = true;
        }
    }
}
