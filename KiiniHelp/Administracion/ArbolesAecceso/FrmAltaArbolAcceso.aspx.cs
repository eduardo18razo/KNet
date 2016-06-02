using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceEncuesta;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.ServiceSistemaTipoArbolAcceso;
using KiiniHelp.ServiceSistemaTipoInformacionConsulta;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceSla;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;

namespace KiiniHelp.Administracion.ArbolesAecceso
{
    public partial class FrmAltaArbolAcceso : Page
    {
        #region Variables
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceTipoArbolAccesoClient _servicioSistemaTipoArbol = new ServiceTipoArbolAccesoClient();
        readonly ServiceTipoInfConsultaClient _servicioSistemaTipoInformacionConsulta = new ServiceTipoInfConsultaClient();
        readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        readonly ServiceSlaClient _servicioSla = new ServiceSlaClient();
        readonly ServiceEncuestaClient _servicioEncuesta = new ServiceEncuestaClient();
        readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();

        private List<string> _lstError = new List<string>();
        #endregion Variables

        #region Propiedades privadas
        private List<string> AlertaGeneral
        {
            set
            {
                panelAlert.Visible = value.Any();
                if (!panelAlert.Visible) return;
                rptHeaderError.DataSource = value;
                rptHeaderError.DataBind();
            }
        }
        private List<string> AlertaNivel
        {
            set
            {
                panelAlertaNivel.Visible = value.Any();
                if (!panelAlertaNivel.Visible) return;
                rptErrorNivel.DataSource = value;
                rptErrorNivel.DataBind();
            }
        }
        #endregion Propiedades privadas

        #region Metodos
        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioNivel, lstTipoUsuario);
                chkNivelTerminal_OnCheckedChanged(chkNivelTerminal, null);
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

