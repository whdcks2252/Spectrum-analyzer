using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabinSA.Model
{
    public class ChartRepository
    {
        private List<ChartModel> ChartDataRepository = new List<ChartModel>();
        private Random random = new Random();

        public ChartRepository() {

            for (double i = 0; i < 2002; i++)
            {
                ChartModel chart = new ChartModel() { Frequency = i };
                ChartDataRepository.Add(chart);
            }
        }


        public List<ChartModel> getDatas()
        {
            return ChartDataRepository;
        }

        public void save(int start,int stop)
        {

            for (double i = 0; i < ChartDataRepository.Count; i++)
            {
                ChartDataRepository[(int)i].Value = random.Next(start, stop);

            }
        }




    }
}
