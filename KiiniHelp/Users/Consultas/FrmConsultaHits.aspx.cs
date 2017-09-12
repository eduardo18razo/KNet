using System;
using System.Collections.Generic;
using System.Linq;
using KiiniHelp.ServiceConsultas;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Helper;

namespace KiiniHelp.Users.Consultas
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
                                                            
                                     var _lstError = _servicioConsultas.ConsultarHits(((Usuario)Session["UserData"]).Id, UcFiltrosConsulta.FiltroGrupos, UcFiltrosConsulta.FiltroTipoUsuario, UcFiltrosConsulta.FiltroOrganizaciones,
                    UcFiltrosConsulta.FiltroUbicaciones, UcFiltrosConsulta.FiltroTipificaciones, UcFiltrosConsulta.FiltroVip, UcFiltrosConsulta.FiltroFechas, 0, 100000);

                gvResult.DataSource = _lstError.Select(s => new { s.IdHit, s.Tipificacion, s.TipoServicio, s.NombreUsuario, s.Ubicacion, s.Organizacion, s.FechaHora, s.Total }).ToList();
                gvResult.DataBind();
                //gvResult.Columns[1].Visible = false;
                //gvResult.Columns[2].Visible = false;
                //gvResult.Columns[3].Visible = false;
                //gvResult.Columns[4].Visible = false;
                //gvResult.Columns[5].Visible = false;
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