using System.Collections.Generic;
using CodeDistribution.Pad.PieChart;
using CodeDistribution.Pad.Results;

namespace CodeDistribution.Pad.FileDistribution
{
    public class FileDistributionResultsPieChart : BaseResultsPieChart
    {
        protected override List<PieDataPoint> DataForResult(SolutionAnalysis r)
        {
            var pieData = new List<PieDataPoint>();
            foreach (var extension in r.Extensions.Values)
                pieData.Add(new PieDataPoint(extension.Extension, (int)extension.PercentageOfSolution));

            return pieData;
        }
    }
}
