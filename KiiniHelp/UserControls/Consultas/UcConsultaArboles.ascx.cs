using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceArea;
using KiiniHelp.ServiceSistemaTipoArbolAcceso;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaArboles : UserControl
    {
        #region Variables
        
        
        //readonly ServiceTipoInfConsultaClient _servicioSistemaTipoInformacionConsulta = new ServiceTipoInfConsultaClient();
        
        //readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        //readonly ServiceSlaClient _servicioSla = new ServiceSlaClient();
        //readonly ServiceEncuestaClient _servicioEncuesta = new ServiceEncuestaClient();
        //readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceTipoArbolAccesoClient _servicioSistemaTipoArbol = new ServiceTipoArbolAccesoClient();
        readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        readonly ServiceAreaClient _servicioAreas = new ServiceAreaClient();

        private List<string> _lstError = new List<string>();
        #endregion Variables

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll();", true);
            }
        }

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlArea, _servicioAreas.ObtenerAreas(true));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaArboles()
        {
            try
            {
                int? idArea = null;
                int? idTipoUsuario = null;
                int? idTipoArbol = null;
                int? idHolding = null;
                int? idCompania = null;
                int? idDireccion = null;
                int? idSubDireccion = null;
                int? idGerencia = null;
                int? idSubGerencia = null;
                int? idJefatura = null;

                if (ddlArea.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idArea = int.Parse(ddlArea.SelectedValue);

                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                if (ddlTipoArbol.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoArbol = int.Parse(ddlTipoArbol.SelectedValue);

                if (ddlNivel1.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idHolding = int.Parse(ddlNivel1.SelectedValue);

                if (ddlNivel2.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idCompania = int.Parse(ddlNivel2.SelectedValue);

                if (ddlNivel3.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idDireccion = int.Parse(ddlNivel3.SelectedValue);

                if (ddlNivel4.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubDireccion = int.Parse(ddlNivel4.SelectedValue);

                if (ddlNivel5.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idGerencia = int.Parse(ddlNivel5.SelectedValue);

                if (ddlNivel6.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idSubGerencia = int.Parse(ddlNivel6.SelectedValue);

                if (ddlNivel7.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idJefatura = int.Parse(ddlNivel7.SelectedValue);

                rptResultados.DataSource = _servicioArbolAcceso.ObtenerArbolesAccesoAll(idArea, idTipoUsuario, idTipoArbol, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenaCombos();
                LlenaArboles();
            }
        }
        protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaArboles();
                Metodos.LimpiarCombo(ddlTipoUsuario);
                Metodos.LimpiarCombo(ddlTipoArbol);
                Metodos.LimpiarCombo(ddlNivel1);
                Metodos.LimpiarCombo(ddlNivel2);
                Metodos.LimpiarCombo(ddlNivel3);
                Metodos.LimpiarCombo(ddlNivel4);
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                if (ddlArea.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index) return;
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuario(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                
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
        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaArboles();
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
                LlenaArboles();
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
                LlenaArboles();
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
                LlenaArboles();
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
                LlenaArboles();
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
                LlenaArboles();
                Metodos.LimpiarCombo(ddlNivel5);
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel4.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), idNivelFiltro, null, null, null))
                {
                    Metodos.FiltraCombo(ddlNivel4, ddlNivel5, _servicioArbolAcceso.ObtenerNivel5(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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
                LlenaArboles();
                Metodos.LimpiarCombo(ddlNivel6);
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel5.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), Convert.ToInt32(ddlNivel4.SelectedValue), idNivelFiltro, null, null))
                {
                    Metodos.FiltraCombo(ddlNivel5, ddlNivel6, _servicioArbolAcceso.ObtenerNivel6(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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
                LlenaArboles();
                Metodos.LimpiarCombo(ddlNivel7);
                int idTipoArbol = Convert.ToInt32(ddlTipoArbol.SelectedValue);
                int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                int idNivelFiltro = Convert.ToInt32(ddlNivel6.SelectedValue);
                if (!_servicioArbolAcceso.EsNodoTerminal(idTipoUsuario, idTipoArbol, Convert.ToInt32(ddlNivel1.SelectedValue), Convert.ToInt32(ddlNivel2.SelectedValue), Convert.ToInt32(ddlNivel3.SelectedValue), Convert.ToInt32(ddlNivel4.SelectedValue), Convert.ToInt32(ddlNivel5.SelectedValue), idNivelFiltro, null))
                {
                    Metodos.FiltraCombo(ddlNivel6, ddlNivel7, _servicioArbolAcceso.ObtenerNivel7(idTipoArbol, idTipoUsuario, idNivelFiltro, true));
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

        protected void ddlNivel7_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaArboles();
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

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioArbolAcceso.HabilitarArbol(Convert.ToInt32(hfId.Value), false);
                LlenaArboles();
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

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioArbolAcceso.HabilitarArbol(Convert.ToInt32(hfId.Value), true);
                LlenaArboles();
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

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                //int nivel = 0;
                //string descripcion = null;
                
                //Organizacion org = _servicioOrganizacion.ObtenerOrganizacionById(Convert.ToInt32(hfId.Value));
                //Session["OrganizacionSeleccionada"] = org;
                //lblTitleCatalogo.Text = ObtenerRuta(org, ref nivel, ref descripcion);
                //txtDescripcionCatalogo.Text = descripcion;
                //hfCatalogo.Value = nivel.ToString();
                //hfAlta.Value = false.ToString();
                //ddlTipoUsuarioCatalogo.SelectedValue = org.IdTipoUsuario.ToString();
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
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

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                //Button btn = (Button)sender;
                //if (sender == null) return;
                //lblTitleCatalogo.Text = ObtenerRuta(btn.CommandArgument, btn.CommandName.ToUpper());
                //hfCatalogo.Value = btn.CommandArgument;
                //hfAlta.Value = true.ToString();
                //ddlTipoUsuarioCatalogo.SelectedValue = IdTipoUsuario.ToString();
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
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