using DabinSA.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;

namespace DabinSA.ViewModel.ViewSouce
{
    public class SubViewSouce
    {

        public SubViewSouce(){

            fre= new CollectionViewSource { Source = Meas };
            span = new CollectionViewSource { Source = Span };
            amp = new CollectionViewSource { Source = Amp };
            marker = new CollectionViewSource { Source = Marker };
            trace = new CollectionViewSource { Source = Trace };

        }

        public CollectionViewSource fre;
        public CollectionViewSource span;
        public CollectionViewSource amp;
        public CollectionViewSource marker;
        public CollectionViewSource trace;

        public ObservableCollection<SubButtonModel> Meas = new ObservableCollection<SubButtonModel>
            {
                new SubButtonModel{ButtonName="CenterFre" },
               
            };
        public ObservableCollection<SubButtonModel> Span = new ObservableCollection<SubButtonModel>
            {
                new SubButtonModel{ButtonName="Span" },
                new SubButtonModel{ButtonName="FullSpan"},

             };
        public ObservableCollection<SubButtonModel> Amp = new ObservableCollection<SubButtonModel>
            {
                new SubButtonModel{ButtonName="Attenuator" },
                new SubButtonModel{ButtonName="Offset"},

             };
        public ObservableCollection<SubButtonModel> Marker = new ObservableCollection<SubButtonModel>
            {
                new SubButtonModel{ButtonName="Select Marker" },
           

             };
        public ObservableCollection<SubButtonModel> Trace = new ObservableCollection<SubButtonModel>
            {
                new SubButtonModel{ButtonName="Select Trace" },
                new SubButtonModel{ButtonName="Trace Type"},

             };
    }
}
