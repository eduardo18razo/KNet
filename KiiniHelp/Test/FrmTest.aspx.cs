using System;
using System.Collections;
using KiiniHelp.ServiceTicket;

namespace KiiniHelp.Test
{
    public partial class FrmTest : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                UcAltaArbolAcceso.IdArbol = 3;
            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}