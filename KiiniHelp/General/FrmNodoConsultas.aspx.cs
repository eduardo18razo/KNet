using System;
using System.Web.UI;

namespace KiiniHelp.General
{
    public partial class FrmNodoConsultas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int idArbol = Convert.ToInt32(Request.QueryString["IdArbol"]);
                    UcInformacionConsulta.IdArbol = idArbol;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}