using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceArea;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Operacion;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaAreas : UserControl
    {
        private readonly ServiceAreaClient _servicioAreas = new ServiceAreaClient();

        private List<string> _lstError = new List<string>();

        public List<string> Alerta
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }
        private void LlenaAreasConsulta(string filtro = "")
        {
            try
            {
                List<Area> areas = _servicioAreas.ObtenerAreaConsulta(txtFiltro.Text.Trim());
                if (filtro != string.Empty)
                    areas = areas.Where(w => w.Descripcion.Contains(filtro)).ToList();
                rptResultados.DataSource = areas;
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
                ucAltaArea.OnAceptarModal += AltaAreaOnAceptarModal;
                ucAltaArea.OnCancelarModal += AltaAreaOnCancelarModal;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        private void AltaAreaOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaArea\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        private void AltaAreaOnAceptarModal()
        {
            try
            {
                LlenaAreasConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaArea\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioAreas.Habilitar(Convert.ToInt32(hfId.Value), false);
                LlenaAreasConsulta();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioAreas.Habilitar(Convert.ToInt32(hfId.Value), true);
                LlenaAreasConsulta();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaArea.EsAlta = false;
                Area puesto = _servicioAreas.ObtenerAreaById(Convert.ToInt32(hfId.Value));
                if (puesto == null) return;
                ucAltaArea.IdArea = puesto.Id;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaArea\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaArea.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaArea\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LlenaAreasConsulta(txtFiltro.Text.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
    }
}
