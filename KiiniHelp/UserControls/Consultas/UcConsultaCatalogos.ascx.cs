using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceSistemaCatalogos;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaCatalogos : UserControl
    {
        private readonly ServiceCatalogosClient _servicioCatalogos = new ServiceCatalogosClient();

        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        public List<string> Alerta
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }

        private void LlenaCombos()
        {
            try
            {
                List<Catalogos> lstCatalogosConsultas = _servicioCatalogos.ObtenerCatalogosMascaraCaptura(true);
                Metodos.LlenaComboCatalogo(ddlCatalogos, lstCatalogosConsultas);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaCatalogoConsulta()
        {
            try
            {
                int? idPuesto = null;
                if (ddlCatalogos.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idPuesto = int.Parse(ddlCatalogos.SelectedValue);

                rptResultados.DataSource = _servicioCatalogos.ObtenerCatalogoConsulta(idPuesto);
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Alerta = new List<string>();
            if (!IsPostBack)
            {
                LlenaCombos();
                LlenaCatalogoConsulta();
            }
            ucAltaCatalogo.OnAceptarModal += AltaCatalogoOnAceptarModal;
            ucAltaCatalogo.OnCancelarModal += AltaCatalogoOnCancelarModal;
            ucCargaCatalgo.OnAceptarModal += ucCargaCatalgo_OnAceptarModal;
            ucCargaCatalgo.OnCancelarModal += UcCargaCatalgoOnOnCancelarModal;
        }

        void ucCargaCatalgo_OnAceptarModal()
        {
            try
            {
                LlenaCatalogoConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalCargaCatalogo\");", true);
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

        private void UcCargaCatalgoOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalCargaCatalogo\");", true);
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

        private void AltaCatalogoOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaCatalogo\");", true);
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

        private void AltaCatalogoOnAceptarModal()
        {
            try
            {
                LlenaCatalogoConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaCatalogo\");", true);
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

        protected void ddlCatalogos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaCatalogoConsulta();
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
                bool esArchivo = bool.Parse(hfEsArchivo.Value);
                if (esArchivo)
                {
                    ucCargaCatalgo.EsAlta = false;
                    ucCargaCatalgo.IdCatalogo = int.Parse(hfId.Value);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalCargaCatalogo\");", true);
                }
                //else
                //{
                //    ucAltaCatalogo.EsAlta = false;
                //    ucAltaCatalogo.IdCatalogo = int.Parse(hfId.Value);
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaCatalogo\");", true);
                //}
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

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioCatalogos.Habilitar(Convert.ToInt32(hfId.Value), false);
                LlenaCatalogoConsulta();
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

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioCatalogos.Habilitar(Convert.ToInt32(hfId.Value), true);
                LlenaCatalogoConsulta();
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
                ucAltaCatalogo.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaCatalogo\");", true);
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

        protected void btnCargarCatalogo_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucCargaCatalgo.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalCargaCatalogo\");", true);
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

        
    }
}
