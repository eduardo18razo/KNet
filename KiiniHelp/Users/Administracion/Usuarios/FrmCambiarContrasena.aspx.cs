using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KiiniHelp.Users.Administracion.Usuarios
{
    public partial class FrmCambiarContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCambiarContrasena.OnAceptarModal += ucCambiarContrasena_OnAceptarModal;
        }

        void ucCambiarContrasena_OnAceptarModal()
        {
            try
            {
                Response.Redirect("~/Users/DashBoard.aspx");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}