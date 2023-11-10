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
        private ChartModel generatedSignal = new ChartModel();

        public ChartRepository() {

            for (int i = 0; i < 6000; i++)
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
            
            for (int i = 0; i < ChartDataRepository.Count; i++)
            {
                if (Mode == "LTE"&&i==3650)
                {
                    for(int j = 0; j < 20; j++)
                    {
                        ChartDataRepository[i-10 + j].Value = random.Next(-23, -18);

                    }
                    i += 10;
                    continue;
                }

               if (Mode == "5G"&& i == 3650)
                {
                    for (int j = 0; j < 100; j++)
                    {

                        ChartDataRepository[(i-50)+j].Value = random.Next(-23, -18);

                    }
                    i += 49;
                    continue;
                }

                if (generatedSignal != null) {
                     if (i == generatedSignal.Frequency && i > 0)
                        {
                            ChartDataRepository[i].Value = generatedSignal.Value;
                            ChartDataRepository[i - 1].Value = generatedSignal.Value;
                            ChartDataRepository[i + 1].Value = generatedSignal.Value;
                            i++;
                            continue;
                        }
                }

                ChartDataRepository[(int)i].Value = random.Next(start, stop);

            }
        }


        public void GeneratedSignal(ChartModel generatedSignal)
        {
            this.generatedSignal = generatedSignal;
        }

        public string Mode { get; set; }
    }
}
