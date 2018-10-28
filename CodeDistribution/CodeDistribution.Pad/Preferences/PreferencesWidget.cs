using System.Collections.Generic;
using Gtk;
using System.Linq;

namespace CodeDistribution.Pad
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PreferencesWidget : Bin
    {
        public Preferences Preferences { get => preferences; set => SetPreferences(value); }
        Preferences preferences;

        ProjectPreferencesNodeView projectsNodeView;

        public bool Enabled
        {
            get => IncludeEntry.Sensitive && ExcludeEntry.Sensitive && ExcludeDirectoriesEntry.Sensitive;
            set => IncludeEntry.Sensitive = ExcludeEntry.Sensitive = ExcludeDirectoriesEntry.Sensitive = value;
        }

        public PreferencesWidget()
        {
            Build();
            ProjectsNodeFrame.Add(projectsNodeView = new ProjectPreferencesNodeView());

            ShowAll();
        }

        public void Refresh()
        {
            if (preferences == null)
            {
                Enabled = false;
                Clear();
                return;
            }

            IncludeEntry.Text = ArrayToString(preferences.Filtering.Include);
            ExcludeEntry.Text = ArrayToString(preferences.Filtering.Exclude);
            ExcludeDirectoriesEntry.Text = ArrayToString(preferences.Filtering.ExcludeDirectories);
            projectsNodeView.ProjectPreferences = preferences.Projects;
            Enabled = true;
        }

        void SetPreferences( Preferences p )
        {
            if (p == preferences) return;
            preferences = p;

            Refresh();
        }

        void Clear()
        {
            IncludeEntry.Text = "";
            ExcludeEntry.Text = "";
            ExcludeDirectoriesEntry.Text = "";
            projectsNodeView.ProjectPreferences = null;
        }

        protected void HandleEntryUnfocused( object sender, FocusOutEventArgs args )
        {
            var newPreferences = StringToArray((sender as Entry).Text);
            (sender as Entry).Text = ArrayToString(newPreferences);


            if (sender == IncludeEntry)
                preferences.Filtering.Include = newPreferences;
            if (sender == ExcludeEntry)
                preferences.Filtering.Exclude = newPreferences;
            if (sender == ExcludeDirectoriesEntry)
                preferences.Filtering.ExcludeDirectories = newPreferences;
        }

        List<string> StringToArray( string input )
        {
            return input.Split(' ', ',')
                        .Select(s => s.Trim())
                        .Where(s => !s.Equals(",") && !s.Equals(" ") && !string.IsNullOrEmpty(s))
                        .ToList();
        }

        string ArrayToString( List<string> array )
        {
            if (array.Count == 0) return "";

            string result = array[0];
            for (int i = 1; i < array.Count; i++)
                result += ", " + array[i];

            return result;
        }

        protected void HandleInfoButtonClicked( object sender, System.EventArgs e )
        {
            using (var dialog = new AboutDialog.AboutDialog())
                dialog.Run();
        }
    }
}
