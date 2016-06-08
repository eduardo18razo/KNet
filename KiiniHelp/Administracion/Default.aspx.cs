using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArea;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Administracion
{
    public partial class Default : Page
    {
        readonly ServiceAreaClient _servicioArea = new ServiceAreaClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptAreas.DataSource = _servicioArea.ObtenerAreasUsuario(((Usuario)Session["UserData"]).Id);
                rptAreas.DataBind();
            }
        }
        protected void btnArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn != null)
                {
                    Session["AreaSeleccionada"] = btn.CommandArgument;
                    Response.Redirect("");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}