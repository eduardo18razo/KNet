using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServicePuesto;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaPuestos : UserControl
    {
        private readonly ServicePuestoClient _servicioPuestos = new ServicePuestoClient();
        private readonly ServiceTipoUsuarioClient _servicioTipoUsuario = new ServiceTipoUsuarioClient();

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
                List<TipoUsuario> lstTipoUsuario = _servicioTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaPuestosConsulta()
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                rptResultados.DataSource = _servicioPuestos.ObtenerPuestoConsulta(idTipoUsuario);
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
                if (!IsPostBack)
                {
                    LlenaCombos();
                    LlenaPuestosConsulta();
                }
                ucAltaPuesto.OnAceptarModal += AltaPuestoOnAceptarModal;
                ucAltaPuesto.OnCancelarModal += AltaPuestoOnCancelarModal;
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

        private void AltaPuestoOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaPuesto\");", true);
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

        private void AltaPuestoOnAceptarModal()
        {
            try
            {
                LlenaPuestosConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaPuesto\");", true);
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

        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaPuestosConsulta();
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.Text = "Agregar Puesto";
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
                _servicioPuestos.Habilitar(Convert.ToInt32(hfId.Value), false);
                LlenaPuestosConsulta();
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
                _servicioPuestos.Habilitar(Convert.ToInt32(hfId.Value), true);
                LlenaPuestosConsulta();
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
                ucAltaPuesto.EsAlta = false;
                ucAltaPuesto.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ucAltaPuesto.IdPuesto = Convert.ToInt32(hfId.Value);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaPuesto\");", true);
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
                ucAltaPuesto.EsAlta = true;
                ucAltaPuesto.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaPuesto\");", true);
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
