using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Components;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;

namespace CodeDistribution.Pad
{
    public class CodeDistributionPad : PadContent
    {
        public override string Id => "CodeDistribution.Pad.CodeDistributionPad";

        public override Control Control => padWidget;
        MainWidget padWidget;

        protected override void Initialize(IPadWindow window)
        {
            base.Initialize(window);

            padWidget = new MainWidget();
            padWidget.ShowAll();
            StartListeningToProjectChanges();
            padWidget.Projects = AllCurrentProjects();
        }

        public override void Dispose()
        {
            base.Dispose();
            StopListeningToProjectChanges();
        }

        public void StopListeningToProjectChanges()
        {
            IdeApp.Workspace.SolutionLoaded -= HandleSolutionLoadedUnloaded;
            IdeApp.Workspace.SolutionUnloaded -= HandleSolutionLoadedUnloaded;
        }

        public void StartListeningToProjectChanges()
        {
            IdeApp.Workspace.SolutionLoaded += HandleSolutionLoadedUnloaded;
            IdeApp.Workspace.SolutionUnloaded += HandleSolutionLoadedUnloaded;
        }

        void HandleSolutionLoadedUnloaded(object sender, SolutionEventArgs e) =>
            padWidget.Projects = AllCurrentProjects();

        List<Project> AllCurrentProjects()
        {
            var projects = new List<Project>();

            var solutions = IdeApp.Workspace.GetAllSolutions();
            if (!solutions.Any()) return projects;

            foreach (var solution in solutions)
                foreach (var project in solution.GetAllProjects())
                    projects.Add(project);

            return projects;
        }
    }
}
