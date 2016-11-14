using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServicePuesto;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaPuestos : UserControl
    {
        private readonly ServicePuestoClient _servicioPuestos = new ServicePuestoClient();

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
                List<Puesto> lstPuestosConsultas = _servicioPuestos.ObtenerPuestos(true);
                Metodos.LlenaComboCatalogo(ddlpuestos, lstPuestosConsultas);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaPuestosConsulta()
        {
            try
            {
                int? idPuesto = null;
                if (ddlpuestos.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idPuesto = int.Parse(ddlpuestos.SelectedValue);

                rptResultados.DataSource = _servicioPuestos.ObtenerPuestoConsulta(idPuesto);
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
                LlenaPuestosConsulta();
            }
            ucAltaPuesto.OnAceptarModal +=  AltaPuestoOnAceptarModal;
            ucAltaPuesto.OnCancelarModal += AltaPuestoOnCancelarModal;
        }

        private void AltaPuestoOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaPuesto\");", true);
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

        private void AltaPuestoOnAceptarModal()
        {
            try
            {
                LlenaPuestosConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaPuesto\");", true);
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

        protected void ddlpuestos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaPuestosConsulta();
                if (ddlpuestos.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.Text = "Agregar Puesto";
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
                Alerta = _lstError;
            }
        }

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioPuestos.Habilitar(Convert.ToInt32(hfId.Value), false);
                LlenaPuestosConsulta();
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
                _servicioPuestos.Habilitar(Convert.ToInt32(hfId.Value), true);
                LlenaPuestosConsulta();
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
                ucAltaPuesto.EsAlta = false;
                ucAltaPuesto.IdPuesto = Convert.ToInt32(hfId.Value);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaPuesto\");", true);
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
                ucAltaPuesto.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaPuesto\");", true);
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
