using System;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace KiiniHelp.Test
{
    public partial class FrmTest : System.Web.UI.Page
    {
        //public int? IdMascara
        //{
        //    get { return 1; }
        //}

        //public int? IdTicket
        //{
        //    get { return 1; }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ucDetalleMascaraCaptura.IdTicket = 10;
                //ucDetalleMascaraCaptura.CargarDatos();
                AfterLoad();
            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            
        }
        private void RandomData(Series series, int numOfPoints)
        {

            Random rand;

            // Use a number to calculate a starting value for

            // the pseudo-random number sequence

            rand = new Random(5);

            // Generate random Y values

            for (int point = 0; point < numOfPoints; point++)
            {

                series.Points.AddY(rand.Next(49) + 1);

            }

        }

        public void AfterLoad()
        {

            // Number of data points

            int numOfPoints = 5;

            // Generate rundom data

            RandomData(Chart1.Series["Default"], numOfPoints);

            // Make Pareto Chart

            MakeParetoChart(Chart1, "Default", "Pareto");

            // Set chart types for output data

            Chart1.Series["Pareto"].ChartType = SeriesChartType.Line;

            // set the markers for each point of the Pareto Line

            Chart1.Series["Pareto"].IsValueShownAsLabel = true;

            Chart1.Series["Pareto"].MarkerColor = Color.Red;

            Chart1.Series["Pareto"].MarkerBorderColor = Color.MidnightBlue;

            Chart1.Series["Pareto"].MarkerStyle = MarkerStyle.Circle;

            Chart1.Series["Pareto"].MarkerSize = 8;

            Chart1.Series["Pareto"].LabelFormat = "0.#";  // format with one decimal and leading zero

            // Set Color of line Pareto chart

            Chart1.Series["Pareto"].Color = Color.FromArgb(252, 180, 65);

        }

        void MakeParetoChart(Chart chart, string srcSeriesName, string destSeriesName)
        {

            // get name of the ChartAre of the source series

            string strChartArea = chart.Series[srcSeriesName].ChartArea;

            // ensure that the source series is a column chart type

            chart.Series[srcSeriesName].ChartType = SeriesChartType.Column;

            // sort the data in all series by their values in descending order

            chart.DataManipulator.Sort(PointSortOrder.Descending, srcSeriesName);

            // find the total of all points in the source series

            double total = 0.0;

            foreach (DataPoint pt in chart.Series[srcSeriesName].Points)

                total += pt.YValues[0];

            // set the max value on the primary axis to total

            chart.ChartAreas[strChartArea].AxisY.Maximum = total;

            // create the destination series and add it to the chart

            Series destSeries = new Series(destSeriesName);

            chart.Series.Add(destSeries);

            // ensure that the destination series is either a Line or Spline chart type

            destSeries.ChartType = SeriesChartType.Line;

            destSeries.BorderWidth = 3;

            // assign the series to the same chart area as the column chart is assigned

            destSeries.ChartArea = chart.Series[srcSeriesName].ChartArea;

            // assign this series to use the secondary axis and set it maximum to be 100%

            destSeries.YAxisType = AxisType.Secondary;

            chart.ChartAreas[strChartArea].AxisY2.Maximum = 100;

            // locale specific percentage format with no decimals

            chart.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "P0";

            // turn off the end point values of the primary X axis

            chart.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

            double percentage = 0.0;

            foreach (DataPoint pt in chart.Series[srcSeriesName].Points)
            {

                percentage += (pt.YValues[0] / total * 100.0);

                destSeries.Points.Add(Math.Round(percentage, 2));

            }

        }

        protected void OnClick(object sender, EventArgs e)
        {
            string url = ResolveUrl("~/FrmEncuesta.aspx?IdTicket=3");
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=300,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}