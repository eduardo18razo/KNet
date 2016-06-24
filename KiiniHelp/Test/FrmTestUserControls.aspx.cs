using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KiiniHelp.Test
{
    public partial class FrmTestUserControls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UcDetalleGrupoUsuario.IdUsuario = 2;
                //UcDetalleOrganizacion.IdOrganizacion = 54;
                //UcDetalleUbicacion.IdUbicacion = 64;
                //UcDetalleUsuario1.OnCerraModal += UcDetalleUsuarioOnOnCerraModal;
                //UcDetalleUsuario1.IdUsuario = 1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void UcDetalleUsuarioOnOnCerraModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDetalleUsuario\");", true);
                upPrincipal.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnOnModal_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDetalleUsuario\");", true);
                upPrincipal.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}