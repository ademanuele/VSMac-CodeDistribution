using System.Collections.Generic;

namespace CodeDistribution.Pad.Results
{
    public abstract class BaseResultsPieChart : PieChart.PieChart
    {
        public SolutionAnalysis Results
        {
            get => results;
            set => SetResults(value);
        }
        SolutionAnalysis results;

        protected virtual void SetResults(SolutionAnalysis r)
        {
            if (results == r) return;
            results = r;

            PieData = results == null ? null : DataForResult(r);
        }

        protected abstract List<PieChart.PieDataPoint> DataForResult(SolutionAnalysis r);
    }
}
