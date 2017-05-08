using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                List<TipoUsuario> lstTipoUsuario = _servicioTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                if (lstTipoUsuario.Count >= 2)
                    lstTipoUsuario.Insert(BusinessVariables.ComboBoxCatalogo.IndexTodos, new TipoUsuario { Id = BusinessVariables.ComboBoxCatalogo.ValueTodos, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionTodos });
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
                string filtro = txtFiltro.Text.Trim().ToUpper();
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                List<Puesto> ptos = _servicioPuestos.ObtenerPuestoConsulta(idTipoUsuario);
                if (filtro != string.Empty)
                    ptos = ptos.Where(w => w.Descripcion.Contains(filtro)).ToList();
                rptResultados.DataSource = ptos;
                rptResultados.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LimpiarPuestos()
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
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    LlenaCombos();
                }
                ucAltaPuesto.OnAceptarModal += AltaPuestoOnAceptarModal;
                ucAltaPuesto.OnCancelarModal += AltaPuestoOnCancelarModal;
                ucAltaPuesto.OnTerminarModal += UcAltaPuestoOnOnTerminarModal;
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

        private void UcAltaPuestoOnOnTerminarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaPuesto\");", true);
            }
            catch (Exception)
            {

                throw;
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
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    LimpiarPuestos();
                    return;
                }
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
                Button btn = (Button)sender;
                ucAltaPuesto.EsAlta = false;
                Puesto puesto = _servicioPuestos.ObtenerPuestoById(int.Parse(btn.CommandArgument));
                if (puesto == null) return;
                ddlTipoUsuario.SelectedValue = puesto.IdTipoUsuario.ToString();
                ddlTipoUsuario_OnSelectedIndexChanged(ddlTipoUsuario, null);
                ucAltaPuesto.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ucAltaPuesto.IdPuesto = puesto.Id;
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
        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
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

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _servicioPuestos.Habilitar(int.Parse(((CheckBox)sender).Attributes["data-id"]), ((CheckBox)sender).Checked);
                LlenaPuestosConsulta();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
