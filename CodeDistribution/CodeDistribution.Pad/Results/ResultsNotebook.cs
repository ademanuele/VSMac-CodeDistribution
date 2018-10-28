using CodeDistribution.Pad.FileDistribution;
using Gtk;

namespace CodeDistribution.Pad.Results
{
    public class ResultsNotebook : Notebook
    {
        public LineDistributionResultsWidget LineDistribution { get; }
        public FileDistributionResultsWidget FileDistribution { get; }

        public ResultsNotebook()
        {
            TabPos = PositionType.Right;
            AppendPage(LineDistribution = new LineDistributionResultsWidget(), BoldLabel("Lines"));
            AppendPage(FileDistribution = new FileDistributionResultsWidget(), BoldLabel("Files"));
            ShowAll();
        }

        Label BoldLabel(string text) => new Label { LabelProp = $"<b>{text}</b>", UseMarkup = true, Angle = 270 };
    }
}
