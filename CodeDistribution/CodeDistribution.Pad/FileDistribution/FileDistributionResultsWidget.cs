using CodeDistribution.Pad.Results;

namespace CodeDistribution.Pad.FileDistribution
{
    public class FileDistributionResultsWidget : ResultsWidget
    {
        protected override BaseResultsNodeView ResultList { get; } = new FileDistributionResultsNodeView();

        protected override BaseResultsPieChart Pie { get; } = new FileDistributionResultsPieChart();
    }
}
