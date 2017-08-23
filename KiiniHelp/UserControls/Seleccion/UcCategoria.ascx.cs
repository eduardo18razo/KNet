using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArea;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.UserControls.Seleccion
{
    public partial class UcCategoria : UserControl
    {
        private readonly ServiceAreaClient _servicioArea = new ServiceAreaClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rptAreas.DataSource = _servicioArea.ObtenerAreasTipoUsuario(((Usuario)Session["UserData"]).IdTipoUsuario, false);
                rptAreas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAreaSelect_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton) sender;
                Response.Redirect("~/Publico/FrmServiceArea.aspx?idArea=" + lnkbtn.CommandArgument);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}