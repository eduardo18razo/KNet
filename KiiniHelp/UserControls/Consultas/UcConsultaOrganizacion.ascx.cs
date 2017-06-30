﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceOrganizacion;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;
using Page = System.Web.UI.Page;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaOrganizacion : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        public delegate void DelegateSeleccionOrganizacionModal();

        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceOrganizacionClient _servicioOrganizacion = new ServiceOrganizacionClient();
        readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();
        private List<string> _lstError = new List<string>();
        private int PageSize = 10;

        public bool Modal
        {
            get { return Convert.ToBoolean(hfModal.Value); }
            set
            {
                hfModal.Value = value.ToString();
                lblTitleOrganizacion.Text = value ? "Agregar Organización" : "Organizaciones";
            }
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
        public string ModalName
        {
            set { hfModalName.Value = value; }
        }
        public List<string> AlertaOrganizacion
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

        public int OrganizacionSeleccionada
        {
            get
            {
                if (hfIdSeleccion.Value == null || hfIdSeleccion.Value.Trim() == string.Empty)
                    throw new Exception("Debe Seleccionar una organización");
                return Convert.ToInt32(hfIdSeleccion.Value);
            }
            set
            {
                LlenaOrganizaciones();
                foreach (RepeaterItem item in rptResultados.Items)
                {
                    if ((((DataBoundLiteralControl)item.Controls[0])).Text.Split('\n')[1].Contains("id='" + value + "'"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Scripts", "SeleccionaOrganizacion(\"" + value + "\");", true);
                }
                hfIdSeleccion.Value = value.ToString();
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)(recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.LlenaOrganizaciones();
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

        private void LlenaOrganizaciones()
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                List<Organizacion> lstOrganizaciones = _servicioOrganizacion.ObtenerOrganizaciones(idTipoUsuario, null, null, null, null, null, null, null);
                if (Modal)
                    lstOrganizaciones = lstOrganizaciones.Where(w => w.Habilitado == Modal).ToList();
                rptResultados.DataSource = lstOrganizaciones;
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
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                AlertaOrganizacion = new List<string>();
                ucAltaOrganizaciones.OnAceptarModal += UcAltaOrganizacionesOnOnAceptarModal;
                ucAltaOrganizaciones.OnCancelarModal += UcAltaOrganizacionesOnOnCancelarModal;
                ucAltaOrganizaciones.OnTerminarModal += UcAltaOrganizacionesOnTerminarModal;
                if (!IsPostBack)
                {
                    LlenaCombos();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        private void UcAltaOrganizacionesOnOnAceptarModal()
        {
            try
            {
                LlenaOrganizaciones();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        private void UcAltaOrganizacionesOnOnCancelarModal()
        {
            try
            {
                LlenaOrganizaciones();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        private void UcAltaOrganizacionesOnTerminarModal()
        {
            try
            {
                LlenaOrganizaciones();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
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

                LlenaOrganizaciones();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaOrganizaciones.IdOrganizacion = int.Parse(((Button)sender).CommandArgument);
                ucAltaOrganizaciones.EsSeleccion = false; 
                ucAltaOrganizaciones.EsAlta = false;
                ucAltaOrganizaciones.Title = "EDITAR ORGANIZACIÓN";
                ucAltaOrganizaciones.SetOrganizacionActualizar();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaOrganizaciones.EsSeleccion = false;
                ucAltaOrganizaciones.EsAlta = true;
                ucAltaOrganizaciones.Title = "ALTA DE ORGANIZACIÓN";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {

        }
        protected void rptResultados_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        List<AliasOrganizacion> alias = _servicioParametros.ObtenerAliasOrganizacion(IdTipoUsuario);
                        if (alias.Count != 7) return;
                        ((Label)e.Item.FindControl("lblHolding")).Text = alias.Single(s => s.Nivel == 1).Descripcion;
                        ((Label)e.Item.FindControl("lblCompania")).Text = alias.Single(s => s.Nivel == 2).Descripcion;
                        ((Label)e.Item.FindControl("lblDireccion")).Text = alias.Single(s => s.Nivel == 3).Descripcion;
                        ((Label)e.Item.FindControl("lblSubDireccion")).Text = alias.Single(s => s.Nivel == 4).Descripcion;
                        ((Label)e.Item.FindControl("lblGerencia")).Text = alias.Single(s => s.Nivel == 5).Descripcion;
                        ((Label)e.Item.FindControl("lblSubGerencia")).Text = alias.Single(s => s.Nivel == 6).Descripcion;
                        ((Label)e.Item.FindControl("lblJefatura")).Text = alias.Single(s => s.Nivel == 7).Descripcion;
                    }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                rptResultados.DataSource = _servicioOrganizacion.BuscarPorPalabra(idTipoUsuario, null, null, null, null, null, null, null, txtFiltroDecripcion.Text.Trim());
                rptResultados.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _servicioOrganizacion.HabilitarOrganizacion(int.Parse(((CheckBox)sender).Attributes["data-id"]), ((CheckBox)sender).Checked);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
            finally { LlenaOrganizaciones(); }
        }
    }
}