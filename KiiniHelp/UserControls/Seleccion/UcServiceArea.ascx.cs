using System;
using System.Web.UI;
using KiiniHelp.ServiceArbolAcceso;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Seleccion
{
    public partial class UcServiceArea : UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["idArea"] != null)
                {

                    rptConsultas.DataSource = new ServiceArbolAccesoClient().ObtenerArbolesAccesoTerminalAllTipificacion(int.Parse(Request.Params["idArea"]), ((Usuario)Session["UserData"]).IdTipoUsuario, (int)BusinessVariables.EnumTipoArbol.ConsultarInformacion, null, null, null, null, null, null, null);
                    rptConsultas.DataBind();
                    rptServicios.DataSource = new ServiceArbolAccesoClient().ObtenerArbolesAccesoTerminalAllTipificacion(int.Parse(Request.Params["idArea"]), ((Usuario)Session["UserData"]).IdTipoUsuario, (int)BusinessVariables.EnumTipoArbol.SolicitarServicio, null, null, null, null, null, null, null);
                    rptServicios.DataBind();
                    rptIncidentes.DataSource = new ServiceArbolAccesoClient().ObtenerArbolesAccesoTerminalAllTipificacion(int.Parse(Request.Params["idArea"]), ((Usuario)Session["UserData"]).IdTipoUsuario, (int)BusinessVariables.EnumTipoArbol.ReportarProblemas, null, null, null, null, null, null, null);
                    rptIncidentes.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}