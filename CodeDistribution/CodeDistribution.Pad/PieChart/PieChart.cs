using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WebKit;

namespace CodeDistribution.Pad.PieChart
{
    public class PieChart : ExtendedWebView, IWKNavigationDelegate
    {
        const string HTML_RESOURCE = "PieChartHtml";
        const string CHART_JS_RESOURCE = "ChartJsBundle";
        static string baseHtml = MakeBaseHtml();

        public List<PieDataPoint> PieData { get => pieData; set => SetData(value); }
        List<PieDataPoint> pieData;

        WKNavigation initialNavigation;
        bool initialized;

        public PieChart()
        {
            NavigationDelegate = new PieChartNavigationDelegate();
        }

        void SetData( List<PieDataPoint> data )
        {
            pieData = data;

            if (!initialized)
            {
                initialNavigation = LoadHtmlString(baseHtml, null);
                return;
            }

            UpdatePieData();
        }

        void UpdatePieData()
        {
            var dataString = pieData == null ? "{}" : JsonConvert.SerializeObject(ChartData.MakeData(pieData));
            EvaluateJavaScript($"window.chart.data = {dataString};", ( result, error ) =>
                EvaluateJavaScript($"window.chart.update();", null));
        }

        static string MakeBaseHtml()
        {
            return LoadStringFromResource(HTML_RESOURCE)
                .Replace("REPLACE_WITH_CHART_JS", LoadStringFromResource(CHART_JS_RESOURCE));
        }

        static string LoadStringFromResource( string id )
        {
            using (var stream = typeof(PieChart).Assembly.GetManifestResourceStream(id))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        class PieChartNavigationDelegate : WKNavigationDelegate
        {
            public override void DidFinishNavigation( WKWebView webView, WKNavigation navigation )
            {
                var pie = (webView as PieChart);
                if (navigation == pie.initialNavigation)
                {
                    pie.initialNavigation = null;
                    pie.initialized = true;
                    pie.SetData(pie.PieData);
                }
            }
        }
    }
}
