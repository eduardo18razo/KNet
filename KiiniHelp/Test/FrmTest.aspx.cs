using System;

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

            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void OnClick(object sender, EventArgs e)
        {
            string url = ResolveUrl("~/FrmEncuesta.aspx?IdTicket=3");
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=300,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}