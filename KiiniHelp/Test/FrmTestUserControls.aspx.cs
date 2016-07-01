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
                UcDetalleTicket.IdTicket = 1;
                //UcCambiarEstatusTicket.IdTicket = 1;
                //UcCambiarEstatusTicket.IdUsuario = 2;
                //UcCambiarEstatusTicket.EsPropietario = true;
                //UcCambiarEstatusTicket1.IdTicket = 1;
                //UcCambiarEstatusTicket1.IdUsuario = 2;
                //UcCambiarEstatusTicket1.EsPropietario = true;
                //UcDetalleGrupoUsuario.IdUsuario = 2;
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
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalOrganizacion\");", true);
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
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalOrganizacion\");", true);
                upPrincipal.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}