using System;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Font = System.Drawing.Font;

namespace KiiniHelp.Test
{
    public partial class FrmTest : System.Web.UI.Page
    {
        
        //private void SendMessageAltiria()
        //{
        //    HttpWebRequest loHttp =
        //        (HttpWebRequest)WebRequest.Create("http://www.altiria.net/api/http");

        //    string lcPostData = "cmd=sendsms&domainId=demo&login=edu18&passwd=fhmdemok&dest=525554374934&msg=tercer";

        //    byte[] lbPostBuffer = System.Text.Encoding.GetEncoding("utf-8").GetBytes(lcPostData);
        //    loHttp.Method = "POST";
        //    loHttp.ContentType = "application/x-www-form-urlencoded";
        //    loHttp.ContentLength = lbPostBuffer.Length;
        //    loHttp.Timeout = 60000;
        //    String error = "";
        //    String response = "";
        //    try
        //    {
        //        Stream loPostData = loHttp.GetRequestStream();
        //        loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
        //        loPostData.Close();
        //        HttpWebResponse loWebResponse = (HttpWebResponse)loHttp.GetResponse();
        //        Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
        //        StreamReader loResponseStream =
        //            new StreamReader(loWebResponse.GetResponseStream(), enc);
        //        response = loResponseStream.ReadToEnd();
        //        loWebResponse.Close();
        //        loResponseStream.Close();
        //    }
        //    catch (WebException e)
        //    {
        //        if (e.Status == WebExceptionStatus.ConnectFailure)
        //            error = "Error en la conexión";
        //        else if (e.Status == WebExceptionStatus.Timeout)
        //            error = "Error TimeOut";
        //        else
        //            error = e.Message;
        //    }
        //    finally
        //    {
        //        if (error != "")
        //            lblerrorMensaje.Text = error;
        //        else
        //            lblerrorMensaje.Text =  response;
        //    }
        //}
        public int IdCatalogo { get { return 10; } }
        protected void btnGetSelectedValues_Click(object sender, EventArgs e)
        {
            //string selectedValues = string.Empty;
            //foreach (ListItem li in lstBoxTest.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        selectedValues += li.Text + ",";
            //    }
            //}
            //Response.Write(selectedValues.ToString());
        }

         protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
         {
             TextBox1.Text = GridView1.SelectedRow.Cells[2].Text;
         }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ucCargaCatalgo.EsAlta = true;
            ////////////Set the appropriate ContentType.
            //////////Response.ContentType = "Application/msword";
            ////////////Get the physical path to the file.
            //////////string FilePath = MapPath("CVEduardoCerritos.docx");
            ////////////Write the file directly to the HTTP content output stream.
            //////////Response.WriteFile(FilePath);
            //////////Response.End();

            // PDF
            //string FilePath = Server.MapPath("CELE860311HDFRCD04.pdf");
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(FilePath);
            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //}


            ////Response.ContentType = "application/pdf";
            ////Response.AddHeader("content-disposition", "attachment;filename=CELE860311HDFRCD04.pdf");
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////StringWriter sw = new StringWriter();
            ////HtmlTextWriter hw = new HtmlTextWriter(sw);
            ////this.Page.RenderControl(hw);
            ////StringReader sr = new StringReader(sw.ToString());
            ////Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            ////HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            ////PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            ////pdfDoc.Open();
            ////htmlparser.Parse(sr);
            ////pdfDoc.Close();
            ////Response.Write(pdfDoc);
            ////Response.End();
            //ucAltaInformacionConsulta.EsAlta = true;
            //lstBoxTest.DataSource = new ServiceTipoUsuarioClient().ObtenerTiposUsuario(true);
            //lstBoxTest.DataTextField = "Descripcion";
            //lstBoxTest.DataValueField = "Id";
            //lstBoxTest.DataBind();
            //SendMessageAltiria();
            //if (!IsPostBack)
            //{
            //    DataTable dt = new DataTable("Data");
            //    dt.Columns.Add(new DataColumn("Id"));
            //    dt.Columns.Add(new DataColumn("Descripcion"));
            //    dt.Columns.Add(new DataColumn("01/01/2016"));
            //    dt.Columns.Add(new DataColumn("02/01/2016"));
            //    dt.Columns.Add(new DataColumn("03/01/2016"));
            //    dt.Columns.Add(new DataColumn("04/01/2016"));
            //    dt.Columns.Add(new DataColumn("05/01/2016"));
            //    dt.Columns.Add(new DataColumn("06/01/2016"));

            //    //Random rand = new Random(5);
            //    //for (int i = 0; i < 5; i++)
            //    //{
            //    //    dt.Rows.Add("Kiininet " + i, rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20));
            //    //}
            //    dt.Rows.Add(1, "Kiininet uno", 2, 4, 6, 8, 1, 3);
            //    dt.Rows.Add(2, "Kiininet dos", 7, 9, 2, 4, 6, 8);
            //    dt.Rows.Add(3, "Kiininet tres", 1, 3, 7, 2, 4, 6);
            //    BusinessGraficoStack.Stack.GenerarGrafica(ChartStack, dt, "ubicaciones");


