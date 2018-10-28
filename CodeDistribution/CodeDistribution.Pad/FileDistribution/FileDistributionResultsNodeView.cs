using CodeDistribution.Pad.Results;
using Gtk;

namespace CodeDistribution.Pad.FileDistribution
{
    [System.ComponentModel.ToolboxItem(true)]
    public class FileDistributionResultsNodeView : BaseResultsNodeView<FileDistributionResultsNodeStore>
    {
        public FileDistributionResultsNodeView()
        {
            AppendColumn("File Type", new CellRendererText(), "text", 0);
            AppendColumn("Lines", new CellRendererText(), "text", 1);
            AppendColumn("Percentage of Solution", new CellRendererText(), "text", 2);
        }
    }

    public class FileDistributionResultsNodeStore : BaseResultsNodeStore
    {
        public FileDistributionResultsNodeStore() : base(typeof(FileDistributionResultsNode)) { }

        protected override void ResultChanged(SolutionAnalysis r)
        {
            foreach (var extension in r.Extensions)
                AddNode(new FileDistributionResultsNode(extension.Value));
        }
    }
}

public class FileDistributionResultsNode : TreeNode
{
    [TreeNodeValue(Column = 0)]
    public string FileType { get; protected set; }

    [TreeNodeValue(Column = 1)]
    public string Lines { get; protected set; }

    [TreeNodeValue(Column = 2)]
    public string Percentage { get; protected set; }

    ExtensionAnalysis Analysis { get; }

    public FileDistributionResultsNode(ExtensionAnalysis analysis)
    {
        Analysis = analysis;
        FileType = Analysis.Extension;
        Lines = Analysis.Lines.ToString();
        Percentage = $"{(double.IsNaN(Analysis.PercentageOfSolution) ? 0 : Analysis.PercentageOfSolution)}%";
    }
}
