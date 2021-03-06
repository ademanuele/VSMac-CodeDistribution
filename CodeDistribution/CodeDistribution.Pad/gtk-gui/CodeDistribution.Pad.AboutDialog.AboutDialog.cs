
// This file has been generated by the GUI designer. Do not modify.
namespace CodeDistribution.Pad.AboutDialog
{
	public partial class AboutDialog
	{
		private global::Gtk.VBox ContentVBox;

		private global::Gtk.Label ExtensionNameLabel;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Label label20;

		private global::Gtk.Label VersionLabel;

		private global::Gtk.Label label5;

		private global::Gtk.Label label7;

		private global::Gtk.Button buttonCancel;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget CodeDistribution.Pad.AboutDialog.AboutDialog
			this.Name = "CodeDistribution.Pad.AboutDialog.AboutDialog";
			this.Title = global::Mono.Unix.Catalog.GetString("About");
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			this.BorderWidth = ((uint)(10));
			this.Resizable = false;
			this.AllowGrow = false;
			this.DestroyWithParent = true;
			this.Gravity = ((global::Gdk.Gravity)(5));
			// Internal child CodeDistribution.Pad.AboutDialog.AboutDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.ContentVBox = new global::Gtk.VBox();
			this.ContentVBox.Name = "ContentVBox";
			this.ContentVBox.Spacing = 6;
			// Container child ContentVBox.Gtk.Box+BoxChild
			this.ExtensionNameLabel = new global::Gtk.Label();
			this.ExtensionNameLabel.Name = "ExtensionNameLabel";
			this.ExtensionNameLabel.Xalign = 0F;
			this.ContentVBox.Add(this.ExtensionNameLabel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.ContentVBox[this.ExtensionNameLabel]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child ContentVBox.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label20 = new global::Gtk.Label();
			this.label20.Name = "label20";
			this.label20.Xalign = 0F;
			this.label20.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Version: </b>");
			this.label20.UseMarkup = true;
			this.hbox4.Add(this.label20);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label20]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.VersionLabel = new global::Gtk.Label();
			this.VersionLabel.Name = "VersionLabel";
			this.hbox4.Add(this.VersionLabel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.VersionLabel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.ContentVBox.Add(this.hbox4);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.ContentVBox[this.hbox4]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child ContentVBox.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 0F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("by Arthur Demanuele");
			this.ContentVBox.Add(this.label5);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.ContentVBox[this.label5]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child ContentVBox.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.Xalign = 0F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Comments issues or queries? Send them below.");
			this.ContentVBox.Add(this.label7);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.ContentVBox[this.label7]));
			w7.Position = 3;
			w7.Expand = false;
			w7.Fill = false;
			w1.Add(this.ContentVBox);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(w1[this.ContentVBox]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Internal child CodeDistribution.Pad.AboutDialog.AboutDialog.ActionArea
			global::Gtk.HButtonBox w9 = this.ActionArea;
			w9.Name = "dialog1_ActionArea";
			w9.Spacing = 10;
			w9.BorderWidth = ((uint)(5));
			w9.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.FocusOnClick = false;
			this.buttonCancel.Label = "gtk-ok";
			this.AddActionWidget(this.buttonCancel, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w10 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9[this.buttonCancel]));
			w10.Expand = false;
			w10.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 714;
			this.DefaultHeight = 300;
			this.Show();
		}
	}
}
