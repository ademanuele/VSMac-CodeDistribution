using System;
using System.Linq;
using MonoDevelop.Projects;
using CodeDistribution.Common;
using System.Collections.Generic;
using Gtk;
using CodeDistribution.Pad.Results;

namespace CodeDistribution.Pad
{
    public partial class MainWidget : Bin
    {
        public List<Project> Projects { get => projects; set => SetProjects(value); }
        List<Project> projects = new List<Project>();

        ResultsNotebook resultsNotebook;
        PreferencesWidget preferencesWidget;
        Preferences preferences;

        Common.CodeDistribution distribution;

        public MainWidget()
        {
            Build();
            RootVBox.PackStart(resultsNotebook = new ResultsNotebook(), true, true, 0);
            distribution = new Common.CodeDistribution();
            preferencesWidget = new PreferencesWidget();
            preferencesWidget.Preferences = preferences = new Preferences();
        }

        protected void HandleReloadClicked( object sender, EventArgs e ) => Reload();

        void SetProjects( List<Project> p )
        {
            if (p == projects) return;
            projects = p;

            preferences.Projects = new ProjectPreferences(p);
            preferencesWidget.Refresh();
            Reload();
        }

        void Reload()
        {
            //ClearResults();
            var r = AnalyseProjects();
            SetResults(r);
        }

        SolutionAnalysis AnalyseProjects()
        {
            distribution.IncludedExtensions = preferences.Filtering.Include;
            distribution.IgnoredExtensions = preferences.Filtering.Exclude;
            distribution.IgnoredDirectories = ExcludedDirectories;
            var analysisResults = distribution.ProcessDirectories(ProjectPaths);
            var results = new Dictionary<Project, DirectoryAnalysis>();
            for (int i = 0; i < analysisResults.Count; i++)
                results.Add(projects[i], analysisResults[i]);
            return new SolutionAnalysis(results);
        }

        List<string> ProjectPaths =>
            preferences.Projects.Select(p => p.Project.BaseDirectory.FullPath.ToString()).ToList();

        List<string> ExcludedDirectories =>
                preferences.Projects.Where(p => !p.Include).Select(p => p.Project.BaseDirectory.FullPath.ToString())
                                    .Concat(preferences.Filtering.ExcludeDirectories)
                                    .ToList();

        void SetResults( SolutionAnalysis results )
        {
            resultsNotebook.LineDistribution.Results = results;
            resultsNotebook.FileDistribution.Results = results;
        }

        //void ClearResults()
        //{
        //    resultsNotebook.LineDistribution.Results = null;
        //    resultsNotebook.FileDistribution.Results = null;
        //}

        protected void HandlePreferencesClicked( object sender, EventArgs e )
        {
            if (!PreferencesShown)
                ShowPreferences();
            else
            {
                Reload();
                HidePreferences();
            }
        }

        bool PreferencesShown => !(RootVBox.Children[0] is ResultsNotebook);

        void HidePreferences()
        {
            if (!PreferencesShown) return;

            RootVBox.Remove(preferencesWidget);
            RootVBox.PackStart(resultsNotebook, true, true, 0);
            ReloadButton.Show();
            PreferencesButton.Label = "";
            PreferencesButton.Image.Show();
            ((Box.BoxChild)ToolbarHBox[PreferencesButton]).Expand = false;
            ((Box.BoxChild)ToolbarHBox[PreferencesButton]).Fill = false;
        }

        void ShowPreferences()
        {
            if (PreferencesShown) return;

            RootVBox.Remove(RootVBox.Children[0]);
            RootVBox.PackStart(preferencesWidget, true, true, 0);
            ReloadButton.Hide();
            PreferencesButton.Label = "Close";
            PreferencesButton.Image.Hide();
            ((Box.BoxChild)ToolbarHBox[PreferencesButton]).Expand = true;
            ((Box.BoxChild)ToolbarHBox[PreferencesButton]).Fill = true;
        }
    }
}
