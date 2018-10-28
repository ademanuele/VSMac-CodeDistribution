using System.Diagnostics;
using Gtk;

namespace CodeDistribution.Pad.AboutDialog
{
    public partial class AboutDialog : Dialog
    {
        public AboutDialog()
        {
            Build();

            SetupNameLabel();
            SetupVersionLabel();
            AddContactButton();
            ShowAll();
        }

        void SetupNameLabel()
        {
            ExtensionNameLabel.ModifyFont(Pango.FontDescription.FromString("monospace Ultra-Bold 24"));
            ExtensionNameLabel.Text = "Code Distribution Extension";
        }

        void SetupVersionLabel()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            VersionLabel.Text = version;
        }

        void AddContactButton()
        {
            var button = new LinkButton("https://github.com/ademanuele/VSMac-CodeDistribution", "GitHub");
            ContentVBox.PackEnd(button);
        }

        protected override void OnResponse( ResponseType response )
        {
            base.OnResponse(response);

            if (response == ResponseType.Ok) Hide();
        }
    }
}
