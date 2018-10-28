using System;
using Gtk;

namespace CodeDistribution.Pad.Results
{
    public abstract class BaseResultsNodeView : NodeView
    {
        public SolutionAnalysis Results
        {
            get => (NodeStore as BaseResultsNodeStore).Results;
            set => (NodeStore as BaseResultsNodeStore).Results = value;
        }

        public BaseResultsNodeView()
        {
            EnableGridLines = TreeViewGridLines.Both;
            NodeStore = MakeNodeStore();
        }

        protected abstract BaseResultsNodeStore MakeNodeStore();
    }

    public class BaseResultsNodeView<T> : BaseResultsNodeView where T : BaseResultsNodeStore, new()
    {
        protected override BaseResultsNodeStore MakeNodeStore() =>
            Activator.CreateInstance(typeof(T)) as BaseResultsNodeStore;
    }

    public abstract class BaseResultsNodeStore : NodeStore
    {
        public SolutionAnalysis Results { get => results; set => SetResult(value); }
        protected SolutionAnalysis results;

        public BaseResultsNodeStore(Type nodeType) : base(nodeType) { }

        void SetResult(SolutionAnalysis r)
        {
            if (results == r) return;
            results = r;

            Clear();
            if (results == null) return;

            ResultChanged(r);
        }

        protected abstract void ResultChanged(SolutionAnalysis r);
    }
}
