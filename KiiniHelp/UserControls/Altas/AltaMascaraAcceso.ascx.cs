using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.ServiceSistemaCatalogos;
using KiiniHelp.ServiceSistemaTipoCampoMascara;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaMascaraAcceso : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        readonly ServiceTipoCampoMascaraClient _servicioSistemaTipoCampoMascara = new ServiceTipoCampoMascaraClient();
        readonly ServiceCatalogosClient _servicioSistemaCatalogos = new ServiceCatalogosClient();
        readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        private List<string> _lstError = new List<string>();

        public int Ejemplo { get; set; }
        #region Alertas
        private List<string> AlertaGeneral
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }

        private List<string> AlertaModalAgregarCampo
        {
            set
            {
                panelAlertaAgregarCampo.Visible = value.Any();
                if (!panelAlertaAgregarCampo.Visible) return;
                rptErrorModalAgregarCampo.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorModalAgregarCampo.DataBind();
            }
        }
        #endregion Alertas

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlTipoCampo, _servicioSistemaTipoCampoMascara.ObtenerTipoCampoMascara(true));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LimpiarModalCampo()
        {
            try
            {
                txtDescripcionCampo.Text = string.Empty;
                chkRequerido.Checked = false;
                txtLongitudMinima.Text = string.Empty;
                txtLongitudMaxima.Text = string.Empty;
                txtValorMaximo.Text = string.Empty;
                txtSimboloMoneda.Text = string.Empty;
                ddlCatalogosCampo.SelectedIndex = ddlCatalogosCampo.SelectedIndex >= 1 ? BusinessVariables.ComboBoxCatalogo.IndexSeleccione : -1;
                hfAltaCampo.Value = true.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LimpiarMascara()
        {
            try
            {
                txtDescripcionCampo.Text = string.Empty;
                chkRequerido.Checked = false;
                txtLongitudMinima.Text = string.Empty;
                txtLongitudMaxima.Text = string.Empty;
                txtValorMaximo.Text = string.Empty;
                txtSimboloMoneda.Text = string.Empty;
                if (ddlCatalogosCampo.SelectedIndex > 0)
                    ddlCatalogosCampo.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                Session["MascaraAlta"] = new Mascara();
                rptControles.DataSource = ((Mascara)Session["MascaraAlta"]).CampoMascara;
                rptControles.DataBind();
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
                AlertaGeneral = new List<string>();
                AlertaModalAgregarCampo = new List<string>();
                if (IsPostBack) return;
                Session["MascaraAlta"] = new Mascara();
                LlenaCombos();
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

        protected void chkObligatorio_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtMin = (TextBox)rptControles.Items[((RepeaterItem)((CheckBox)sender).NamingContainer).ItemIndex].FindControl("txtLongitudMinima");
                TextBox txtMax = (TextBox)rptControles.Items[((RepeaterItem)((CheckBox)sender).NamingContainer).ItemIndex].FindControl("txtLongitudMaxima");
                if (txtMin == null || txtMax == null) return;
                txtMin.Enabled = ((CheckBox)sender).Checked;
                txtMax.Enabled = ((CheckBox)sender).Checked;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void ddlTipoCampo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoCampo.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione) return;
                TipoCampoMascara tipoCampo = _servicioSistemaTipoCampoMascara.TipoCampoMascaraId(Convert.ToInt32(ddlTipoCampo.SelectedValue));
                if (tipoCampo == null) return;
                lblTitleAgregarCampo.Text = "Agregar campo " + tipoCampo.Descripcion;
                txtLongitudMinima.Visible = tipoCampo.LongitudMinima;
                txtLongitudMaxima.Visible = tipoCampo.LongitudMaxima;
                divLongitudes.Visible = tipoCampo.LongitudMinima && tipoCampo.LongitudMaxima;
                divMoneda.Visible = tipoCampo.SimboloMoneda;
                divValorMaximo.Visible = tipoCampo.ValorMaximo;
                divCatalgo.Visible = tipoCampo.Catalogo;
                divMascara.Visible = tipoCampo.Mask;
                if (tipoCampo.Catalogo)
                {
                    Metodos.LlenaComboCatalogo(ddlCatalogosCampo, _servicioSistemaCatalogos.ObtenerCatalogosMascaraCaptura(true));
                    divCatalgo.Visible = tipoCampo.Catalogo;
                }
                if (tipoCampo.UploadFile)
                {
                    txtLongitudMaxima.Text = "255";
                    txtLongitudMaxima.Visible = false;
                }

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAgregarCampoMascara\");", true);
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

        protected void btnGuardarCampo_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionCampo.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar una descripcion");
                Regex rgx = new Regex("[0-9]");

                if (rgx.IsMatch(txtDescripcionCampo.Text.Trim()))
                    throw new Exception("La descripcion no puede contener numeros.");

                if (divLongitudes.Visible)
                {
                    if (txtLongitudMinima.Text.Trim() == string.Empty)
                        throw new Exception("Debe especificar una longitud minima");
                    if (txtLongitudMaxima.Text.Trim() == string.Empty)
                        throw new Exception("Debe especificar una longitud maxima");
                    if (int.Parse(txtLongitudMinima.Text.Trim()) > int.Parse(txtLongitudMaxima.Text.Trim()))
                    {
                        throw new Exception("Longitud minima no puede ser mayor que longitud maxima");
                    }
                }

                if (divCatalgo.Visible)
                    if (ddlCatalogosCampo.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                        throw new Exception("Debe especificar un catalogo");

                if (divMoneda.Visible)
                    if (txtSimboloMoneda.Text.Trim() == string.Empty)
                        throw new Exception("Debe especificar una descripcion de moneda");

                if (divValorMaximo.Visible)
                    if (txtValorMaximo.Text.Trim() == string.Empty)
                        throw new Exception("Debe especificar un valor maximo");
                if (divMascara.Visible)
                    if (txtMascara.Text.Trim() == string.Empty)
                        throw new Exception("Debe especificar un Formulario de Cliente");

                Mascara tmpMascara = ((Mascara)Session["MascaraAlta"]);
                if (bool.Parse(hfAltaCampo.Value))
                {
                    if (tmpMascara.CampoMascara == null)
                        tmpMascara.CampoMascara = new List<CampoMascara>();
                    TipoCampoMascara tipoCampo = _servicioSistemaTipoCampoMascara.TipoCampoMascaraId(Convert.ToInt32(ddlTipoCampo.SelectedValue));

                    tmpMascara.CampoMascara.Add(item: new CampoMascara
                    {
                        IdCatalogo = tipoCampo.Catalogo ? Convert.ToInt32(ddlCatalogosCampo.SelectedValue) : (int?)null,
                        IdTipoCampoMascara = tipoCampo.Id,
                        Descripcion = txtDescripcionCampo.Text.Trim().ToUpper(),
                        Requerido = chkRequerido.Checked,
                        LongitudMinima =
                            tipoCampo.LongitudMinima
                                ? Convert.ToInt32(txtLongitudMinima.Text.Trim())
                                : tipoCampo.Mask ? 1 : (int?)null,
                        LongitudMaxima =
                            tipoCampo.LongitudMaxima
                                ? Convert.ToInt32(txtLongitudMaxima.Text.Trim())
                                : tipoCampo.Mask ? txtMascara.Text.Trim().Length : (int?)null,
                        SimboloMoneda = tipoCampo.SimboloMoneda ? txtSimboloMoneda.Text.Trim().ToUpper() : null,
                        ValorMaximo = tipoCampo.ValorMaximo ? Convert.ToInt32(txtValorMaximo.Text.Trim()) : (int?)null,
                        MascaraDetalle = tipoCampo.Mask ? txtMascara.Text.Trim() : null,
                        TipoCampoMascara = tipoCampo
                    });
                }
                else
                {
                    TipoCampoMascara tipoCampo = _servicioSistemaTipoCampoMascara.TipoCampoMascaraId(Convert.ToInt32(ddlTipoCampo.SelectedValue));
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].IdCatalogo = tipoCampo.Catalogo ? Convert.ToInt32(ddlCatalogosCampo.SelectedValue) : (int?)null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].IdTipoCampoMascara = tipoCampo.Id;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].Descripcion = txtDescripcionCampo.Text.Trim().ToUpper();
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].Requerido = chkRequerido.Checked;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].LongitudMinima = tipoCampo.LongitudMinima ? Convert.ToInt32(txtLongitudMinima.Text.Trim()) : tipoCampo.Mask ? 1 : (int?)null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].LongitudMaxima = tipoCampo.LongitudMaxima ? Convert.ToInt32(txtLongitudMaxima.Text.Trim()) : tipoCampo.Mask ? txtMascara.Text.Trim().Length : (int?)null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].SimboloMoneda = tipoCampo.SimboloMoneda ? txtSimboloMoneda.Text.Trim().ToUpper() : null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].ValorMaximo = tipoCampo.ValorMaximo ? Convert.ToInt32(txtValorMaximo.Text.Trim()) : (int?)null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].MascaraDetalle = tipoCampo.Mask ? txtMascara.Text.Trim() : null;
                    tmpMascara.CampoMascara[int.Parse(hfCampoEditado.Value)].TipoCampoMascara = tipoCampo;
                }

                rptControles.DataSource = tmpMascara.CampoMascara;
                rptControles.DataBind();
                Session["MascaraAlta"] = tmpMascara;
                ddlTipoCampo.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                LimpiarModalCampo();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAgregarCampoMascara\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar un nombre.");
                Mascara nuevaMascara = ((Mascara)Session["MascaraAlta"]);
                if (((Mascara)Session["MascaraAlta"]).CampoMascara.Count <= 0)
                    throw new Exception("Debe al menos un campo.");
                nuevaMascara.Descripcion = txtNombre.Text.Trim();
                nuevaMascara.Random = chkClaveRegistro.Checked;
                nuevaMascara.IdUsuarioAlta = ((Usuario) Session["UserData"]).Id;
                _servicioMascaras.CrearMascara(nuevaMascara);
                LimpiarMascara();
                if (OnAceptarModal != null)
                    OnAceptarModal();
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

        protected void btnLimpiarCampo_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarModalCampo();
                ddlTipoCampo.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAgregarCampoMascara\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarMascara();
                if (OnLimpiarModal != null)
                    OnLimpiarModal();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarMascara();
                if (OnCancelarModal != null)
                    OnCancelarModal();
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

        protected void btnEditarCampo_OnClick(object sender, EventArgs e)
        {
            try
            {
                Mascara tmpMascara = ((Mascara)Session["MascaraAlta"]);
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                Label lblIdTipoCampo = (Label)item.FindControl("lblIdTipoCampoMascara");
                Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                Label lblRequerido = (Label)item.FindControl("lblRequerido");

                CampoMascara campoEditar = tmpMascara.CampoMascara.SingleOrDefault(s => s.IdTipoCampoMascara == int.Parse(lblIdTipoCampo.Text) && s.Descripcion == lblDescripcion.Text && s.Requerido == (lblRequerido.Text == "SI"));
                if (campoEditar == null) return;
                hfCampoEditado.Value = tmpMascara.CampoMascara.IndexOf(campoEditar).ToString();
                ddlTipoCampo.SelectedValue = campoEditar.IdTipoCampoMascara.ToString();
                ddlTipoCampo_OnSelectedIndexChanged(ddlTipoCampo, null);
                chkRequerido.Checked = campoEditar.Requerido;
                txtDescripcionCampo.Text = campoEditar.Descripcion;
                if (divLongitudes.Visible)
                {
                    txtLongitudMinima.Text = campoEditar.LongitudMinima.ToString();
                    txtLongitudMaxima.Text = campoEditar.LongitudMaxima.ToString();
                }

                if (divCatalgo.Visible)
                    ddlCatalogosCampo.SelectedValue = campoEditar.IdCatalogo.ToString();

                if (divMoneda.Visible)
                    txtSimboloMoneda.Text = campoEditar.SimboloMoneda;

                if (divValorMaximo.Visible)
                    txtValorMaximo.Text = campoEditar.ValorMaximo.ToString();
                if (divMascara.Visible)
                    txtMascara.Text = campoEditar.MascaraDetalle;
                ddlTipoCampo_OnSelectedIndexChanged(ddlTipoCampo, null);
                hfAltaCampo.Value = false.ToString();

            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void btnEliminarCampo_OnClick(object sender, EventArgs e)
        {
            try
            {
                Mascara tmpMascara = ((Mascara)Session["MascaraAlta"]);
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                Label lblIdTipoCampo = (Label)item.FindControl("lblIdTipoCampoMascara");
                Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                Label lblRequerido = (Label)item.FindControl("lblRequerido");

                CampoMascara campoEditar = tmpMascara.CampoMascara.SingleOrDefault(s => s.IdTipoCampoMascara == int.Parse(lblIdTipoCampo.Text) && s.Descripcion == lblDescripcion.Text && s.Requerido == (lblRequerido.Text == "SI"));
                if (campoEditar == null) return;
                tmpMascara.CampoMascara.Remove(campoEditar);
                rptControles.DataSource = tmpMascara.CampoMascara;
                rptControles.DataBind();
                Session["MascaraAlta"] = tmpMascara;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void btnSubir_OnClick(object sender, EventArgs e)
        {
            try
            {
                Mascara tmpMascara = ((Mascara)Session["MascaraAlta"]);
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                Label lblIdTipoCampo = (Label)item.FindControl("lblIdTipoCampoMascara");
                Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                Label lblRequerido = (Label)item.FindControl("lblRequerido");

                CampoMascara campoEditar = tmpMascara.CampoMascara.SingleOrDefault(s => s.IdTipoCampoMascara == int.Parse(lblIdTipoCampo.Text) && s.Descripcion == lblDescripcion.Text && s.Requerido == (lblRequerido.Text == "SI"));
                if (campoEditar == null) return;
                int indexActual = tmpMascara.CampoMascara.IndexOf(campoEditar);
                tmpMascara.CampoMascara.Remove(campoEditar);
                tmpMascara.CampoMascara.Insert(indexActual -1, campoEditar);
                rptControles.DataSource = tmpMascara.CampoMascara;
                rptControles.DataBind();
                Session["MascaraAlta"] = tmpMascara;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }

        protected void btnBajar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Mascara tmpMascara = ((Mascara)Session["MascaraAlta"]);
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                Label lblIdTipoCampo = (Label)item.FindControl("lblIdTipoCampoMascara");
                Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                Label lblRequerido = (Label)item.FindControl("lblRequerido");

                CampoMascara campoEditar = tmpMascara.CampoMascara.SingleOrDefault(s => s.IdTipoCampoMascara == int.Parse(lblIdTipoCampo.Text) && s.Descripcion == lblDescripcion.Text && s.Requerido == (lblRequerido.Text == "SI"));
                if (campoEditar == null) return;
                int indexActual = tmpMascara.CampoMascara.IndexOf(campoEditar);
                tmpMascara.CampoMascara.Remove(campoEditar);
                tmpMascara.CampoMascara.Insert(indexActual + 1, campoEditar);
                rptControles.DataSource = tmpMascara.CampoMascara;
                rptControles.DataBind();
                Session["MascaraAlta"] = tmpMascara;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaModalAgregarCampo = _lstError;
            }
        }
    }
}