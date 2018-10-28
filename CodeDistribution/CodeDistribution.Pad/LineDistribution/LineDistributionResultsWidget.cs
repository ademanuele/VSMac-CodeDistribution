using CodeDistribution.Pad.Results;
using CodeDistribution.Pad.LineDistribution;

namespace CodeDistribution.Pad
{
    public class LineDistributionResultsWidget : ResultsWidget
    {
        protected override BaseResultsNodeView ResultList { get; } = new LineDistributionResultsNodeView();

        protected override BaseResultsPieChart Pie { get; } = new LineDistributionResultsPieChart();
    }
}