        private void LimpiarNivel()
        {
            try
            {
                txtDescripcionNivel.Text = string.Empty;
                chkNivelTerminal.Checked = false;
                chkNivelTerminal_OnCheckedChanged(chkNivelTerminal, null);
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


        private void ValidaCapturaConsulta()
        {
            StringBuilder sb = new StringBuilder();
            if ((from RepeaterItem item in rptInformacion.Items select (CheckBox)item.FindControl("chkInfoConsulta")).Count(chk => chk.Checked) <= 0)
            {
                sb.AppendLine("<li>Debe especificar al menos un tipo de información de consulta.</li>");
            }
            DropDownList ddl = null;
            foreach (RepeaterItem item in rptInformacion.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkInfoConsulta");
                if (chk.Checked)
                {
                    Label lbl = (Label)item.FindControl("lblIdTipoInformacion");
                    switch (int.Parse(lbl.Text))
                    {
                        case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                            ddl = (DropDownList)item.FindControl("ddlPropietario");
                            break;
                        case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                            ddl = (DropDownList)item.FindControl("ddlDocumento");
                            break;
                        case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                            ddl = (DropDownList)item.FindControl("ddlUrl");
                            break;
                    }
                    if (ddl != null && ddl.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                        sb.AppendLine("<li>Seleccioine alguna información de consulta.</li>");
                }

            }
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Consulta</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void ValidaCapturaTicket()
        {
            StringBuilder sb = new StringBuilder();
            if (ddlMascaraAcceso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                sb.AppendLine("<li>Debe especificar una mascara de captura.</li>");
            if (ddlSla.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                sb.AppendLine("<li>Debe especificar un SLA.</li>");
            if (ddlEncuesta.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                sb.AppendLine("<li>Debe especificar una encuesta.</li>");
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Ticket</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void ValidaCapturaGrupos()
        {
            StringBuilder sb = new StringBuilder();
            if (!AsociarGrupoUsuario.ValidaCapturaGrupos())
                sb.AppendLine("<li>Debe asignar al menos un Grupo.</li>");
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Grupos</h3>");
                throw new Exception(sb.ToString());
            }
        }




        #endregion Metodos

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                AlertaNivel = new List<string>();
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
                AlertaGeneral = _lstError;
            }

        }

        protected void OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                    throw new Exception("Seleccione un tipo de usuario");
                }
                if (ddlTipoArbol.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                    throw new Exception("Seleccione un tipo de arbol");
                }
                Button lbtn = (Button)sender;
                if (sender == null) return;
                switch (lbtn.CommandArgument)
                {
                    case "2":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        break;
                    case "3":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 2");
                        }
                        break;
                    case "4":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 3");
                        }
                        break;
                    case "5":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 4");
                        }
                        break;
                    case "6":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 4");
                        }
                        if (ddlNivel5.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 5");
                        }
                        break;
                    case "7":
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 4");
                        }
                        if (ddlNivel5.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 5");
                        }
                        if (ddlNivel6.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                            throw new Exception("Seleccione nivel 6");
                        }
                        break;
                }
                lblTitleCatalogo.Text = lbtn.CommandName;
                hfCatalogo.Value = lbtn.CommandArgument;
                ddlTipoUsuarioNivel.SelectedIndex = ddlTipoUsuario.SelectedIndex;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editNivel\");", true);

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

        #region Seleccion Arbol
        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlTipoArbol);
                Metodos.LimpiarCombo(ddlNivel1);
                Metodos.LimpiarCombo(ddlNivel2);
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                if (ddlTipoUsuario.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                {
                    Metodos.LimpiarCombo(ddlTipoArbol);
                    Metodos.LimpiarCombo(ddlNivel1);
                    Metodos.LimpiarCombo(ddlNivel2);
                    Metodos.LimpiarCombo(ddlNivel3);
                    Metodos.LimpiarCombo(ddlNivel4);
                    Metodos.LimpiarCombo(ddlNivel5);
                    Metodos.LimpiarCombo(ddlNivel6);
                    Metodos.LimpiarCombo(ddlNivel7);
                    Metodos.LlenaComboCatalogo(ddlTipoArbol, _servicioSistemaTipoArbol.ObtenerTiposArbolAcceso(true));
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
        protected void ddlTipoArbol_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel1);
                Metodos.LimpiarCombo(ddlNivel2);
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                Metodos.LlenaComboCatalogo(ddlNivel1, _servicioArbolAcceso.ObtenerNivel1(idTipoArbol, idTipoUsuario, true));

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

        protected void ddlNivel1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel2);
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel1.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, idNivelFiltro, null, null, null, null, null, null))
                    Metodos.FiltraCombo(ddlNivel1, ddlNivel2, _servicioArbolAcceso.ObtenerNivel2(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel2.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), idNivelFiltro, null, null, null, null, null))
                    Metodos.FiltraCombo(ddlNivel2, ddlNivel3, _servicioArbolAcceso.ObtenerNivel3(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel3.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), null, null, null, null))
                    Metodos.FiltraCombo(ddlNivel3, ddlNivel4, _servicioArbolAcceso.ObtenerNivel4(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel4.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), idNivelFiltro, null, null, null))
                    Metodos.FiltraCombo(ddlNivel4, ddlNivel5, _servicioArbolAcceso.ObtenerNivel5(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel5_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel5.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), Convert.ToInt32(ddlNivel4.SelectedValue), idNivelFiltro, null, null))
                    Metodos.FiltraCombo(ddlNivel5, ddlNivel6, _servicioArbolAcceso.ObtenerNivel6(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel6_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel6.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), Convert.ToInt32(ddlNivel4.SelectedValue), Convert.ToInt32(ddlNivel5.SelectedValue), idNivelFiltro, null))
                    Metodos.FiltraCombo(ddlNivel6, ddlNivel7, _servicioArbolAcceso.ObtenerNivel7(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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
        #endregion Seleccion Arbol

        protected void btnGuardarNivel_OnClick(object sender, EventArgs e)
        {
            try
            {

                if (Metodos.ValidaCapturaCatalogo(txtDescripcionNivel.Text))
                {
                    if (!chkHerencia.Checked)
                        AsociarGrupoUsuario.ValidaCapturaGrupos();
                    int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                    int idTipoArbolAcceso = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                    ArbolAcceso arbol = new ArbolAcceso
                    {
                        IdTipoUsuario = idTipoUsuario,
                        IdTipoArbolAcceso = idTipoArbolAcceso,
                        EsTerminal = chkNivelTerminal.Checked,
                        Habilitado = chkNivelHabilitado.Checked
                    };
                    arbol.InventarioArbolAcceso = new List<InventarioArbolAcceso> { new InventarioArbolAcceso() };
                    arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol = new List<GrupoUsuarioInventarioArbol>();
                    if (AsociarGrupoUsuario.GruposAsociados.Count > 0)
                    {
                        
                        

                        foreach (RepeaterItem item in AsociarGrupoUsuario.GruposAsociados)
                        {
                            Label lblIdGrupoUsuario = (Label) item.FindControl("lblIdGrupoUsuario");
                            Label lblIdRol = (Label) item.FindControl("lblIdTipoSubGrupo");
                            Label lblIdSubGrupoUsuario = (Label) item.FindControl("lblIdSubGrupo");
                            if (lblIdGrupoUsuario != null && lblIdRol != null && lblIdSubGrupoUsuario != null)
                            {
                                arbol.InventarioArbolAcceso[0].GrupoUsuarioInventarioArbol.Add(new GrupoUsuarioInventarioArbol
                                {
                                    IdGrupoUsuario = Convert.ToInt32(lblIdGrupoUsuario.Text),
                                    IdRol = Convert.ToInt32(lblIdRol.Text),
                                    IdSubGrupoUsuario =
                                        lblIdSubGrupoUsuario.Text.Trim() == string.Empty
                                            ? (int?) null
                                            : Convert.ToInt32(lblIdSubGrupoUsuario.Text)
                                });
                            }
                        }
                    }
                    if (chkNivelTerminal.Checked)
                    {
                        ValidaCapturaConsulta();
                        ValidaCapturaTicket();
                        ValidaCapturaGrupos();
                        arbol.InventarioArbolAcceso.First().IdMascara = Convert.ToInt32(ddlMascaraAcceso.SelectedValue);
                        arbol.InventarioArbolAcceso.First().IdSla = Convert.ToInt32(ddlSla.SelectedValue);
                        foreach (RepeaterItem item in AsociarGrupoUsuario.GruposAsociados)
                        {
                            Label lblIdGrupoUsuario = (Label)item.FindControl("lblIdGrupoUsuario");
                            Label lblIdRol = (Label)item.FindControl("lblIdTipoSubGrupo");
                            Label lblIdSubGrupoUsuario = (Label)item.FindControl("lblIdSubGrupo");
                            if (lblIdGrupoUsuario != null && lblIdRol != null && lblIdSubGrupoUsuario != null)
                            {
                                arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Add(new GrupoUsuarioInventarioArbol
                                {
                                    IdGrupoUsuario = Convert.ToInt32(lblIdGrupoUsuario.Text),
                                    IdRol = Convert.ToInt32(lblIdRol.Text),
                                    IdSubGrupoUsuario = lblIdSubGrupoUsuario.Text.Trim() == string.Empty ? (int?)null : Convert.ToInt32(lblIdSubGrupoUsuario.Text)
                                });
                            }
                        }
                        arbol.InventarioArbolAcceso.First().Descripcion = txtDescripcionNivel.Text.Trim();
                        arbol.InventarioArbolAcceso.First().IdEncuesta = Convert.ToInt32(ddlEncuesta.SelectedValue) == BusinessVariables.ComboBoxCatalogo.Value ? (int?)null : Convert.ToInt32(ddlEncuesta.SelectedValue);
                        arbol.InventarioArbolAcceso.First().InventarioInfConsulta = new List<InventarioInfConsulta>();
                        foreach (RepeaterItem item in rptInformacion.Items)
                        {
                            if (((CheckBox)item.FindControl("chkInfoConsulta")).Checked)
                            {
                                Label lblId = (Label)item.FindControl("lblIdTipoInformacion");
                                DropDownList ddl = null;
                                switch (Convert.ToInt32(lblId.Text))
                                {
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                                        ddl = (DropDownList)item.FindControl("ddlPropietario");
                                        break;
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                                        ddl = (DropDownList)item.FindControl("ddlDocumento");
                                        break;
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                                        ddl = (DropDownList)item.FindControl("ddlUrl");
                                        break;
                                }
                                arbol.InventarioArbolAcceso[0].InventarioInfConsulta.Add(new InventarioInfConsulta { IdInfConsulta = Convert.ToInt32(ddl.SelectedValue) });
                            }
                        }
                    }

                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 1:
                            arbol.Nivel1 = new Nivel1
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlTipoArbol_OnSelectedIndexChanged(ddlTipoArbol, null);
                            break;
                        case 2:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.Nivel2 = new Nivel2
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel1_OnSelectedIndexChanged(ddlNivel1, null);
                            break;
                        case 3:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                            arbol.Nivel3 = new Nivel3
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel2_OnSelectedIndexChanged(ddlNivel2, null);
                            break;
                        case 4:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                            arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                            arbol.Nivel4 = new Nivel4
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel3_OnSelectedIndexChanged(ddlNivel3, null);
                            break;
                        case 5:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                            arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                            arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                            arbol.Nivel5 = new Nivel5
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel4_OnSelectedIndexChanged(ddlNivel4, null);
                            break;
                        case 6:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                            arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                            arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                            arbol.IdNivel5 = Convert.ToInt32(ddlNivel5.SelectedValue);
                            arbol.Nivel6 = new Nivel6
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel5_OnSelectedIndexChanged(ddlNivel5, null);
                            break;
                        case 7:
                            arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                            arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                            arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                            arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                            arbol.IdNivel5 = Convert.ToInt32(ddlNivel5.SelectedValue);
                            arbol.IdNivel6 = Convert.ToInt32(ddlNivel6.SelectedValue);
                            arbol.Nivel7 = new Nivel7
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionNivel.Text.Trim(),
                                Habilitado = chkNivelHabilitado.Checked
                            };
                            _servicioArbolAcceso.GuardarArbol(arbol);
                            ddlNivel6_OnSelectedIndexChanged(ddlNivel6, null);
                            break;
                    }
                    LimpiarNivel();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }

        }

        protected void btnCancelarNivel_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarNivel();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editNivel\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnLimpiarNivel_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarNivel();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void chkNivelTerminal_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnModalConsultas.Visible = chkNivelTerminal.Checked;
                btnMocalTicket.Visible = chkNivelTerminal.Checked;
                //Grupos
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.Acceso, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeOperación, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeAtención, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.EspecialDeConsulta, true);
                AsociarGrupoUsuario.Limpiar();

                if (!chkNivelTerminal.Checked) return;


                //Información de Consulta
                List<InformacionConsulta> infoCons = _servicioSistemaTipoInformacionConsulta.ObtenerTipoInformacionConsulta(false).Select(tipoInf => new InformacionConsulta { TipoInfConsulta = tipoInf }).ToList();
                rptInformacion.DataSource = infoCons;
                rptInformacion.DataBind();

                //Ticket
                Metodos.LlenaComboCatalogo(ddlMascaraAcceso, _servicioMascaras.ObtenerMascarasAcceso(true));
                Metodos.LlenaComboCatalogo(ddlSla, _servicioSla.ObtenerSla(true));
                Metodos.LlenaComboCatalogo(ddlEncuesta, _servicioEncuesta.ObtenerEncuestas(true));

                upGrupos.Update();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }

        }

        protected void rptInformacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DropDownList ddlPropietario = (DropDownList)e.Item.FindControl("ddlPropietario");
                DropDownList ddlDocumento = (DropDownList)e.Item.FindControl("ddlDocumento");
                DropDownList ddlUrl = (DropDownList)e.Item.FindControl("ddlUrl");
                if (ddlPropietario == null && ddlDocumento == null && ddlUrl == null) return;
                Metodos.LlenaComboCatalogo(ddlPropietario, _servicioInformacionConsulta.ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta.Texto, true));
                Metodos.LlenaComboCatalogo(ddlDocumento, _servicioInformacionConsulta.ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta.Documento, true));
                Metodos.LlenaComboCatalogo(ddlUrl, _servicioInformacionConsulta.ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void chkInfoConsulta_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl;
                Button btn;
                CheckBox chk = (CheckBox)sender;
                if (chk == null) return;
                BusinessVariables.EnumTiposInformacionConsulta seleccion = Metodos.Enumeradores.GetStringEnum<BusinessVariables.EnumTiposInformacionConsulta>(chk.Text);
                switch (seleccion)
                {
                    case BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        ddl = (DropDownList)rptInformacion.Items[0].FindControl("ddlPropietario");
                        btn = (Button)rptInformacion.Items[0].FindControl("btnAgregarPropietario");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        ddl = (DropDownList)rptInformacion.Items[1].FindControl("ddlDocumento");
                        btn = (Button)rptInformacion.Items[1].FindControl("btnAgregarDocumento");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        ddl = (DropDownList)rptInformacion.Items[2].FindControl("ddlUrl");
                        btn = (Button)rptInformacion.Items[2].FindControl("btnAgregarUrl");
                        break;
                    default:
                        ddl = null;
                        btn = null;
                        break;
                }

                if (ddl == null) return;
                if (!chk.Checked)
                    ddl.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                ddl.Enabled = chk.Checked;
                btn.Enabled = chk.Checked;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        #endregion Eventos#endregion Eventos

        #region Cerrar Modales
        protected void btnCerraraltaInformacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                rptInformacion.DataSource = null;
                rptInformacion.DataBind();
                List<InformacionConsulta> infoCons = _servicioSistemaTipoInformacionConsulta.ObtenerTipoInformacionConsulta(false).Select(tipoInf => new InformacionConsulta { TipoInfConsulta = tipoInf }).ToList();
                rptInformacion.DataSource = infoCons;
                rptInformacion.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaInfCons\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarModalAltaMascara_OnClick(object sender, EventArgs e)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlMascaraAcceso, _servicioMascaras.ObtenerMascarasAcceso(true));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaMascara\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarAltaSla_OnClick(object sender, EventArgs e)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlSla, _servicioSla.ObtenerSla(true));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaSla\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarEncuesta_OnClick(object sender, EventArgs e)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlEncuesta, _servicioEncuesta.ObtenerEncuestas(true));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaEncuesta\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarModalAltaGrupoUsuario_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (!AsociarGrupoUsuario.ValidaCapturaGrupos()) return;
                btnModalGrupos.CssClass = "btn btn-success btn-lg";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaGrupoUsuario\");", true);
                upGrupos.Update();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarConsultas_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalConsultas\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerrarTicket_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalTicket\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }

        protected void btnCerraGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalGruposNodo\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }
        #endregion Cerrar Modales

        protected void chkHerencia_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AsociarGrupoUsuario.Limpiar();
                if (!chkHerencia.Checked)
                {
                    return;
                }
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idTipoArbolAcceso = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                ArbolAcceso arbol = new ArbolAcceso
                {
                    IdTipoUsuario = idTipoUsuario,
                    IdTipoArbolAcceso = idTipoArbolAcceso,
                    EsTerminal = chkNivelTerminal.Checked,
                    Habilitado = chkNivelHabilitado.Checked
                };
                switch (int.Parse(hfCatalogo.Value))
                {
                    case 1:
                        arbol.Nivel1 = new Nivel1
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, null, null, null, null, null, null, null);
                        break;
                    case 2:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.Nivel2 = new Nivel2
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, null, null, null, null, null, null);
                        break;
                    case 3:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                        arbol.Nivel3 = new Nivel3
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, null, null, null, null, null);
                        break;
                    case 4:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                        arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                        arbol.Nivel4 = new Nivel4
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, null, null, null, null);
                        break;
                    case 5:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                        arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                        arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                        arbol.Nivel5 = new Nivel5
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, null, null, null);
                        break;
                    case 6:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                        arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                        arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                        arbol.IdNivel5 = Convert.ToInt32(ddlNivel5.SelectedValue);
                        arbol.Nivel6 = new Nivel6
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, arbol.IdNivel5, null, null);
                        break;
                    case 7:
                        arbol.IdNivel1 = Convert.ToInt32(ddlNivel1.SelectedValue);
                        arbol.IdNivel2 = Convert.ToInt32(ddlNivel2.SelectedValue);
                        arbol.IdNivel3 = Convert.ToInt32(ddlNivel3.SelectedValue);
                        arbol.IdNivel4 = Convert.ToInt32(ddlNivel4.SelectedValue);
                        arbol.IdNivel5 = Convert.ToInt32(ddlNivel5.SelectedValue);
                        arbol.IdNivel6 = Convert.ToInt32(ddlNivel6.SelectedValue);
                        arbol.Nivel7 = new Nivel7
                        {
                            IdTipoUsuario = idTipoUsuario,
                            Descripcion = txtDescripcionNivel.Text.Trim(),
                            Habilitado = chkNivelHabilitado.Checked
                        };
                        AsociarGrupoUsuario.ObtenerGruposHerencia(idTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, arbol.IdNivel5, arbol.IdNivel6, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaNivel = _lstError;
            }
        }
    }
}

