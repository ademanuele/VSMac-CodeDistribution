using System;
using CodeDistribution.Pad.Results;
using Gtk;

namespace CodeDistribution.Pad
{
    [System.ComponentModel.ToolboxItem(true)]
    class LineDistributionResultsNodeView : BaseResultsNodeView<LineDistributionResultsNodeStore>
    {
        public LineDistributionResultsNodeView()
        {
            AppendColumn("Project", new CellRendererText(), "text", 0);
            AppendColumn("Lines", new CellRendererText(), "text", 1);
            AppendColumn("Percentage of Solution", new CellRendererText(), "text", 2);
        }
    }

    public class LineDistributionResultsNodeStore : BaseResultsNodeStore
    {
        public LineDistributionResultsNodeStore() : base(typeof(LineDistributionResultsNode)) { }

        protected override void ResultChanged(SolutionAnalysis r)
        {
            foreach (var project in r.Projects)
                AddNode(new LineDistributionResultsNode(project));
            AddNode(new LineDistributionResultsTotalNode(r));
        }
    }

    public class LineDistributionResultsNode : TreeNode
    {
        [TreeNodeValue(Column = 0)]
        public string Name { get; protected set; }

        [TreeNodeValue(Column = 1)]
        public string Lines { get; protected set; }

        [TreeNodeValue(Column = 2)]
        public string Percentage { get; protected set; }

        IProjectAnalysis Result { get; }

        protected LineDistributionResultsNode() { }

        public LineDistributionResultsNode(IProjectAnalysis result)
        {
            Result = result;
            Name = Result.Project.Name;
            Lines = Result.Results.TotalLines.ToString();
            Percentage = $"{(double.IsNaN(Result.PercentageOfTotal) ? 0 : Result.PercentageOfTotal)}%";
        }
    }

    class LineDistributionResultsTotalNode : LineDistributionResultsNode
    {
        public LineDistributionResultsTotalNode(SolutionAnalysis solutionResult)
        {
            Name = "Total";
            Lines = solutionResult.TotalLines.ToString();
        }
    }
}