using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceConsultas;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.Graficos
{
    public partial class FrmGraficaEncuestas : Page
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
                ucFiltrosParametrosGraficoEncuestas.OnAceptarModal += UcFiltrosParametrosGraficoEncuestasOnOnAceptarModal;
                ucFiltrosParametrosGraficoEncuestas.OnCancelarModal += UcFiltrosParametrosGraficoEncuestasOnOnCancelarModal;
                if(Convert.ToBoolean(hfGraficado.Value))
                    UcFiltrosParametrosGraficoEncuestasOnOnAceptarModal();
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

        private void UcFiltrosParametrosGraficoEncuestasOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroParametros\");", true);
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

        private void UcFiltrosParametrosGraficoEncuestasOnOnAceptarModal()
        {
            try
            {
                switch (ucFiltrosParametrosGraficoEncuestas.TipoGrafico)
                {
                    case "Geografico":
                        frameGeoCharts.Attributes.Add("src", ResolveUrl("FrmGeoChart.aspx?Data=" + _servicioConsultas.GraficarConsultaEncuestaGeografica(((Usuario)Session["UserData"]).Id,
                            ucFiltrosGraficasEncuestas.FiltroGrupos,
                            ucFiltrosGraficasEncuestas.FiltroTipoArbol,
                            ucFiltrosGraficasEncuestas.FiltroResponsables,
                            ucFiltrosGraficasEncuestas.FiltroEncuestas,
                            ucFiltrosGraficasEncuestas.FiltroAtendedores,
                            ucFiltrosGraficasEncuestas.FiltroFechas,
                            ucFiltrosGraficasEncuestas.FiltroTipoUsuario,
                            ucFiltrosGraficasEncuestas.FiltroPrioridad,
                            ucFiltrosGraficasEncuestas.FiltroSla,
                            ucFiltrosGraficasEncuestas.FiltroUbicaciones,
                            ucFiltrosGraficasEncuestas.FiltroOrganizaciones,
                            ucFiltrosGraficasEncuestas.FiltroVip,
                            ucFiltrosGraficasEncuestas.TipoPeriodo)));
                        frameGeoCharts.Visible = true;
                        cGrafico.Visible = false;
                        upGrafica.Visible = true;
                        break;
                    case "Linear":
                        BusinessGraficoStack.Encuestas.Linear.GenerarGrafica(cGrafico, _servicioConsultas.GraficarConsultaEncuesta(((Usuario)Session["UserData"]).Id,
                            ucFiltrosGraficasEncuestas.FiltroGrupos,
                            ucFiltrosGraficasEncuestas.FiltroTipoArbol,
                            ucFiltrosGraficasEncuestas.FiltroResponsables,
                            ucFiltrosGraficasEncuestas.FiltroEncuestas,
                            ucFiltrosGraficasEncuestas.FiltroAtendedores,
                            ucFiltrosGraficasEncuestas.FiltroFechas,
                            ucFiltrosGraficasEncuestas.FiltroTipoUsuario,
                            ucFiltrosGraficasEncuestas.FiltroPrioridad,
                            ucFiltrosGraficasEncuestas.FiltroSla,
                            ucFiltrosGraficasEncuestas.FiltroUbicaciones,
                            ucFiltrosGraficasEncuestas.FiltroOrganizaciones,
                            ucFiltrosGraficasEncuestas.FiltroVip,
                            ucFiltrosGraficasEncuestas.TipoPeriodo));
                        frameGeoCharts.Visible = false;
                        cGrafico.Visible = true;
                        upGrafica.Visible = true;
                        break;
                    case "Barra Comparativa":
                        BusinessGraficoStack.Encuestas.Columns.GenerarGrafica(cGrafico, _servicioConsultas.GraficarConsultaEncuesta(((Usuario)Session["UserData"]).Id,
                            ucFiltrosGraficasEncuestas.FiltroGrupos,
                            ucFiltrosGraficasEncuestas.FiltroTipoArbol,
                            ucFiltrosGraficasEncuestas.FiltroResponsables,
                            ucFiltrosGraficasEncuestas.FiltroEncuestas,
                            ucFiltrosGraficasEncuestas.FiltroAtendedores,
                            ucFiltrosGraficasEncuestas.FiltroFechas,
                            ucFiltrosGraficasEncuestas.FiltroTipoUsuario,
                            ucFiltrosGraficasEncuestas.FiltroPrioridad,
                            ucFiltrosGraficasEncuestas.FiltroSla,
                            ucFiltrosGraficasEncuestas.FiltroUbicaciones,
                            ucFiltrosGraficasEncuestas.FiltroOrganizaciones,
                            ucFiltrosGraficasEncuestas.FiltroVip,
                            ucFiltrosGraficasEncuestas.TipoPeriodo));
                        frameGeoCharts.Visible = false;
                        cGrafico.Visible = true;
                        upGrafica.Visible = true;
                        break;
                }
                upGrafica.Update();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroParametros\");", true);
                hfGraficado.Value = true.ToString();
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
                List<HelperReportesTicket> lstConsulta =
                    _servicioConsultas.ConsultarEncuestas(((Usuario)Session["UserData"]).Id,
                        ucFiltrosGraficasEncuestas.FiltroGrupos, ucFiltrosGraficasEncuestas.FiltroTipoArbol,
                        ucFiltrosGraficasEncuestas.FiltroResponsables, ucFiltrosGraficasEncuestas.FiltroEncuestas,
                        ucFiltrosGraficasEncuestas.FiltroAtendedores, ucFiltrosGraficasEncuestas.FiltroFechas,
                        ucFiltrosGraficasEncuestas.FiltroTipoUsuario, ucFiltrosGraficasEncuestas.FiltroPrioridad,
                        ucFiltrosGraficasEncuestas.FiltroSla, ucFiltrosGraficasEncuestas.FiltroUbicaciones,
                        ucFiltrosGraficasEncuestas.FiltroOrganizaciones, ucFiltrosGraficasEncuestas.FiltroVip, 0, 1000);
                if (lstConsulta == null) return;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroParametros\");", true);
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