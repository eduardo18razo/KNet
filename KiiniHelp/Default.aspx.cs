using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KinniNet.Business.Utils;

namespace KiiniHelp
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkBtnEmpleado_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/DefaultAspnet.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void lnkBtnCliente_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/DefaultAspnet.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkBtnProveedor_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/DefaultAspnet.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}