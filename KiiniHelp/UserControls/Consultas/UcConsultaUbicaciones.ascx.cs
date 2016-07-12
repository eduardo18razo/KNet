using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceSistemaDomicilio;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceUbicacion;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones.Domicilio;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaUbicaciones : UserControl
    {
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceUbicacionClient _servicioUbicacion = new ServiceUbicacionClient();
        readonly ServiceDomicilioSistemaClient _servicioSistemaDomicilio = new ServiceDomicilioSistemaClient();
        private List<string> _lstError = new List<string>();

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set { ddlTipoUsuario.SelectedValue = value.ToString(); }
        }

        public List<string> AlertaUbicacion
        {
            set
            {
                panelAlertaUbicacion.Visible = value.Any();
                if (!panelAlertaUbicacion.Visible) return;
                rptErrorUbicacion.DataSource = value;
                rptErrorUbicacion.DataBind();
            }
        }

        private List<string> AlertaCampus
        {
            set
            {
                panelAlertaCampus.Visible = value.Any();
                if (!panelAlertaCampus.Visible) return;
                rptErrorCampus.DataSource = value;
                rptErrorCampus.DataBind();
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
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioCatalogo, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioCampus, lstTipoUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaComboUbicacion(int idTipoUsuario)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlpais, _servicioUbicacion.ObtenerPais(idTipoUsuario, true));
                if (ddlpais.Items.Count != 2) return;
                ddlpais.SelectedIndex = 1;
                ddlpais_OnSelectedIndexChanged(ddlpais, null);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void LlenaUbicaciones()
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

                if (ddlpais.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idHolding = int.Parse(ddlpais.SelectedValue);

                if (ddlCampus.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idCompania = int.Parse(ddlCampus.SelectedValue);

                if (ddlTorre.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idDireccion = int.Parse(ddlTorre.SelectedValue);

                if (ddlPiso.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubDireccion = int.Parse(ddlPiso.SelectedValue);

                if (ddlZona.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idGerencia = int.Parse(ddlZona.SelectedValue);

                if (ddlSubZona.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubGerencia = int.Parse(ddlSubZona.SelectedValue);

                if (ddlSiteRack.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idJefatura = int.Parse(ddlSiteRack.SelectedValue);

                rptResultados.DataSource = _servicioUbicacion.ObtenerUbicaciones(idTipoUsuario, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaUbicacion = new List<string>();
                AlertaCatalogos = new List<string>();
                AlertaCampus = new List<string>();
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
                AlertaUbicacion = _lstError;
            }

        }

        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlpais);
                Metodos.LimpiarCombo(ddlCampus);
                Metodos.LimpiarCombo(ddlTorre);
                Metodos.LimpiarCombo(ddlPiso);
                Metodos.LimpiarCombo(ddlZona);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    LlenaComboUbicacion(IdTipoUsuario);
                    LlenaUbicaciones();
                    btnNew.Visible = true;
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
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlpais_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlCampus);
                Metodos.LimpiarCombo(ddlTorre);
                Metodos.LimpiarCombo(ddlPiso);
                Metodos.LimpiarCombo(ddlZona);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlCampus, _servicioUbicacion.ObtenerCampus(idTipoUsuario, id, true));
                LlenaUbicaciones();
                if (ddlpais.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.CommandName = "Campus";
                    btnNew.Text = "Agregar Campus";
                    btnNew.CommandArgument = "0";
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
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlCampus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlTorre);
                Metodos.LimpiarCombo(ddlPiso);
                Metodos.LimpiarCombo(ddlZona);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlTorre, _servicioUbicacion.ObtenerTorres(IdTipoUsuario, id, true));
                LlenaUbicaciones();
                btnNew.CommandName = "Torre";
                btnNew.Text = "Agregar Torre";
                btnNew.CommandArgument = "3";
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlTorre_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlPiso);
                Metodos.LimpiarCombo(ddlZona);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlPiso, _servicioUbicacion.ObtenerPisos(idTipoUsuario, id, true));
                LlenaUbicaciones();
                btnNew.CommandName = "Piso";
                btnNew.Text = "Agregar Piso";
                btnNew.CommandArgument = "4";
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlPiso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlZona);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlZona, _servicioUbicacion.ObtenerZonas(idTipoUsuario, id, true));
                LlenaUbicaciones();
                btnNew.CommandName = "Zona";
                btnNew.Text = "Agregar Zona";
                btnNew.CommandArgument = "5";
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlZona_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSubZona);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlSubZona, _servicioUbicacion.ObtenerSubZonas(idTipoUsuario, id, true));
                LlenaUbicaciones();
                btnNew.CommandName = "SubZona";
                btnNew.Text = "Agregar Campus";
                btnNew.CommandArgument = "6";
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void ddlSubZona_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSiteRack);
                FiltraCombo((DropDownList)sender, ddlSiteRack, _servicioUbicacion.ObtenerSiteRacks(idTipoUsuario, id, true));
                LlenaUbicaciones();
                btnNew.CommandName = "Site Rack";
                btnNew.Text = "Agregar Site Rack";
                btnNew.CommandArgument = "7";
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioUbicacion.HabilitarUbicacion(Convert.ToInt32(hfId.Value), false);
                LlenaUbicaciones();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioUbicacion.HabilitarUbicacion(Convert.ToInt32(hfId.Value), true);
                LlenaUbicaciones();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int nivel = 0;
                string descripcion = null;
                Ubicacion org = _servicioUbicacion.ObtenerUbicacionById(Convert.ToInt32(hfId.Value));
                Session["UbicacionSeleccionada"] = org;
                lblTitleCatalogo.Text = ObtenerRuta(org, ref nivel, ref descripcion);
                txtDescripcionCatalogo.Text = descripcion;
                hfCatalogo.Value = nivel.ToString();
                hfAlta.Value = false.ToString();
                ddlTipoUsuarioCatalogo.SelectedValue = org.IdTipoUsuario.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        private string ObtenerRuta(Ubicacion ubicacion, ref int nivel, ref string descripcion)
        {
            string result = null;
            try
            {
                if (ubicacion.Pais != null)
                {
                    nivel = 8;
                    result = ubicacion.Pais.Descripcion;
                    descripcion = ubicacion.Pais.Descripcion;
                }
                if (ubicacion.Campus != null)
                {
                    nivel = 9;
                    result += ">" + ubicacion.Campus.Descripcion;
                    descripcion = ubicacion.Campus.Descripcion;
                }
                if (ubicacion.Torre != null)
                {
                    nivel = 10;
                    result += ">" + ubicacion.Torre.Descripcion;
                    descripcion = ubicacion.Torre.Descripcion;
                }
                if (ubicacion.Piso != null)
                {
                    nivel = 11;
                    result += ">" + ubicacion.Piso.Descripcion;
                    descripcion = ubicacion.Piso.Descripcion;
                }
                if (ubicacion.Zona != null)
                {
                    nivel = 12;
                    result += ">" + ubicacion.Zona.Descripcion;
                    descripcion = ubicacion.Zona.Descripcion;
                }
                if (ubicacion.SubZona != null)
                {
                    nivel = 13;
                    result += ">" + ubicacion.SubZona.Descripcion;
                    descripcion = ubicacion.SubZona.Descripcion;
                }
                if (ubicacion.SiteRack != null)
                {
                    nivel = 14;
                    result += ">" + ubicacion.SiteRack.Descripcion;
                    descripcion = ubicacion.SiteRack.Descripcion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

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
                ValidaSeleccion(btn.CommandArgument);
                if (btn.CommandArgument == "0")
                {
                    ddlTipoUsuarioCampus.SelectedValue = IdTipoUsuario.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCampus\");", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "MostrarPopup(\"#editCatalogoUbicacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        private void ValidaSeleccion(string command)
        {
            try
            {
                switch (command)
                {
                    case "0":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "3":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCampus.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "4":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCampus.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlTorre.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "5":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCampus.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlTorre.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlPiso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "6":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCampus.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlTorre.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlPiso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlZona.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                    case "7":
                        if (ddlpais.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlCampus.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlTorre.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlPiso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlZona.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        if (ddlSubZona.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception();
                        break;
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoUbicacion\");", true);
                throw new Exception("Debe de Seleccionarse un Padre para esta Operacion");
            }
        }

        public string ObtenerRuta(string command, string modulo)
        {
            string result = "<h3>ALTA NUEVA " + modulo + "</h3><span style=\"font-size: x-small;\">";
            switch (command)
            {
                case "9":
                    result += ddlpais.SelectedItem.Text;
                    break;
                case "10":
                    result += ddlpais.SelectedItem.Text + ">" + ddlCampus.SelectedItem.Text;
                    break;
                case "11":
                    result += ddlpais.SelectedItem.Text + ">" + ddlCampus.SelectedItem.Text + ">" + ddlTorre.SelectedItem.Text;
                    break;
                case "12":
                    result += ddlpais.SelectedItem.Text + ">" + ddlCampus.SelectedItem.Text + ">" + ddlTorre.SelectedItem.Text + ">" + ddlPiso.SelectedItem.Text;
                    break;
                case "13":
                    result += ddlpais.SelectedItem.Text + ">" + ddlCampus.SelectedItem.Text + ">" + ddlTorre.SelectedItem.Text + ">" + ddlPiso.SelectedItem.Text + ">" + ddlZona.SelectedItem.Text;
                    break;
                case "14":
                    result += ddlpais.SelectedItem.Text + ">" + ddlCampus.SelectedItem.Text + ">" + ddlTorre.SelectedItem.Text + ">" + ddlPiso.SelectedItem.Text + ">" + ddlZona.SelectedItem.Text + ">" + ddlSubZona.SelectedItem.Text;
                    break;
            }
            result += "</span>";
            return result;
        }

        protected void btnCrearCampus_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Metodos.ValidaCapturaCatalogoCampus(Convert.ToInt32(ddlTipoUsuarioCampus.SelectedValue), txtDescripcionCampus.Text, ddlColonia.SelectedValue == "" ? 0 : Convert.ToInt32(ddlColonia.SelectedValue), txtCalle.Text.Trim(), txtNoExt.Text.Trim(), txtNoInt.Text.Trim()))
                {
                    Ubicacion ubicacion = new Ubicacion
                    {
                        IdTipoUsuario = IdTipoUsuario,
                        IdPais = Convert.ToInt32(ddlpais.SelectedValue),
                        Campus = new Campus
                        {
                            IdTipoUsuario = Convert.ToInt32(ddlTipoUsuarioCampus.SelectedValue),
                            Descripcion = txtDescripcionCampus.Text.Trim(),
                            Domicilio = new List<Domicilio>
                            {
                                new Domicilio
                                {
                                    IdColonia = Convert.ToInt32(ddlColonia.SelectedValue),
                                    Calle = txtCalle.Text.Trim(),
                                    NoExt = txtNoExt.Text.Trim(),
                                    NoInt = txtNoInt.Text.Trim()
                                }
                            },
                            Habilitado = chkHabilitado.Checked
                        }
                    };
                    _servicioUbicacion.GuardarUbicacion(ubicacion);
                    LimpiaCampus();
                    ddlpais_OnSelectedIndexChanged(ddlpais, null);
                    LlenaUbicaciones();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCampus\");", true);
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCampus = _lstError;
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
                AlertaUbicacion = _lstError;
            }
        }

        protected void btnGuardarCatalogo_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (!Metodos.ValidaCapturaCatalogo(txtDescripcionCatalogo.Text)) return;
                Ubicacion ubicacion;
                if (Convert.ToBoolean(hfAlta.Value))
                {

                    int idTipoUsuario = IdTipoUsuario;
                    ubicacion = new Ubicacion
                    {
                        IdTipoUsuario = idTipoUsuario,
                        IdPais = Convert.ToInt32(ddlpais.SelectedValue)
                    };
                    idTipoUsuario = IdTipoUsuario;
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 3:
                            ubicacion.IdCampus = Convert.ToInt32(ddlCampus.SelectedValue);
                            ubicacion.Torre = new Torre
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioUbicacion.GuardarUbicacion(ubicacion);
                            ddlCampus_OnSelectedIndexChanged(ddlCampus, null);
                            break;
                        case 4:
                            ubicacion.IdCampus = Convert.ToInt32(ddlCampus.SelectedValue);
                            ubicacion.IdTorre = Convert.ToInt32(ddlTorre.SelectedValue);
                            ubicacion.Piso = new Piso
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioUbicacion.GuardarUbicacion(ubicacion);
                            ddlTorre_OnSelectedIndexChanged(ddlTorre, null);
                            break;
                        case 5:
                            ubicacion.IdCampus = Convert.ToInt32(ddlCampus.SelectedValue);
                            ubicacion.IdTorre = Convert.ToInt32(ddlTorre.SelectedValue);
                            ubicacion.IdPiso = Convert.ToInt32(ddlPiso.SelectedValue);
                            ubicacion.Zona = new Zona
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };

                            _servicioUbicacion.GuardarUbicacion(ubicacion);
                            ddlPiso_OnSelectedIndexChanged(ddlPiso, null);
                            break;
                        case 6:
                            ubicacion.IdCampus = Convert.ToInt32(ddlCampus.SelectedValue);
                            ubicacion.IdTorre = Convert.ToInt32(ddlTorre.SelectedValue);
                            ubicacion.IdPiso = Convert.ToInt32(ddlPiso.SelectedValue);
                            ubicacion.IdZona = Convert.ToInt32(ddlZona.SelectedValue);
                            ubicacion.SubZona = new SubZona
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };

                            _servicioUbicacion.GuardarUbicacion(ubicacion);
                            ddlZona_OnSelectedIndexChanged(ddlZona, null);
                            break;
                        case 7:
                            ubicacion.IdCampus = Convert.ToInt32(ddlCampus.SelectedValue);
                            ubicacion.IdTorre = Convert.ToInt32(ddlTorre.SelectedValue);
                            ubicacion.IdPiso = Convert.ToInt32(ddlPiso.SelectedValue);
                            ubicacion.IdZona = Convert.ToInt32(ddlZona.SelectedValue);
                            ubicacion.IdSubZona = Convert.ToInt32(ddlSubZona.SelectedValue);
                            ubicacion.SiteRack = new SiteRack
                            {
                                IdTipoUsuario = idTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioUbicacion.GuardarUbicacion(ubicacion);
                            ddlSubZona_OnSelectedIndexChanged(ddlSubZona, null);
                            break;
                    }

                }
                else
                {
                    ubicacion = (Ubicacion)Session["UbicacionSeleccionada"];
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 8:
                            ubicacion.Pais.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 9:
                            ubicacion.Campus.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 10:
                            ubicacion.Torre.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 11:
                            ubicacion.Piso.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 12:
                            ubicacion.Zona.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 13:
                            ubicacion.SubZona.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                        case 14:
                            ubicacion.SiteRack.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            break;
                    }
                    _servicioUbicacion.ActualizarUbicacion(ubicacion);
                }
                LimpiaCatalogo();
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
                AlertaCatalogos = _lstError;
            }
        }

        protected void btnCancelarCatalogo_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiaCatalogo();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoUbicacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaUbicacion = _lstError;
            }
        }

        #endregion

        #region Campus
        private void LimpiaCampus()
        {
            try
            {
                txtDescripcionCampus.Text = string.Empty;
                txtCp.Text = string.Empty;
                txtCalle.Text = string.Empty;
                txtNoExt.Text = string.Empty;
                txtNoInt.Text = string.Empty;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCampus = _lstError;
            }
        }

        protected void txtCp_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlColonia, _servicioSistemaDomicilio.ObtenerColoniasCp(int.Parse(txtCp.Text.Trim()), true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCampus = _lstError;
            }
        }

        protected void btnCancelarCampus_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiaCampus();
                ddlTipoUsuarioCatalogo.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCampus\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCampus = _lstError;
            }
        }

        #endregion Campus
    }
}