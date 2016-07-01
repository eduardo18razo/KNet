using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceOrganizacion;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaOrganizacion : UserControl, IControllerModal
    {
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceOrganizacionClient _servicioOrganizacion = new ServiceOrganizacionClient();
        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateCerrarModal OnCerraModal;

        public int IdTipoUsuario
        {
            get
            {
                return FromModal ? Convert.ToInt32(hfTipoUsuario.Value) : Convert.ToInt32(ddlTipoUsuario.SelectedValue);
            }
            set
            {
                hfTipoUsuario.Value = value.ToString();
                if (FromModal)
                {
                    LlenaComboOrganizacion(IdTipoUsuario);
                    LlenaCombos();
                }

            }
        }
        public bool FromModal
        {
            get
            {
                bool s = (bool)ViewState["FromModal"];
                return s;
            }

            set
            {
                ViewState["FromModal"] = value;
            }
        }

        public Organizacion ObtenerOrganizacion()
        {
            Organizacion organizacion;
            try
            {
                organizacion = new Organizacion
                {
                    IdTipoUsuario = IdTipoUsuario,
                    IdHolding = Convert.ToInt32(ddlHolding.SelectedValue)
                };

                if (ddlCompañia.SelectedValue != string.Empty & ddlCompañia.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);

                if (ddlDireccion.SelectedValue != string.Empty & ddlDireccion.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);

                if (ddlSubDireccion.SelectedValue != string.Empty & ddlSubDireccion.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);

                if (ddlGerencia.SelectedValue != string.Empty & ddlGerencia.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdGerencia = Convert.ToInt32(ddlGerencia.SelectedValue);

                if (ddlSubGerencia.SelectedValue != string.Empty & ddlSubGerencia.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdSubGerencia = Convert.ToInt32(ddlSubGerencia.SelectedValue);

                if (ddlJefatura.SelectedValue != string.Empty & ddlJefatura.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                    organizacion.IdJefatura = Convert.ToInt32(ddlJefatura.SelectedValue);

            }
            catch
            {
                throw new Exception("Error al obtener Organizacion intente nuevamente.");
            }
            return organizacion;
        }

        public List<string> AlertaOrganizacion
        {
            set
            {
                panelAlertaOrganizacion.Visible = value.Any();
                if (!panelAlertaOrganizacion.Visible) return;
                rptErrorOrganizacion.DataSource = value;
                rptErrorOrganizacion.DataBind();
            }
        }

        private List<string> AlertaCatalogos
        {
            set
            {
                panelAlertaCatalogo.Visible = value.Any();
                if (!panelAlertaCatalogo.Visible) return;
                rptErrorCatalogo.DataSource = value;
                rptErrorCatalogo.DataBind();
            }
        }

        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                if (!FromModal)
                    Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioCatalogo, lstTipoUsuario);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaComboOrganizacion(int idTipoUsuario)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlHolding, _servicioOrganizacion.ObtenerHoldings(idTipoUsuario, true));
                if (ddlHolding.Items.Count != 2) return;
                ddlHolding.SelectedIndex = 1;
                ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void FiltraCombo(DropDownList ddlFiltro, DropDownList ddlLlenar, object source)
        {
            try
            {
                ddlLlenar.Items.Clear();
                if (ddlFiltro.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    ddlLlenar.Enabled = true;
                    Metodos.LlenaComboCatalogo(ddlLlenar, source);
                }
                else
                {
                    ddlLlenar.DataSource = null;
                    ddlLlenar.DataBind();
                }

                ddlLlenar.Enabled = ddlLlenar.DataSource != null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ValidaCapturaOrganizacion()
        {
            StringBuilder sb = new StringBuilder();
            if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                sb.AppendLine("<li>Debe especificar un Tipo de Usuario.</li>");
            if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                sb.AppendLine("<li>Debe especificar al menos un Holding.</li>");

            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Organizacion</h3>");
                throw new Exception(sb.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaOrganizacion = new List<string>();
                AlertaCatalogos = new List<string>();
                if (!IsPostBack)
                {
                    divTipoUsuario.Visible = !FromModal;
                    if (FromModal)
                    {
                        LlenaComboOrganizacion(IdTipoUsuario);
                    }
                }
                //LlenaCombos();
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
                Metodos.LimpiarCombo(ddlHolding);
                Metodos.LimpiarCombo(ddlCompañia);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                if (ddlTipoUsuario.SelectedIndex != BusinessVariables.ComboBoxCatalogo.Index)
                {
                    LlenaComboOrganizacion(IdTipoUsuario);
                }
                btnAltaHolding.Visible = int.Parse(ddlTipoUsuario.SelectedValue) != (int)BusinessVariables.EnumTiposUsuario.Empleado;
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

        protected void ddlHolding_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlCompañia);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlCompañia, _servicioOrganizacion.ObtenerCompañias(idTipoUsuario, id, true));
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

        protected void ddlCompañia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlDireccion, _servicioOrganizacion.ObtenerDirecciones(idTipoUsuario, id, true));
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

        protected void ddlDirecion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlSubDireccion, _servicioOrganizacion.ObtenerSubDirecciones(idTipoUsuario, id, true));
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

        protected void ddlSubDireccion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlGerencia, _servicioOrganizacion.ObtenerGerencias(idTipoUsuario, id, true));
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

        protected void ddlGerencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlSubGerencia, _servicioOrganizacion.ObtenerSubGerencias(idTipoUsuario, id, true));
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

        protected void ddlSubGerencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlJefatura, _servicioOrganizacion.ObtenerJefaturas(idTipoUsuario, id, true));
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

        protected void btnAceptarOrganizacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnAceptarModal != null)
                {
                    OnAceptarModal();
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

        protected void OnClick(object sender, EventArgs e)
        {
            try
            {
                Button lbtn = (Button)sender;
                if (sender == null) return;
                ValidaSeleccion(lbtn.CommandArgument);
                lblTitleCatalogo.Text = ObtenerRuta(lbtn.CommandArgument, lbtn.CommandName.ToUpper());
                hfCatalogo.Value = lbtn.CommandArgument;
                ddlTipoUsuarioCatalogo.SelectedValue = IdTipoUsuario.ToString();
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

        public string ObtenerRuta(string command, string modulo)
        {
            string result = "<h3>ALTA NUEVA " + modulo + "</h3><span style=\"font-size: x-small;\">";
            switch (command)
            {
                case "9":
                    result += ddlHolding.SelectedItem.Text;
                    break;
                case "10":
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text;
                    break;
                case "11":
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text;
                    break;
                case "12":
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text + ">" + ddlSubDireccion.SelectedItem.Text;
                    break;
                case "13":
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text + ">" + ddlSubDireccion.SelectedItem.Text + ">" + ddlGerencia.SelectedItem.Text;
                    break;
                case "14":
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text + ">" + ddlSubDireccion.SelectedItem.Text + ">" + ddlGerencia.SelectedItem.Text + ">" + ddlSubGerencia.SelectedItem.Text;
                    break;
            }
            result += "</span>";
            return result;
        }

        public void ValidaSeleccion(string command)
        {
            try
            {
                if (IdTipoUsuario <= 0)
                    throw new Exception("Debe seleccionar un Tipo de usuario");
                switch (command)
                {
                    case "9":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "10":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCompañia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "11":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCompañia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "12":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCompañia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlSubDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "13":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCompañia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlSubDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlGerencia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "14":
                        if (ddlHolding.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCompañia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlSubDireccion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlGerencia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlSubGerencia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
                throw new Exception("Debe de seleccionarse un Padre para esta Operación");
            }
        }

        #region CatalogoAlta
        private void LimpiaCatalogo()
        {
            try
            {
                txtDescripcionCatalogo.Text = string.Empty;
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

        protected void btnGuardarCatalogo_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Metodos.ValidaCapturaCatalogo(txtDescripcionCatalogo.Text))
                {
                    Organizacion organizacion = new Organizacion
                    {
                        IdTipoUsuario = IdTipoUsuario,
                        IdHolding = Convert.ToInt32(ddlHolding.SelectedValue)
                    };
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 99:
                            organizacion.Holding = new Holding
                            {
                                IdTipoUsuario = Convert.ToInt32(ddlTipoUsuarioCatalogo.SelectedValue),
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            LlenaComboOrganizacion(Convert.ToInt32(ddlTipoUsuarioCatalogo.SelectedValue));
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            upOrganizacion.Update();
                            break;
                        case 9:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.Compania = new Compania
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            upOrganizacion.Update();
                            break;
                        case 10:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.Direccion = new Direccion
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                            upOrganizacion.Update();
                            break;
                        case 11:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.SubDireccion = new SubDireccion
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                            upOrganizacion.Update();
                            break;
                        case 12:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.Gerencia = new Gerencia
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                            upOrganizacion.Update();
                            break;
                        case 13:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.IdGerencia = Convert.ToInt32(ddlGerencia.SelectedValue);
                            organizacion.SubGerencia = new SubGerencia
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                            upOrganizacion.Update();
                            break;
                        case 14:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.IdGerencia = Convert.ToInt32(ddlGerencia.SelectedValue);
                            organizacion.IdSubGerencia = Convert.ToInt32(ddlSubGerencia.SelectedValue);
                            organizacion.Jefatura = new Jefatura
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                            upOrganizacion.Update();
                            break;
                    }
                    LimpiaCatalogo();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCatalogos = _lstError;
            }
        }

        protected void btnCancelarCatalogo_OnClick(object sender, EventArgs e)
        {
            LimpiaCatalogo();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
        }
        #endregion
    }
}