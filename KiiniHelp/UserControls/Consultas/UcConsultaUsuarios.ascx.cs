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
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
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
                UcDetalleUsuario1.OnCancelarModal += UcDetalleUsuario1OnOnCancelarModal;
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

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            //    _servicioUbicacion.HabilitarUbicacion(Convert.ToInt32(hfId.Value), false);
            //    LlenaUbicaciones();
            //}
            //catch (Exception ex)
            //{
            //    if (_lstError == null)
            //    {
            //        _lstError = new List<string>();
            //    }
            //    _lstError.Add(ex.Message);
            //    AlertaUbicacion = _lstError;
            //}
        }

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            //    _servicioUbicacion.HabilitarUbicacion(Convert.ToInt32(hfId.Value), true);
            //    LlenaUbicaciones();
            //}
            //catch (Exception ex)
            //{
            //    if (_lstError == null)
            //    {
            //        _lstError = new List<string>();
            //    }
            //    _lstError.Add(ex.Message);
            //    AlertaUbicacion = _lstError;
            //}
        }

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            //    int nivel = 0;
            //    string descripcion = null;
            //    Ubicacion org = _servicioUbicacion.ObtenerUbicacionById(Convert.ToInt32(hfId.Value));
            //    Session["UbicacionSeleccionada"] = org;
            //    lblTitleCatalogo.Text = ObtenerRuta(org, ref nivel, ref descripcion);
            //    txtDescripcionCatalogo.Text = descripcion;
            //    hfCatalogo.Value = nivel.ToString();
            //    hfAlta.Value = false.ToString();
            //    ddlTipoUsuarioCatalogo.SelectedValue = org.IdTipoUsuario.ToString();
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
            //}
            //catch (Exception ex)
            //{
            //    if (_lstError == null)
            //    {
            //        _lstError = new List<string>();
            //    }
            //    _lstError.Add(ex.Message);
            //    AlertaUbicacion = _lstError;
            //}
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            //    Button btn = (Button)sender;
            //    if (sender == null) return;
            //    lblTitleCatalogo.Text = ObtenerRuta(btn.CommandArgument, btn.CommandName.ToUpper());
            //    hfCatalogo.Value = btn.CommandArgument;
            //    hfAlta.Value = true.ToString();
            //    ddlTipoUsuarioCatalogo.SelectedValue = IdTipoUsuario.ToString();
            //    ValidaSeleccion(btn.CommandArgument);
            //    if (btn.CommandArgument == "0")
            //    {
            //        ddlTipoUsuarioCampus.SelectedValue = IdTipoUsuario.ToString();
            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCampus\");", true);
            //    }
            //    else
            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
            //}
            //catch (Exception ex)
            //{
            //    if (_lstError == null)
            //    {
            //        _lstError = new List<string>();
            //    }
            //    _lstError.Add(ex.Message);
            //    AlertaUbicacion = _lstError;
            //}
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