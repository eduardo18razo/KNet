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
                AlertaGeneral = new List<string>();
                UcFiltrosConsulta.OnAceptarModal += UcFiltrosConsulta_OnAceptarModal;
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

        private void UcFiltrosConsulta_OnAceptarModal()
        {
            try
            {
                List<HelperHits> lstHits = _servicioConsultas.ConsultarHits(((Usuario)Session["UserData"]).Id, UcFiltrosConsulta.FiltroGrupos, UcFiltrosConsulta.FiltroTipoUsuario, UcFiltrosConsulta.FiltroOrganizaciones, UcFiltrosConsulta.FiltroUbicaciones, UcFiltrosConsulta.FiltroTipificaciones, UcFiltrosConsulta.FiltroVip, UcFiltrosConsulta.FiltroFechas, 0, 100000);

                if (lstHits != null)
                {
                    gvResult.DataSource = lstHits.Select(s => new { s.IdHit, s.Tipificacion, s.TipoServicio, s.NombreUsuario, s.Ubicacion, s.Organizacion, s.FechaHora, s.Total }).ToList();
                    gvResult.DataBind();
                    pnlAlertaGral.Update();
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

        //protected void btnConsultar_OnClick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        List<HelperHits> lstHits = _servicioConsultas.ConsultarHits(((Usuario)Session["UserData"]).Id, UcFiltrosConsulta.FiltroGrupos, UcFiltrosConsulta.FiltroTipoUsuario, UcFiltrosConsulta.FiltroOrganizaciones,UcFiltrosConsulta.FiltroUbicaciones, UcFiltrosConsulta.FiltroTipificaciones, UcFiltrosConsulta.FiltroVip, UcFiltrosConsulta.FiltroFechas, 0, 100000);

        //        if (lstHits != null)
        //        {
        //            gvResult.DataSource = lstHits.Select(s => new { s.IdHit, s.Tipificacion, s.TipoServicio, s.NombreUsuario ,s.Ubicacion, s.Organizacion, s.FechaHora, s.Total }).ToList();
        //            gvResult.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_lstError == null)
        //        {
        //            _lstError = new List<string>();
        //        }
        //        _lstError.Add(ex.Message);
        //        AlertaGeneral = _lstError;
        //    }
        //}
    }
}