using CoreGraphics;
using Gtk;
using WebKit;
using Xwt.GtkBackend;

namespace CodeDistribution.Pad.PieChart
{
    public class ExtendedWebView : WKWebView
    {
        public ExtendedWebView() : base(new CGRect(), new WKWebViewConfiguration()) { }

        public Widget GtkWidget => gtkWidget ?? (gtkWidget = GtkMacInterop.NSViewToGtkWidget(this));
        Widget gtkWidget;
    }
}
