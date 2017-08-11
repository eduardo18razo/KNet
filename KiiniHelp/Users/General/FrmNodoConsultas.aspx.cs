using System;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniNet.Entities.Operacion;

namespace KiiniHelp.Users.General
{
    public partial class FrmNodoConsultas : Page
    {
        private readonly ServiceArbolAccesoClient _servicoArbol = new ServiceArbolAccesoClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int idArbol = Convert.ToInt32(Request.QueryString["IdArbol"]);
                    UcPreviewConsulta.MuestraEvaluacion = _servicoArbol.ObtenerArbolAcceso(idArbol).Evaluacion;
                    Session["PreviewAltaDataConsulta"] = new ServiceInformacionConsultaClient().ObtenerInformacionConsultaById(new ServiceArbolAccesoClient().ObtenerArbolAcceso(idArbol).InventarioArbolAcceso.First().InventarioInfConsulta.First().IdInfConsulta);
                    UcPreviewConsulta.MuestraPreview((InformacionConsulta)Session["PreviewAltaDataConsulta"]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}