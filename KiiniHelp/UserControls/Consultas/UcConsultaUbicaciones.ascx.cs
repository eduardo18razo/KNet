using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceUbicacion;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaUbicaciones : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        private readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        private readonly ServiceUbicacionClient _servicioUbicacion = new ServiceUbicacionClient();
        private readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();

        private List<string> _lstError = new List<string>();

        private string AlertaSucces
        {
            set
            {
                if (value.Trim() != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "SuccsessAlert('Éxito: ','" + value + "');", true);
                }
            }
        }
        public string ModalName
        {
            set { hfModalName.Value = value; }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set
            {
                ddlTipoUsuario.SelectedValue = value.ToString();
                ddlTipoUsuario_OnSelectedIndexChanged(null, null);
                ddlTipoUsuario.Enabled = false;
            }
        }

        public List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }
        
        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                if (lstTipoUsuario.Count >= 2)
                    lstTipoUsuario.Insert(BusinessVariables.ComboBoxCatalogo.IndexTodos, new TipoUsuario { Id = BusinessVariables.ComboBoxCatalogo.ValueTodos, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionTodos });
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaUbicaciones()
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                List<Ubicacion> lstUbicaciones = _servicioUbicacion.ObtenerUbicaciones(idTipoUsuario, null, null, null, null, null, null, null);
                //if (Modal)
                //    lstUbicaciones = lstUbicaciones.Where(w => w.Habilitado == Modal).ToList();

                rptResultados.DataSource = lstUbicaciones;
                rptResultados.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LimpiarOrganizaciones()
        {
            try
            {
                rptResultados.DataSource = null;
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
                //lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                Alerta = new List<string>();
                ucAltaUbicaciones.OnAceptarModal += UcAltaUbicacionesOnOnAceptarModal;
                ucAltaUbicaciones.OnCancelarModal += UcAltaUbicacionesOnOnCancelarModal;
                ucAltaUbicaciones.OnTerminarModal += ucAltaUbicaciones_OnTerminarModal;
                if (!IsPostBack)
                {
                    LlenaCombos();
                    LlenaUbicaciones();                    
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
        
        private void UcAltaUbicacionesOnOnAceptarModal()
        {
            try
            {
                LlenaUbicaciones();
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

        private void UcAltaUbicacionesOnOnCancelarModal()
        {
            try
            {
                LlenaUbicaciones();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoUbicacion\");", true);
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

        private void ucAltaUbicaciones_OnTerminarModal()
        {
            try
            {
                LlenaUbicaciones();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoUbicacion\");", true);
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
                txtFiltroDecripcion.Text = string.Empty;
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    LimpiarOrganizaciones();
                    return;
                }
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexTodos)
                {
                    LlenaUbicaciones();
                }
                else if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                {
                    if (IdTipoUsuario == (int)BusinessVariables.EnumTiposUsuario.Empleado || IdTipoUsuario == (int)BusinessVariables.EnumTiposUsuario.Cliente || IdTipoUsuario == (int)BusinessVariables.EnumTiposUsuario.Proveedor)
                        LlenaUbicaciones();
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
        
        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaUbicaciones.IdUbicacion = int.Parse(((Button)sender).CommandArgument);
                ucAltaUbicaciones.EsSeleccion = false;
                ucAltaUbicaciones.EsAlta = false;
                ucAltaUbicaciones.Title = "Editar Ubicación";
                ucAltaUbicaciones.SetUbicacionActualizar();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
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
                ucAltaUbicaciones.EsSeleccion = false;
                ucAltaUbicaciones.EsAlta = true;
                ucAltaUbicaciones.Title = "Nueva Ubicación";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
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
        
        protected void rptResultados_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        List<AliasUbicacion> alias = _servicioParametros.ObtenerAliasUbicacion(IdTipoUsuario);
                        if (alias.Count != 7) return;
                        ((Label)e.Item.FindControl("lblNivel1")).Text = alias.Single(s => s.Nivel == 1).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel2")).Text = alias.Single(s => s.Nivel == 2).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel3")).Text = alias.Single(s => s.Nivel == 3).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel4")).Text = alias.Single(s => s.Nivel == 4).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel5")).Text = alias.Single(s => s.Nivel == 5).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel6")).Text = alias.Single(s => s.Nivel == 6).Descripcion;
                        ((Label)e.Item.FindControl("lblNivel7")).Text = alias.Single(s => s.Nivel == 7).Descripcion;
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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                rptResultados.DataSource = _servicioUbicacion.BuscarPorPalabra(idTipoUsuario, null, null, null, null, null, null, null, txtFiltroDecripcion.Text.Trim());
                rptResultados.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "HightSearch(\"tblHeader\", \"" + txtFiltroDecripcion.Text.Trim() + "\");", true);
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

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _servicioUbicacion.HabilitarUbicacion(int.Parse(((CheckBox)sender).Attributes["data-id"]), ((CheckBox)sender).Checked);
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
            finally { LlenaUbicaciones(); }
        }
    }
}