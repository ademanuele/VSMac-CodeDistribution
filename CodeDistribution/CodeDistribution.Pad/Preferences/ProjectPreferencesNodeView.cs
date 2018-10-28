using System.Collections.Generic;
using Gtk;

namespace CodeDistribution.Pad
{
    [System.ComponentModel.ToolboxItem(true)]
    public class ProjectPreferencesNodeView : NodeView
    {
        public List<ProjectPreference> ProjectPreferences
        {
            get => projectStore.ProjectPreferences;
            set => projectStore.ProjectPreferences = value;
        }
        ProjectPreferenceNodeStore projectStore;

        public ProjectPreferencesNodeView()
        {
            NodeStore = projectStore = new ProjectPreferenceNodeStore();
            EnableGridLines = TreeViewGridLines.Both;
            AppendColumn("Include", new IncludeCellRenderer(projectStore), "active", 0);
            AppendColumn("Project", new CellRendererText(), "text", 1);
        }

        class IncludeCellRenderer : CellRendererToggle
        {
            NodeStore Store { get; }

            public IncludeCellRenderer(NodeStore store)
            {
                Store = store;
                Activatable = true;
                Toggled += (o, args) =>
                {
                    var node = store.GetNode(new TreePath(args.Path)) as ProjectPreferenceTreeNode;
                    node.Include = !node.Include;
                };
            }
        }

        class ProjectPreferenceNodeStore : NodeStore
        {
            public List<ProjectPreference> ProjectPreferences
            {
                get => projectPreferences;
                set => SetPreferences(value);
            }
            List<ProjectPreference> projectPreferences;

            public ProjectPreferenceNodeStore() : base(typeof(ProjectPreferenceTreeNode)) { }

            void SetPreferences(List<ProjectPreference> preferences)
            {
                if (projectPreferences == preferences) return;
                projectPreferences = preferences;

                Clear();
                if (projectPreferences == null) return;

                foreach (var preference in projectPreferences)
                    AddNode(new ProjectPreferenceTreeNode(preference));
            }
        }

        class ProjectPreferenceTreeNode : TreeNode
        {
            [TreeNodeValue(Column = 0)]
            public bool Include
            {
                get => preference.Include;
                set => preference.Include = value;
            }

            [TreeNodeValue(Column = 1)]
            public string Project => preference.Project.Name;

            ProjectPreference preference;

            public ProjectPreferenceTreeNode(ProjectPreference p)
            {
                preference = p;
            }
        }
    }
}
