using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.ServiceSistemaCatalogos;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaCatalogo : UserControl
    {
        private readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
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
                if (ddlCatalogos.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    List<CatalogoGenerico> lst = _servicioCatalogos.ObtenerRegistrosSistemaCatalogo(int.Parse(ddlCatalogos.SelectedValue), true).Where(w=>w.Id != 0).ToList();
                    rptResultados.DataSource = lst;
                }
                else
                {
                    rptResultados.DataSource = null;
                }
                
                rptResultados.DataBind();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
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
            ucRegistroCatalogo.OnAceptarModal += AltaRegistroCatalogoOnAceptarModal;
            ucRegistroCatalogo.OnCancelarModal += AltaRegistroCatalogoOnCancelarModal;
        }

        private void AltaRegistroCatalogoOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "hidden();", true);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaRegistro\");", true);
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

        private void AltaRegistroCatalogoOnAceptarModal()
        {
            try
            {
                LlenaCatalogoConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaRegistro\");", true);
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
                ucRegistroCatalogo.EsAlta = true;
                ucRegistroCatalogo.IdCatalogo = int.Parse(ddlCatalogos.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaRegistro\");", true);
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
