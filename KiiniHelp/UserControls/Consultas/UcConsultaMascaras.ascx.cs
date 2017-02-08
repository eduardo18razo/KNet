using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceMascaraAcceso;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaMascaras : UserControl
    {
        private readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        private List<string> _lstError = new List<string>();

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

        private void LlenaMascaras()
        {
            try
            {
                string descripcion = txtDescripcion.Text.Trim();

                rptResultados.DataSource = _servicioMascaras.Consulta(descripcion);
                rptResultados.DataBind();
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
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    LlenaMascaras();
                }
                AltaMascaraAcceso.OnAceptarModal += AltaMascaraOnAceptarModal;
                AltaMascaraAcceso.OnCancelarModal += AltaMascaraOnCancelarModal;
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

        private void AltaMascaraOnCancelarModal()
        {
            try
            {
                Response.Redirect("~/Users/Administracion/Mascaras/FrmConsultaMascaraAcceso.aspx");
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

        private void AltaMascaraOnAceptarModal()
        {
            try
            {
                Response.Redirect("~/Users/Administracion/Mascaras/FrmConsultaMascaraAcceso.aspx");
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

        protected void txtDescripcion_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaMascaras();
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
                _servicioMascaras.HabilitarMascara(Convert.ToInt32(hfId.Value), false);
                LlenaMascaras();
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
                _servicioMascaras.HabilitarMascara(Convert.ToInt32(hfId.Value), true);
                LlenaMascaras();
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
                //AltaInformacionConsulta.GrupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(Convert.ToInt32(hfId.Value));
                //ucAltaGrupoUsuario.Alta = false;
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaGrupoUsuarios\");", true);
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
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "MostrarPopup(\"#modalAltaMascara\");", true);
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