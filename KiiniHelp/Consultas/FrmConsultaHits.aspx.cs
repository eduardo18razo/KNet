using System;
using System.Collections.Generic;
using System.Linq;
using KiiniHelp.ServiceConsultas;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Consultas
{
    public partial class FrmConsultaHits : System.Web.UI.Page
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
                gvResult.DataSource = _servicioConsultas.ConsultarHits(((Usuario)Session["UserData"]).Id, UcFiltrosConsulta.FiltroGrupos, UcFiltrosConsulta.FiltroTipoUsuario, UcFiltrosConsulta.FiltroOrganizaciones,
                    UcFiltrosConsulta.FiltroUbicaciones, UcFiltrosConsulta.FiltroTipificaciones, UcFiltrosConsulta.FiltroVip, UcFiltrosConsulta.FiltroFechas, 0, 100000);
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