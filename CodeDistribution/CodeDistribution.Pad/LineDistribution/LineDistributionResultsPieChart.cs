using System.Collections.Generic;
using CodeDistribution.Pad.Results;

namespace CodeDistribution.Pad.LineDistribution
{
    public class LineDistributionResultsPieChart : BaseResultsPieChart
    {
        protected override List<PieChart.PieDataPoint> DataForResult(SolutionAnalysis r)
        {
            var pieData = new List<PieChart.PieDataPoint>();
            foreach (var project in r.Projects)
                pieData.Add(new PieChart.PieDataPoint(project.Project.Name, (int)project.PercentageOfTotal));

            return pieData;
        }
    }
}
