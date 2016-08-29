using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceOrganizacion;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaOrganizacion : UserControl, IControllerModal
    {
        public delegate void DelegateSeleccionOrganizacionModal();
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceOrganizacionClient _servicioOrganizacion = new ServiceOrganizacionClient();
        private List<string> _lstError = new List<string>();

        public bool Modal
        {
            get { return Convert.ToBoolean(hfModal.Value); }
            set { hfModal.Value = value.ToString(); }
        }
        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set { ddlTipoUsuario.SelectedValue = value.ToString(); }
        }

        public string ModalName
        {
            set { hfModalName.Value = value; }
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

        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
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

        private void LlenaOrganizaciones()
        {
            try
            {
                int? idTipoUsuario = null;
                int? idHolding = null;
                int? idCompania = null;
                int? idDireccion = null;
                int? idSubDireccion = null;
                int? idGerencia = null;
                int? idSubGerencia = null;
                int? idJefatura = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                if (ddlHolding.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idHolding = int.Parse(ddlHolding.SelectedValue);

                if (ddlCompañia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idCompania = int.Parse(ddlCompañia.SelectedValue);

                if (ddlDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idDireccion = int.Parse(ddlDireccion.SelectedValue);

                if (ddlSubDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubDireccion = int.Parse(ddlSubDireccion.SelectedValue);

                if (ddlGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idGerencia = int.Parse(ddlGerencia.SelectedValue);

                if (ddlSubGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubGerencia = int.Parse(ddlSubGerencia.SelectedValue);

                if (ddlJefatura.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idJefatura = int.Parse(ddlJefatura.SelectedValue);

                rptResultados.DataSource = new ServiceOrganizacionClient().ObtenerOrganizaciones(idTipoUsuario, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AlertaOrganizacion = new List<string>();
            AlertaCatalogos = new List<string>();
            if (!IsPostBack)
            {
                LlenaCombos();
                LlenaOrganizaciones();
            }
            //if (Request["__EVENTTARGET"] == "SeleccionarOrganizacion")
            //    Seleccionar(Convert.ToInt32(Request["__EVENTARGUMENT"]));
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
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    LlenaComboOrganizacion(IdTipoUsuario);
                    LlenaOrganizaciones();
                    btnNew.Visible = true;
                    btnNew.CommandName = "Holding";
                    btnNew.Text = "Agregar Holding";
                    btnNew.CommandArgument = "99";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Compañia";
                btnNew.Text = "Agregar Compañia";
                btnNew.CommandArgument = "9";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Dirección";
                btnNew.Text = "Agregar Dirección";
                btnNew.CommandArgument = "10";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Sub Dirección";
                btnNew.Text = "Agregar Sub Dirección";
                btnNew.CommandArgument = "11";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Gerencia";
                btnNew.Text = "Agregar Gerencia";
                btnNew.CommandArgument = "12";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Sub Gerencia";
                btnNew.Text = "Agregar Sub Gerencia";
                btnNew.CommandArgument = "13";
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
                LlenaOrganizaciones();
                btnNew.CommandName = "Jefatura";
                btnNew.Text = "Agregar Jefatura";
                btnNew.CommandArgument = "14";
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

        protected void ddlJefatura_OnSelectedIndexChanged(object sender, EventArgs e)
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

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioOrganizacion.HabilitarOrganizacion(Convert.ToInt32(hfId.Value), false);
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

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioOrganizacion.HabilitarOrganizacion(Convert.ToInt32(hfId.Value), true);
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
                int nivel = 0;
                string descripcion = null;
                Organizacion org = _servicioOrganizacion.ObtenerOrganizacionById(Convert.ToInt32(hfId.Value));
                Session["OrganizacionSeleccionada"] = org;
                lblTitleCatalogo.Text = ObtenerRuta(org, ref nivel, ref descripcion);
                txtDescripcionCatalogo.Text = descripcion;
                hfCatalogo.Value = nivel.ToString();
                hfAlta.Value = false.ToString();
                ddlTipoUsuarioCatalogo.SelectedValue = org.IdTipoUsuario.ToString();
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

        private string ObtenerRuta(Organizacion organizacion, ref int nivel, ref string descripcion)
        {
            string result = null;
            try
            {
                if (organizacion.Holding != null)
                {
                    nivel = 1;
                    result = organizacion.Holding.Descripcion;
                    descripcion = organizacion.Holding.Descripcion;
                }
                if (organizacion.Compania != null)
                {
                    nivel = 2;
                    result += ">" + organizacion.Compania.Descripcion;
                    descripcion = organizacion.Compania.Descripcion;
                }
                if (organizacion.Direccion != null)
                {
                    nivel = 3;
                    result += ">" + organizacion.Direccion.Descripcion;
                    descripcion = organizacion.Direccion.Descripcion;
                }
                if (organizacion.SubDireccion != null)
                {
                    nivel = 4;
                    result += ">" + organizacion.SubDireccion.Descripcion;
                    descripcion = organizacion.SubDireccion.Descripcion;
                }
                if (organizacion.Gerencia != null)
                {
                    nivel = 5;
                    result += ">" + organizacion.Gerencia.Descripcion;
                    descripcion = organizacion.Gerencia.Descripcion;
                }
                if (organizacion.SubGerencia != null)
                {
                    nivel = 6;
                    result += ">" + organizacion.SubGerencia.Descripcion;
                    descripcion = organizacion.SubGerencia.Descripcion;
                }
                if (organizacion.Jefatura != null)
                {
                    nivel = 7;
                    result += ">" + organizacion.Jefatura.Descripcion;
                    descripcion = organizacion.Jefatura.Descripcion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
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
                if (!Metodos.ValidaCapturaCatalogo(txtDescripcionCatalogo.Text)) return;
                Organizacion organizacion;
                if (Convert.ToBoolean(hfAlta.Value))
                {
                    organizacion = new Organizacion
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
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
                            //upOrganizacion.Update();
                            break;
                    }
                }
                else
                {
                    organizacion = (Organizacion)Session["OrganizacionSeleccionada"];
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 1:
                            organizacion.Holding.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 2:
                            organizacion.Compania.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 3:
                            organizacion.Direccion.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 4:
                            organizacion.SubDireccion.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 5:
                            organizacion.Gerencia.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 6:
                            organizacion.SubGerencia.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 7:
                            organizacion.Jefatura.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                    }
                    _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                }
                LimpiaCatalogo();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
                LlenaOrganizaciones();
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

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (sender == null) return;
                lblTitleCatalogo.Text = ObtenerRuta(btn.CommandArgument, btn.CommandName.ToUpper());
                hfCatalogo.Value = btn.CommandArgument;
                hfAlta.Value = true.ToString();
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

        private void Seleccionar(int id)
        {
            hfIdSeleccion.Value = id.ToString();
            if (OnSeleccionOrganizacionModal != null)
                OnSeleccionOrganizacionModal();
        }

        public event DelegateSeleccionOrganizacionModal OnSeleccionOrganizacionModal;
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCerrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnSeleccionOrganizacionModal != null)
                    OnSeleccionOrganizacionModal();
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
    }
}