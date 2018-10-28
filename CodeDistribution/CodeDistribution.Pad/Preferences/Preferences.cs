using System.Collections.Generic;
using MonoDevelop.Projects;

namespace CodeDistribution.Pad
{
    public class Preferences
    {
        public FilteringPreferences Filtering { get; set; }
        public ProjectPreferences Projects { get; set; }

        public Preferences( List<Project> projects = null )
        {
            Filtering = new FilteringPreferences();
            Projects = new ProjectPreferences(projects);
        }
    }

    public class FilteringPreferences
    {
        public List<string> Include { get; set; } = new List<string> { "cs" };
        public List<string> Exclude { get; set; } = new List<string> { "designer.cs", "xaml.cs" };
        public List<string> ExcludeDirectories { get; set; } = new List<string> { "bin", "obj" };
    }

    public class ProjectPreferences : List<ProjectPreference>
    {
        public ProjectPreferences( List<Project> projects = null )
        {
            if (projects == null) return;

            foreach (var project in projects)
                Add(new ProjectPreference(project));
        }
    }

    public class ProjectPreference
    {
        public Project Project { get; }
        public bool Include { get; set; } = true;

        public ProjectPreference( Project project )
        {
            Project = project;
        }
    }
}
