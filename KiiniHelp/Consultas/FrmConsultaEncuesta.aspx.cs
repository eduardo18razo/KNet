using System;
using System.Collections.Generic;
using System.Linq;
using KiiniHelp.ServiceConsultas;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Consultas
{
    public partial class FrmConsultaEncuesta : System.Web.UI.Page
    {
        private readonly ServiceConsultasClient _servicioConsultas = new ServiceConsultasClient();

        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                pnlAlertaGeneral.Visible = value.Any();
                if (!pnlAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UcFiltrosConsulta.EsTicket = false;
                    UcFiltrosConsulta.EsConsulta = false;
                    UcFiltrosConsulta.EsEncuesta = true;
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            try
            {
                gvResult.DataSource = _servicioConsultas.ConsultarEncuestas(((Usuario)Session["UserData"]).Id, UcFiltrosConsulta.FiltroGrupos, UcFiltrosConsulta.FiltroOrganizaciones,
                    UcFiltrosConsulta.FiltroUbicaciones, UcFiltrosConsulta.FiltroTipoArbol, UcFiltrosConsulta.FiltroTipificaciones,
                    UcFiltrosConsulta.FiltroPrioridad, UcFiltrosConsulta.FiltroEstatus, UcFiltrosConsulta.FiltroSla, UcFiltrosConsulta.FiltroFechas, 0, 1000);
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }
    }
}