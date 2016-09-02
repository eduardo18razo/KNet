using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceConsultas;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Consultas
{
    public partial class FrmConsultaTickets : Page
    {
        private readonly  ServiceConsultasClient _servicioConsultas = new ServiceConsultasClient();
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
                    ucFiltrosConsulta.EsTicket = true;
                    ucFiltrosConsulta.EsConsulta = false;
                    ucFiltrosConsulta.EsEncuesta = false;
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
                gvResult.DataSource = _servicioConsultas.ConsultarTickets(((Usuario)Session["UserData"]).Id, ucFiltrosConsulta.FiltroGrupos, ucFiltrosConsulta.FiltroOrganizaciones,
                    ucFiltrosConsulta.FiltroUbicaciones, ucFiltrosConsulta.FiltroTipoArbol, ucFiltrosConsulta.FiltroTipificaciones,
                    ucFiltrosConsulta.FiltroPrioridad, ucFiltrosConsulta.FiltroEstatus, ucFiltrosConsulta.FiltroSla, ucFiltrosConsulta.FiltroFechas, 0, 1000);
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