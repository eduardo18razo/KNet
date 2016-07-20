using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KiiniHelp.Administracion.Ubicaciones
{
    public partial class FrmConsultaUbicaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UcConsultaUbicaciones.Modal = false;
        }
    }
}