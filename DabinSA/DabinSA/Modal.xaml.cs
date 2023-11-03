using DabinSA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Modal.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Modal : UserControl
    {

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Modal), new PropertyMetadata(false, IsOpenChanged));

        private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Modal modal)
            {
                bool isOpen = (bool)e.NewValue;
                if (isOpen)
                {
                    modal.BringToFront();
                    modal.Visibility = Visibility.Visible;

                }
                else
                {
                    modal.Visibility = Visibility.Collapsed;
                }
            }



        }

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(Modal), new PropertyMetadata(Brushes.White));



        public SolidColorBrush ShadowColor
        {
            get { return (SolidColorBrush)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(SolidColorBrush), typeof(Modal), new PropertyMetadata(Brushes.Black));




        public Modal()
        {
            InitializeComponent();
            Visibility = Visibility.Collapsed;
        }

        private void opacityGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsOpen = false;
        }
    
}
}
