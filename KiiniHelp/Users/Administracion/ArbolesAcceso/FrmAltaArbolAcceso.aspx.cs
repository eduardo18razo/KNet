using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceArea;
using KiiniHelp.ServiceEncuesta;
using KiiniHelp.ServiceGrupoUsuario;
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

namespace KiiniHelp.Users.Administracion.ArbolesAcceso
{
    public partial class FrmAltaArbolAcceso : Page
    {
        #region Variables
        private readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        private readonly ServiceTipoArbolAccesoClient _servicioSistemaTipoArbol = new ServiceTipoArbolAccesoClient();
        private readonly ServiceTipoInfConsultaClient _servicioSistemaTipoInformacionConsulta = new ServiceTipoInfConsultaClient();
        private readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        private readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        private readonly ServiceSlaClient _servicioSla = new ServiceSlaClient();
        private readonly ServiceEncuestaClient _servicioEncuesta = new ServiceEncuestaClient();
        private readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();
        private readonly ServiceAreaClient _servicioAreas = new ServiceAreaClient();
        private readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();

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
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll();", true);
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
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll();", true);
            }
        }
        private List<string> AlertaInfoConsulta
        {
            set
            {
                panelAlertaInfoConsulta.Visible = value.Any();
                if (!panelAlertaInfoConsulta.Visible) return;
                rptErrorInfoConsulta.DataSource = value;
                rptErrorInfoConsulta.DataBind();
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll();", true);
            }
        }
        private List<string> AlertaTicket
        {
            set
            {
                panelAlertaTicket.Visible = value.Any();
                if (!panelAlertaTicket.Visible) return;
                rptErrorTicket.DataSource = value;
                rptErrorTicket.DataBind();
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll();", true);
            }
        }
        #endregion Propiedades privadas

        #region Metodos

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlArea, _servicioAreas.ObtenerAreas(true));
                chkNivelTerminal_OnCheckedChanged(chkNivelTerminal, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ValidaCapturaConsulta()
        {
            try
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
                            case (int)BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ValidaCapturaTicket()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (ddlMascaraAcceso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                    sb.AppendLine("<li>Debe especificar un Formulario de Cliente.</li>");

                if (sb.ToString() != string.Empty)
                {
                    sb.Append("</ul>");
                    sb.Insert(0, "<ul>");
                    sb.Insert(0, "<h3>Ticket</h3>");
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ValidaCapturaEncuesta()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidaCapturaGrupos()
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void AgregarSubMenuOpcion(Button sender, bool esTerminal)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                {
                    throw new Exception("Seleccione un tipo de usuario");
                }
                if (ddlTipoArbol.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                {
                    throw new Exception("Seleccione un tipo de arbol");
                }
                Button lbtn = sender;
                int nivel = 7;
                switch (lbtn.CommandArgument)
                {
                    case "1":
                        nivel = 1;
                        break;
                    case "2":
                        nivel = 2;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        break;
                    case "3":
                        nivel = 3;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 2");
                        }
                        break;
                    case "4":
                        nivel = 4;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 3");
                        }
                        break;
                    case "5":
                        nivel = 5;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 4");
                        }
                        break;
                    case "6":
                        nivel = 6;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 4");
                        }
                        if (ddlNivel5.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 5");
                        }
                        break;
                    case "7":
                        nivel = 7;
                        if (ddlNivel1.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 1");
                        }
                        if (ddlNivel2.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 2");
                        }
                        if (ddlNivel3.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 3");
                        }
                        if (ddlNivel4.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 4");
                        }
                        if (ddlNivel5.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 5");
                        }
                        if (ddlNivel6.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                        {
                            throw new Exception("Seleccione SubMenu/Opcion 6");
                        }
                        break;
                }
                string sTitle = string.Empty;
                switch (nivel)
                {
                    case 1:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">";
                        break;
                    case 2:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + (esTerminal ? ddlNivel2.SelectedItem.Text + ">" : ">");
                        break;
                    case 3:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + ddlNivel2.SelectedItem.Text + ">" + (esTerminal ? ddlNivel3.SelectedItem.Text + ">" : ">");
                        break;
                    case 4:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + ddlNivel2.SelectedItem.Text + ">" + ddlNivel3.SelectedItem.Text + ">" + (esTerminal ? ddlNivel4.SelectedItem.Text + ">" : ">");
                        break;
                    case 5:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + ddlNivel2.SelectedItem.Text + ">" + ddlNivel3.SelectedItem.Text + ">" + ddlNivel4.SelectedItem.Text + ">" + (esTerminal ? ddlNivel5.SelectedItem.Text + ">" : ">");
                        break;
                    case 6:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + ddlNivel2.SelectedItem.Text + ">" + ddlNivel3.SelectedItem.Text + ">" + ddlNivel4.SelectedItem.Text + ">" + ddlNivel5.SelectedItem.Text + ">" + (esTerminal ? ddlNivel6.SelectedItem.Text + ">" : ">");
                        break;
                    case 7:
                        sTitle = ddlTipoArbol.SelectedItem.Text + ">" + ddlNivel1.SelectedItem.Text + ">" + ddlNivel2.SelectedItem.Text + ">" + ddlNivel3.SelectedItem.Text + ">" + ddlNivel4.SelectedItem.Text + ">" + ddlNivel5.SelectedItem.Text + ">" + ddlNivel6.SelectedItem.Text + ">";
                        break;
                    default:
                        throw new Exception("Error al intentar agregar. Intente Nuevamente");
                }
                sTitle += lbtn.CommandName;
                lblTitleCatalogo.Text = sTitle;
                hfCatalogo.Value = lbtn.CommandArgument;
                ddlTipoUsuarioNivel.SelectedIndex = ddlTipoUsuario.SelectedIndex;
                chkNivelTerminal.Checked = esTerminal;
                if (esTerminal)
                    chkNivelTerminal_OnCheckedChanged(chkNivelTerminal, null);
                divDatos.Visible = esTerminal;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editNivel\");", true);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion Metodos

        #region Delegados
        void AltaAreas_OnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAreas\");", true);
                LlenaCombos();
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

        void AltaAreas_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAreas\");", true);
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

        void UcAltaInformacionConsulta_OnAceptarModal()
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

        void UcAltaInformacionConsulta_OnCancelarModal()
        {
            try
            {
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

        void UcAltaMascaraAcceso_OnAceptarModal()
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

        void UcAltaMascaraAcceso_OnCancelarModal()
        {
            try
            {
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

        void UcSla_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSla\");", true);
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

        void UcSla_OnAceptarModal()
        {
            try
            {
                btnModalSla.CssClass = "btn btn-success";
                btnModalImpactoUrgencia.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSla\");", true);
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

        private void AltaTiempoEstimadoOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalTiempoInforme\");", true);
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

        private void AltaTiempoEstimadoOnOnAceptarModal()
        {
            try
            {
                btnModalInforme.CssClass = "btn btn-success";
                btnModalEncuesta.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalTiempoInforme\");", true);
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


        void UcEncuesta_OnAceptarModal()
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

        void UcEncuesta_OnCancelarModal()
        {
            try
            {
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

        #endregion Delegados

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                AlertaNivel = new List<string>();
                AlertaInfoConsulta = new List<string>();
                AltaAreas.OnCancelarModal += AltaAreas_OnCancelarModal;
                AltaAreas.OnAceptarModal += AltaAreas_OnAceptarModal;

                UcAltaInformacionConsulta.EsAlta = true;
                UcAltaInformacionConsulta.OnAceptarModal += UcAltaInformacionConsulta_OnAceptarModal;
                UcAltaInformacionConsulta.OnCancelarModal += UcAltaInformacionConsulta_OnCancelarModal;

                UcAltaMascaraAcceso.OnAceptarModal += UcAltaMascaraAcceso_OnAceptarModal;
                UcAltaMascaraAcceso.OnCancelarModal += UcAltaMascaraAcceso_OnCancelarModal;

                AsociarGrupoUsuario.AsignacionAutomatica = true;

                UcSla.FromModal = false;
                UcSla.OnAceptarModal += UcSla_OnAceptarModal;
                UcSla.OnCancelarModal += UcSla_OnCancelarModal;

                ucAltaTiempoEstimado.OnAceptarModal += AltaTiempoEstimadoOnOnAceptarModal;
                ucAltaTiempoEstimado.OnCancelarModal += AltaTiempoEstimadoOnOnCancelarModal;

                UcEncuesta.OnAceptarModal += UcEncuesta_OnAceptarModal;
                UcEncuesta.OnCancelarModal += UcEncuesta_OnCancelarModal;

                UcImpactoUrgencia.OnAceptarModal += UcImpactoUrgencia_OnAceptarModal;
                UcImpactoUrgencia.OnCancelarModal += UcImpactoUrgenciaOnOnCancelarModal;

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

        void UcImpactoUrgencia_OnAceptarModal()
        {
            try
            {
                btnModalImpactoUrgencia.CssClass = "btn btn-success";
                btnModalInforme.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalImpacto\");", true);
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

        private void UcImpactoUrgenciaOnOnCancelarModal()
        {
            try
            {
                btnModalImpactoUrgencia.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalImpacto\");", true);
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

        protected void OnClickNivelSubMenu(object sender, EventArgs e)
        {
            try
            {
                AgregarSubMenuOpcion((Button)sender, false);
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

        protected void OnClickNivelOpcion(object sender, EventArgs e)
        {
            try
            {
                AgregarSubMenuOpcion((Button)sender, true);
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
        protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlTipoUsuario);
                Metodos.LimpiarCombo(ddlTipoArbol);
                Metodos.LimpiarCombo(ddlNivel1);
                Metodos.LimpiarCombo(ddlNivel2);
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                btnAddMenu1.Enabled = false;
                btnAddOpti1.Enabled = false;
                btnAddMenu2.Enabled = false;
                btnAddOpti2.Enabled = false;
                btnAddMenu3.Enabled = false;
                btnAddOpti3.Enabled = false;
                btnAddMenu4.Enabled = false;
                btnAddOpti4.Enabled = false;
                btnAddMenu5.Enabled = false;
                btnAddOpti5.Enabled = false;
                btnAddMenu6.Enabled = false;
                btnAddOpti6.Enabled = false;
                btnAddOpti7.Enabled = false;
                if (ddlArea.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index) return;
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuario(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioNivel, lstTipoUsuario);
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

        #region MuestraModales
        protected void btnAddArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAreas\");", true);
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
        #endregion MuestraModales
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
                btnAddMenu1.Enabled = false;
                btnAddOpti1.Enabled = false;
                btnAddMenu2.Enabled = false;
                btnAddOpti2.Enabled = false;
                btnAddMenu3.Enabled = false;
                btnAddOpti3.Enabled = false;
                btnAddMenu4.Enabled = false;
                btnAddOpti4.Enabled = false;
                btnAddMenu5.Enabled = false;
                btnAddOpti5.Enabled = false;
                btnAddMenu6.Enabled = false;
                btnAddOpti6.Enabled = false;
                btnAddOpti7.Enabled = false;
                if (ddlTipoUsuario.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                {
                    Metodos.LlenaComboCatalogo(ddlTipoArbol, _servicioSistemaTipoArbol.ObtenerTiposArbolAcceso(true));
                    AsociarGrupoUsuario.IdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
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
                if (ddlTipoArbol.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnAddMenu1.Enabled = true;
                    btnAddOpti1.Enabled = true;
                    btnAddMenu2.Enabled = false;
                    btnAddOpti2.Enabled = false;
                    btnAddMenu3.Enabled = false;
                    btnAddOpti3.Enabled = false;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu1.Enabled = false;
                    btnAddOpti1.Enabled = false;
                    btnAddMenu2.Enabled = false;
                    btnAddOpti2.Enabled = false;
                    btnAddMenu3.Enabled = false;
                    btnAddOpti3.Enabled = false;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                switch (int.Parse(ddlTipoArbol.SelectedValue))
                {
                    case (int)BusinessVariables.EnumTipoArbol.Consultas:
                        btnModalConsultas.Visible = true;
                        btnModalTicket.Visible = false;
                        break;
                    default:
                        btnModalConsultas.Visible = false;
                        btnModalTicket.Visible = true;
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
                {
                    Metodos.FiltraCombo(ddlNivel1, ddlNivel2, _servicioArbolAcceso.ObtenerNivel2(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = false;
                    btnAddOpti3.Enabled = false;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu2.Enabled = false;
                    btnAddOpti2.Enabled = false;
                    btnAddMenu3.Enabled = false;
                    btnAddOpti3.Enabled = false;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
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
                {
                    Metodos.FiltraCombo(ddlNivel2, ddlNivel3, _servicioArbolAcceso.ObtenerNivel3(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = true;
                    btnAddOpti3.Enabled = true;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu3.Enabled = false;
                    btnAddOpti3.Enabled = false;
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
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
                {
                    Metodos.FiltraCombo(ddlNivel3, ddlNivel4, _servicioArbolAcceso.ObtenerNivel4(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = true;
                    btnAddOpti3.Enabled = true;
                    btnAddMenu4.Enabled = true;
                    btnAddOpti4.Enabled = true;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu4.Enabled = false;
                    btnAddOpti4.Enabled = false;
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
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
                {
                    Metodos.FiltraCombo(ddlNivel4, ddlNivel5, _servicioArbolAcceso.ObtenerNivel5(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = true;
                    btnAddOpti3.Enabled = true;
                    btnAddMenu4.Enabled = true;
                    btnAddOpti4.Enabled = true;
                    btnAddMenu5.Enabled = true;
                    btnAddOpti5.Enabled = true;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu5.Enabled = false;
                    btnAddOpti5.Enabled = false;
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
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
                {
                    Metodos.FiltraCombo(ddlNivel5, ddlNivel6, _servicioArbolAcceso.ObtenerNivel6(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = true;
                    btnAddOpti3.Enabled = true;
                    btnAddMenu4.Enabled = true;
                    btnAddOpti4.Enabled = true;
                    btnAddMenu5.Enabled = true;
                    btnAddOpti5.Enabled = true;
                    btnAddMenu6.Enabled = true;
                    btnAddOpti6.Enabled = true;
                    btnAddOpti7.Enabled = false;
                }
                else
                {
                    btnAddMenu6.Enabled = false;
                    btnAddOpti6.Enabled = false;
                    btnAddOpti7.Enabled = false;
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
        protected void ddlNivel6_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel6.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), Convert.ToInt32(ddlNivel4.SelectedValue), Convert.ToInt32(ddlNivel5.SelectedValue), idNivelFiltro, null))
                {
                    Metodos.FiltraCombo(ddlNivel6, ddlNivel7, _servicioArbolAcceso.ObtenerNivel7(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
                    btnAddMenu2.Enabled = true;
                    btnAddOpti2.Enabled = true;
                    btnAddMenu3.Enabled = true;
                    btnAddOpti3.Enabled = true;
                    btnAddMenu4.Enabled = true;
                    btnAddOpti4.Enabled = true;
                    btnAddMenu5.Enabled = true;
                    btnAddOpti5.Enabled = true;
                    btnAddMenu6.Enabled = true;
                    btnAddOpti6.Enabled = true;
                    btnAddOpti7.Enabled = true;
                }
                else
                {
                    btnAddOpti7.Enabled = false;
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
        #endregion Seleccion Arbol

        protected void btnGuardarNivel_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Metodos.ValidaCapturaCatalogo(txtDescripcionNivel.Text))
                {
                    AsociarGrupoUsuario.ValidaCapturaGrupos();
                    int idArea = Convert.ToInt32(ddlArea.SelectedValue);
                    int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                    int idTipoArbolAcceso = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                    ArbolAcceso arbol = new ArbolAcceso
                    {
                        IdArea = idArea,
                        IdTipoUsuario = idTipoUsuario,
                        IdTipoArbolAcceso = idTipoArbolAcceso,
                        EsTerminal = chkNivelTerminal.Checked,
                        Habilitado = chkNivelHabilitado.Checked
                    };
                    if (chkNivelTerminal.Checked)
                    {
                        if (int.Parse(ddlTipoArbol.SelectedValue) == (int) BusinessVariables.EnumTipoArbol.Consultas)
                        {
                            ValidaCapturaConsulta();
                            ValidaCapturaEncuesta();
                        }
                        if (int.Parse(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas)
                        {
                            ValidaCapturaTicket();
                            ValidaCapturaEncuesta();
                            arbol.IdImpacto = UcImpactoUrgencia.ObtenerImpactoUrgencia().Id;
                        }
                        ValidaCapturaGrupos();
                        arbol.InventarioArbolAcceso = new List<InventarioArbolAcceso> { new InventarioArbolAcceso() };
                        if (ddlTipoArbol.SelectedValue != ((int)BusinessVariables.EnumTipoArbol.Consultas).ToString())
                        {
                            arbol.InventarioArbolAcceso.First().IdMascara = Convert.ToInt32(ddlMascaraAcceso.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(ddlMascaraAcceso.SelectedValue);
                            arbol.InventarioArbolAcceso.First().Sla = UcSla.Sla;
                            arbol.InventarioArbolAcceso.First().IdEncuesta = Convert.ToInt32(ddlEncuesta.SelectedValue) == BusinessVariables.ComboBoxCatalogo.Value ? (int?)null : Convert.ToInt32(ddlEncuesta.SelectedValue);
                        }
                        arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol = new List<GrupoUsuarioInventarioArbol>();
                        arbol.TiempoInformeArbol = new List<TiempoInformeArbol>();
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
                                var gpo = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(Convert.ToInt32(lblIdGrupoUsuario.Text));
                                if (btnModalInforme.Visible)
                                    switch (gpo.IdTipoGrupo)
                                    {
                                        //TODO: AGREGAR GRUPO DE DUEÑO
                                        case (int)BusinessVariables.EnumTiposGrupos.DueñoDelServicio:
                                            arbol.TiempoInformeArbol.Add(new TiempoInformeArbol
                                            {
                                                IdTipoGrupo = gpo.IdTipoGrupo,
                                                IdGrupoUsuario = gpo.Id,
                                                Dias = ucAltaTiempoEstimado.TiempoDueño.Dias,
                                                Horas = ucAltaTiempoEstimado.TiempoDueño.Horas,
                                                Minutos = ucAltaTiempoEstimado.TiempoDueño.Minutos,
                                                Segundos = ucAltaTiempoEstimado.TiempoDueño.Segundos,
                                                TiempoNotificacion = ucAltaTiempoEstimado.TiempoDueño.TiempoNotificacion,
                                                IdTipoNotificacion = ucAltaTiempoEstimado.TiempoDueño.IdTipoNotificacion

                                            });
                                            break;
                                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeInformaciónPublicada:
                                            arbol.TiempoInformeArbol.Add(new TiempoInformeArbol
                                        {
                                            IdTipoGrupo = gpo.IdTipoGrupo,
                                            IdGrupoUsuario = gpo.Id,
                                            Dias = ucAltaTiempoEstimado.TiempoDueño.Dias,
                                            Horas = ucAltaTiempoEstimado.TiempoDueño.Horas,
                                            Minutos = ucAltaTiempoEstimado.TiempoDueño.Minutos,
                                            Segundos = ucAltaTiempoEstimado.TiempoDueño.Segundos,
                                            TiempoNotificacion = ucAltaTiempoEstimado.TiempoDueño.TiempoNotificacion,
                                            IdTipoNotificacion = ucAltaTiempoEstimado.TiempoDueño.IdTipoNotificacion

                                        });
                                            break;
                                        case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                                            arbol.TiempoInformeArbol.Add(new TiempoInformeArbol
                                        {
                                            IdTipoGrupo = gpo.IdTipoGrupo,
                                            IdGrupoUsuario = gpo.Id,
                                            Dias = ucAltaTiempoEstimado.TiempoDesarrollo.Dias,
                                            Horas = ucAltaTiempoEstimado.TiempoDesarrollo.Horas,
                                            Minutos = ucAltaTiempoEstimado.TiempoDesarrollo.Minutos,
                                            Segundos = ucAltaTiempoEstimado.TiempoDesarrollo.Segundos,
                                            TiempoNotificacion = ucAltaTiempoEstimado.TiempoDesarrollo.TiempoNotificacion,
                                            IdTipoNotificacion = ucAltaTiempoEstimado.TiempoDesarrollo.IdTipoNotificacion

                                        });
                                            break;
                                        case (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta:
                                            arbol.TiempoInformeArbol.Add(new TiempoInformeArbol
                                        {
                                            IdTipoGrupo = gpo.IdTipoGrupo,
                                            IdGrupoUsuario = gpo.Id,
                                            Dias = ucAltaTiempoEstimado.TiempoConsulta.Dias,
                                            Horas = ucAltaTiempoEstimado.TiempoConsulta.Horas,
                                            Minutos = ucAltaTiempoEstimado.TiempoConsulta.Minutos,
                                            Segundos = ucAltaTiempoEstimado.TiempoConsulta.Segundos,
                                            TiempoNotificacion = ucAltaTiempoEstimado.TiempoConsulta.TiempoNotificacion,
                                            IdTipoNotificacion = ucAltaTiempoEstimado.TiempoConsulta.IdTipoNotificacion

                                        });
                                            break;

                                    }
                            }
                        }
                        arbol.InventarioArbolAcceso.First().Descripcion = txtDescripcionNivel.Text.Trim();
                        arbol.InventarioArbolAcceso.First().InventarioInfConsulta = new List<InventarioInfConsulta>();
                        foreach (RepeaterItem item in rptInformacion.Items)
                        {
                            if (((CheckBox)item.FindControl("chkInfoConsulta")).Checked)
                            {
                                Label lblId = (Label)item.FindControl("lblIdTipoInformacion");
                                DropDownList ddl = null;
                                switch (Convert.ToInt32(lblId.Text))
                                {
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
                                        ddl = (DropDownList)item.FindControl("ddlPropietario");
                                        break;
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                                        ddl = (DropDownList)item.FindControl("ddlDocumento");
                                        break;
                                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                                        ddl = (DropDownList)item.FindControl("ddlUrl");
                                        break;
                                }
                                if (ddl != null)
                                    arbol.InventarioArbolAcceso.First().InventarioInfConsulta.Add(new InventarioInfConsulta { IdInfConsulta = Convert.ToInt32(ddl.SelectedValue) });
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

                btnModalConsultas.Visible = Convert.ToInt32(ddlTipoArbol.SelectedValue) == (int)BusinessVariables.EnumTipoArbol.Consultas;
                btnModalTicket.Visible = chkNivelTerminal.Checked && Convert.ToInt32(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas;
                btnModalSla.Visible = chkNivelTerminal.Checked && Convert.ToInt32(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas;
                btnModalImpactoUrgencia.Visible = chkNivelTerminal.Checked && Convert.ToInt32(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas;
                btnModalInforme.Visible = chkNivelTerminal.Checked && Convert.ToInt32(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas;
                //btnModalEncuesta.Visible = chkNivelTerminal.Checked && Convert.ToInt32(ddlTipoArbol.SelectedValue) != (int)BusinessVariables.EnumTipoArbol.Consultas;

                if (ddlTipoArbol.SelectedValue == ((int)BusinessVariables.EnumTipoArbol.Consultas).ToString())
                {
                    AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.Acceso, true);
                    AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.DueñoDelServicio, true);
                    AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeInformaciónPublicada, true);
                    AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.EspecialDeConsulta, true);
                    //Información de Consulta
                    List<InformacionConsulta> infoCons = _servicioSistemaTipoInformacionConsulta.ObtenerTipoInformacionConsulta(false).Select(tipoInf => new InformacionConsulta { TipoInfConsulta = tipoInf }).ToList();
                    rptInformacion.DataSource = infoCons;
                    rptInformacion.DataBind();
                    AsociarGrupoUsuario.Limpiar();
                    return;
                }

                //Grupos
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.Acceso, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeInformaciónPublicada, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeOperación, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeAtención, true);
                AsociarGrupoUsuario.HabilitaGrupos((int)BusinessVariables.EnumRoles.EspecialDeConsulta, true);
                AsociarGrupoUsuario.Limpiar();

                //Ticket
                Metodos.LlenaComboCatalogo(ddlMascaraAcceso, _servicioMascaras.ObtenerMascarasAcceso(true));
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
                Metodos.LlenaComboCatalogo(ddlPropietario, _servicioInformacionConsulta.ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido, true));
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
                    case BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
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
                ValidaCapturaConsulta();
                btnModalGrupos.CssClass = "btn btn-primary";
                btnModalConsultas.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalConsultas\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaInfoConsulta = _lstError;
            }
        }

        protected void btnCerrarTicket_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaTicket();
                btnModalTicket.CssClass = "btn btn-success";
                btnModalGrupos.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalTicket\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaTicket = _lstError;
            }
        }

        protected void btnCerraGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaGrupos();
                btnModalGrupos.CssClass = "btn btn-success";
                if (Convert.ToInt32(ddlTipoArbol.SelectedValue) == (int)BusinessVariables.EnumTipoArbol.Consultas)
                    btnModalImpactoUrgencia.CssClass = "btn btn-primary";
                else
                    btnModalSla.CssClass = "btn btn-primary";
                int idGrupo = 0;
                foreach (RepeaterItem item in AsociarGrupoUsuario.GruposAsociados)
                {
                    Label lblIdGrupoUsuario = (Label)item.FindControl("lblIdGrupoUsuario");
                    Label lblIdRol = (Label)item.FindControl("lblIdTipoSubGrupo");
                    if (Convert.ToInt32(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.ResponsableDeAtención)
                        idGrupo = Convert.ToInt32(lblIdGrupoUsuario.Text);
                }
                if (idGrupo != 0)
                    UcSla.IdGrupo = idGrupo;
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

        protected void btnCerrarEncuesta_OnClick(object o, EventArgs e)
        {
            try
            {
                ValidaCapturaTicket();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalEncuesta\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaTicket = _lstError;
            }
        }

        #endregion Cerrar Modales

        #region Abre modales Maestros
        protected void btnModalConsultas_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalGrupos.CssClass = "btn btn-primary disabled";
                btnModalConsultas.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalConsultas\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalTicket_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalTicket.CssClass = "btn btn-primary";
                btnModalGrupos.CssClass = "btn btn-primary disabled";
                btnModalSla.CssClass = "btn btn-primary disabled";
                btnModalImpactoUrgencia.CssClass = "btn btn-primary disabled";
                btnModalInforme.CssClass = "btn btn-primary disabled";
                btnModalEncuesta.CssClass = "btn btn-primary disabled";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalTicket\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalGrupos.CssClass = "btn btn-primary";
                btnModalSla.CssClass = "btn btn-primary disabled";
                btnModalImpactoUrgencia.CssClass = "btn btn-primary disabled";
                btnModalInforme.CssClass = "btn btn-primary disabled";
                btnModalEncuesta.CssClass = "btn btn-primary disabled";
                AsociarGrupoUsuario.AsignacionAutomatica = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalGruposNodo\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalSla_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalSla.CssClass = "btn btn-primary";
                btnModalImpactoUrgencia.CssClass = "btn btn-primary disabled";
                btnModalInforme.CssClass = "btn btn-primary disabled";
                btnModalEncuesta.CssClass = "btn btn-primary disabled";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalSla\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalInforme_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalInforme.CssClass = "btn btn-primary";
                btnModalEncuesta.CssClass = "btn btn-primary disabled";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalTiempoInforme\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalEncuesta_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalEncuesta.CssClass = "btn btn-primary";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalEncuesta\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnModalImpactoUrgencia_OnClick(object sender, EventArgs e)
        {
            try
            {
                btnModalImpactoUrgencia.CssClass = "btn btn-primary";
                btnModalInforme.CssClass = "btn btn-primary disabled";
                btnModalEncuesta.CssClass = "btn btn-primary disabled";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalImpacto\");", true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        #endregion Abre modales Maestros


    }
}

