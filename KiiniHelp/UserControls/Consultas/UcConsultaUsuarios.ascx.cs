using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaUsuarios : UserControl
    {
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        private List<string> _lstError = new List<string>();

        public List<string> AlertaGeneral
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
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaUsuarios()
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                    btnNew.Visible = true;
                }
                else
                    btnNew.Visible = false;
                rptResultados.DataSource = _servicioUsuarios.ObtenerUsuarios(idTipoUsuario);
                rptResultados.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UcDetalleUsuario1.FromModal = true;
                UcDetalleUsuario1.OnCancelarModal += UcDetalleUsuario1OnOnCancelarModal;
                UcAltaUsuario.OnAceptarModal += UcAltaUsuario_OnAceptarModal;
                UcAltaUsuario.OnCancelarModal += UcAltaUsuario_OnCancelarModal;
                if (!IsPostBack)
                {
                    LlenaCombos();
                    LlenaUsuarios();
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

        void UcAltaUsuario_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editUser\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void UcAltaUsuario_OnAceptarModal()
        {
            try
            {
                LlenaUsuarios();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editUser\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UcDetalleUsuario1OnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDetalleUsuario\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnUsuario_OnClick(object sender, EventArgs e)
        {
            try
            {

                UcDetalleUsuario1.IdUsuario = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDetalleUsuario\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioUsuarios.HabilitarUsuario(Convert.ToInt32(hfId.Value), false);
                LlenaUsuarios();
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

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioUsuarios.HabilitarUsuario(Convert.ToInt32(hfId.Value), true);
                LlenaUsuarios();
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

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                UcAltaUsuario.IdUsuario = Convert.ToInt32(hfId.Value);
                UcAltaUsuario.Alta = false;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editUser\");", true);
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

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                UcAltaUsuario.Alta = true;
                UcAltaUsuario.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editUser\");", true);
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

        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaUsuarios();
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