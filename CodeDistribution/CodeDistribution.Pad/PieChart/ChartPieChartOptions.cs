using System;
using System.Collections.Generic;

namespace CodeDistribution.Pad.PieChart
{
    public class PieDataPoint
    {
        public string Label { get; set; }
        public int Value { get; set; }

        public PieDataPoint(string label, int value)
        {
            Label = label;
            Value = value;
        }
    }

    public class Dataset
    {
        public IList<int> data { get; set; }
        public IList<string> backgroundColor { get; set; }
        public string label { get; set; }
    }

    public class Data
    {
        public IList<Dataset> datasets { get; set; }
        public IList<string> labels { get; set; }
    }

    public class ChartData
    {
        static Random random = new Random();

        internal static Data MakeData(List<PieDataPoint> points)
        {
            var data = new Data()
            {
                datasets = new List<Dataset>(new Dataset[] {
                            new Dataset {
                                data = new List<int>(),
                                backgroundColor = new List<string>(),
                                label = "Dataset 1"
                            }
                        }),
                labels = new List<string>()
            };

            foreach (var point in points)
            {
                data.labels.Add(point.Label);
                data.datasets[0].backgroundColor.Add(RandomColor());
                data.datasets[0].data.Add(point.Value);
            }

            return data;
        }

        static string RandomColor()
        {
            int[] values = new int[3];
            values[random.Next(0, 2)] = random.Next(150, 255);
            for (int i = 0; i < values.Length; i++)
                if (values[i] == 0)
                    values[i] = random.Next(70, 200);

            return string.Format("rgb({0}, {1}, {2})",
                                 values[0],
                                 values[1],
                                 values[2]);
        }
    }
}