            //    ChartStack.ChartAreas[0].AxisX.Title = "Ubicación";
            //    ChartStack.ChartAreas[0].AxisY.Title = "Tickets";
            //    //ChartStack.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //    BusinessGraficoStack.ColumnsClustered.GenerarGrafica(chartColumn, dt, "ubicaciones");
            //    //chartColumn.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

            //    DataTable dtStack = new DataTable("Data");
            //    dtStack.Columns.Add("Ubicacion");
            //    dtStack.Columns.Add("Total");
            //    dtStack.Columns.Add("Frecuencia");

            //    dtStack.Rows.Add("MÉXICO>OFICINAS CENTRALES", 7, 77.77);
            //    dtStack.Rows.Add("MÉXICO", 2, 100);

            //    AfterLoad(dtStack);
            //    //Chart1.SaveImage();
            //}
            //ChartStack.Click += ChartStackOnClick;
        }

        private void ChartStackOnClick(object sender, ImageMapEventArgs imageMapEventArgs)
        {
            try
            {
                //lblFormatMonth.Text = sender;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {

        }

        private void MakeBarChart(Chart grafico, DataTable dt)
        {
            grafico.Series["Ubicacion"].Label = "#VALY{#.##}";
            for (int point = 0; point < dt.Rows.Count; point++)
            {
                grafico.Series["Ubicacion"].Points.Add(new DataPoint
                {
                    AxisLabel = dt.Rows[point][0].ToString(),
                    YValues = new double[] { Convert.ToDouble(dt.Rows[point][1].ToString()) }
                });
            }
            foreach (Series serie in grafico.Series)
            {
                serie.Font = new Font("Tahoma", 8, FontStyle.Bold);
                foreach (DataPoint dp in serie.Points)
                {
                    dp.PostBackValue = "#VALX,#VALY";
                    dp.LabelPostBackValue = "label";
                    dp.LegendPostBackValue = "legendVALX";
                }
            }

        }

        public void AfterLoad(DataTable dt)
        {

            //Chart1.Series.Add("Ubicacion");
            //MakeBarChart(Chart1, dt);
            //MakeParetoChart(Chart1, "Ubicacion", "Pareto");
            //Chart1.Series["Pareto"].ChartType = SeriesChartType.Line;
            //Chart1.Series["Pareto"].IsValueShownAsLabel = true;
            //Chart1.Series["Pareto"].MarkerColor = Color.Red;
            //Chart1.Series["Pareto"].MarkerBorderColor = Color.MidnightBlue;
            //Chart1.Series["Pareto"].MarkerStyle = MarkerStyle.Circle;
            //Chart1.Series["Pareto"].MarkerSize = 8;
            //Chart1.Series["Pareto"].LabelFormat = "0.#";
            //Chart1.Series["Pareto"].Color = Color.FromArgb(252, 180, 65);
            //Chart1.Legends[0].Title = "Stack";
        }



        private void MakeParetoChart(Chart chart, string srcSeriesName, string destSeriesName)
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

            //chart.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "P0";

            // turn off the end point values of the primary X axis

            chart.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

            double percentage = 0.0;

            foreach (DataPoint pt in chart.Series[srcSeriesName].Points)
            {

                percentage += (pt.YValues[0] / total * 100.0);
                if (percentage >= 1000)
                {

                }
                destSeries.Points.Add(Math.Round(percentage, 2));

            }

        }

        //protected void OnClick(object sender, EventArgs e)
        //{
        //    string url = ResolveUrl("~/FrmEncuesta.aspx?IdTicket=3");
        //    string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=300,top=100,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}

        protected void OnClick(object sender, EventArgs e)
        {

            //lblFormatWeek.Text = txtWeek.Text;
            //lblFormatMonth.Text = txtMonth.Text;

            //BusinessCadenas.Fechas.ObtenerRangoFechasNumeroSemana(Convert.ToInt32(txtWeek.Text.Split('-')[0]),
            //    Convert.ToInt32(txtWeek.Text.Split('-')[1].Substring(1)));
            //DateTime jan1 = new DateTime(Convert.ToInt32(txtWeek.Text.Split('-')[0]), 1, 1);

            //int daysOffset = DayOfWeek.Tuesday - jan1.DayOfWeek;

            //DateTime firstMonday = jan1.AddDays(daysOffset);

            //var cal = CultureInfo.CurrentCulture.Calendar;

            //int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            //int weekNum = Convert.ToInt32(txtWeek.Text.Split('-')[1].Substring(1));

            //if (firstWeek <= 1)
            //{
            //    weekNum -= 1;
            //}

            //var result = firstMonday.AddDays(weekNum * 7 + 0 - 1);
        }

    }
}