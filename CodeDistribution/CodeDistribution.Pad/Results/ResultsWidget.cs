using Gtk;

namespace CodeDistribution.Pad.Results
{
    public abstract partial class ResultsWidget : Bin
    {
        protected abstract BaseResultsNodeView ResultList { get; }
        protected abstract BaseResultsPieChart Pie { get; }

        public SolutionAnalysis Results
        {
            get => results;
            set => SetResult(value);
        }
        SolutionAnalysis results;

        public ResultsWidget()
        {
            Build();
            RootVBox.PackStart(Pie.GtkWidget, true, true, 0);
            ResultNodeScroll.Add(ResultList);
        }

        void SetResult(SolutionAnalysis r)
        {
            if (results == r) return;
            results = r;

            ResultList.Results = results;
            Pie.Results = results;
        }
    }
}
