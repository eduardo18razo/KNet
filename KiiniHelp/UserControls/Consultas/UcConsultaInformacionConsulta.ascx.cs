using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniHelp.ServiceSistemaTipoInformacionConsulta;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaInformacionConsulta : UserControl
    {
        private readonly ServiceTipoInfConsultaClient _servicioSistemaTipoInformacion = new ServiceTipoInfConsultaClient();
        private readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();

        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        
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

        private void LlenaCombos()
        {
            try
            {
                List<TipoInfConsulta> lstTipoInformacionConsultas = _servicioSistemaTipoInformacion.ObtenerTipoInformacionConsulta(true);
                Metodos.LlenaComboCatalogo(ddlTipoInformacion, lstTipoInformacionConsultas);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaInformacionConsulta()
        {
            try
            {
                int? idTipoInfotmacion = null;
                if (ddlTipoInformacion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoInfotmacion = int.Parse(ddlTipoInformacion.SelectedValue);

                rptResultados.DataSource = _servicioInformacionConsulta.ObtenerConsulta(idTipoInfotmacion, null);
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Alerta = new List<string>();
            if (!IsPostBack)
            {
                LlenaCombos();
                LlenaInformacionConsulta();
            }
            AltaInformacionConsulta.OnAceptarModal += AltaInformacionConsultaOnAceptarModal;
            AltaInformacionConsulta.OnCancelarModal += AltaInformacionConsultaOnCancelarModal;
        }

        private void AltaInformacionConsultaOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaInformacion\");", true);
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

        private void AltaInformacionConsultaOnAceptarModal()
        {
            try
            {
                LlenaInformacionConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaInformacion\");", true);
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

        protected void ddlTipoInformacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaInformacionConsulta();
                if (ddlTipoInformacion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.Text = "Agregar Información";
                }
                else
                {
                    btnNew.Visible = false;
                }
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
                _servicioInformacionConsulta.HabilitarInformacion(Convert.ToInt32(hfId.Value), false);
                LlenaInformacionConsulta();
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
                _servicioInformacionConsulta.HabilitarInformacion(Convert.ToInt32(hfId.Value), true);
                LlenaInformacionConsulta();
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
                AltaInformacionConsulta.EsAlta = false;
                AltaInformacionConsulta.IdInformacionConsulta = Convert.ToInt32(hfId.Value);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaInformacion\");", true);
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
                AltaInformacionConsulta.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaInformacion\");", true);
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