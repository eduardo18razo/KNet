using System;
using System.Web.UI;
using KinniNet.Business.Utils;

namespace KiiniHelp
{
    public partial class Default1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(UcLogCopia.Fail)
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "OpenDropLogin();", true);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        protected void lnkBtnEmpleado_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/DefaultUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
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
                Response.Redirect("~/DefaultUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
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
                Response.Redirect("~/DefaultUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}