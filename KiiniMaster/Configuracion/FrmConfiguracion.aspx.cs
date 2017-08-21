using System;
using System.Web.UI;

namespace KiiniMaster.Configuracion
{
    public partial class FrmConfiguracion : Page
    {
        //private BusinessCatalogosUsuario _nCatalogos = new BusinessCatalogosUsuario();
        private void Source()
        {
            //try
            //{
            //    rptAplicaciones.DataSource = BusinessCatalogos.Parametros.ObtenerAplicaciones();
            //    rptAplicaciones.DataBind();

            //    rptTipoUsuairio.DataSource = BusinessCatalogos.Parametros.ObtenerTiposUsuario();
            //    rptTipoUsuairio.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Source();
        }
    }
}