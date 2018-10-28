using System.Collections.Generic;
using CodeDistribution.Common;
using MonoDevelop.Projects;
using System.Linq;
using System;

namespace CodeDistribution.Pad
{
    public class SolutionAnalysis
    {
        public List<IProjectAnalysis> Projects { get; }
        public double TotalLines { get; private set; }
        public Dictionary<string, ExtensionAnalysis> Extensions { get; private set; }

        public SolutionAnalysis( Dictionary<Project, DirectoryAnalysis> results )
        {
            Projects = results.Select(r => new ProjectAnalysis(r.Key, r.Value) as IProjectAnalysis).ToList();
            AnalyseResults();
        }

        void AnalyseResults()
        {
            CalculateTotalLines();
            CalculateLineDistribution();
            CalculateExtensionDistribution();
        }

        void CalculateTotalLines() =>
            Projects.ForEach(p => TotalLines += p.Results.TotalLines);

        void CalculateLineDistribution() =>
            Projects.ForEach(p =>
            {
                var percentage = Math.Round((p.Results.TotalLines / TotalLines) * 100, 2);
                if (double.IsNaN(percentage)) percentage = 0;
                (p as ProjectAnalysis).PercentageOfTotal = percentage;
            });

        void CalculateExtensionDistribution()
        {
            Extensions = new Dictionary<string, ExtensionAnalysis>();
            foreach (var project in Projects)
            {
                foreach (var extension in project.Results.LinesByExtension)
                {
                    if (Extensions.TryGetValue(extension.Key, out var e))
                        e.Lines += extension.Value;
                    else
                        Extensions.Add(extension.Key, new ExtensionAnalysis(extension.Key, extension.Value, extension.Value));
                }
            }

            foreach (var extension in Extensions)
                extension.Value.PercentageOfSolution = Math.Round((extension.Value.Lines / TotalLines) * 100, 2);
        }
    }

    class ProjectAnalysis : IProjectAnalysis
    {
        public Project Project { get; }
        public DirectoryAnalysis Results { get; }

        public double PercentageOfTotal { get; set; }

        public ProjectAnalysis( Project project, DirectoryAnalysis results )
        {
            Project = project;
            Results = results;
        }
    }
}

public interface IProjectAnalysis
{
    Project Project { get; }
    DirectoryAnalysis Results { get; }
    double PercentageOfTotal { get; }
}

public class ExtensionAnalysis
{
    public string Extension { get; }
    public int Lines { get; set; }
    public double PercentageOfSolution { get; set; }

    public ExtensionAnalysis( string extension, int lines, double percentageOfSolution )
    {
        Extension = extension;
        Lines = lines;
        PercentageOfSolution = percentageOfSolution;
    }
}
