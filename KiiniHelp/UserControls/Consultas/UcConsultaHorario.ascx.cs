using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceDiasHorario;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaHorario : UserControl
    {
        private readonly ServiceDiasHorarioClient _servicioHorarios = new ServiceDiasHorarioClient();
        private readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();

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

        private void LlenaCombos()
        {
            try
            {
                List<GrupoUsuario> lstGrupos = _servicioGrupoUsuario.ObtenerGrupos(true);
                Metodos.LlenaComboCatalogo(ddlGrupoUsuario, lstGrupos);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaHorariosConsulta()
        {
            try
            {
                int? idTipoUsuario = null;
                if (ddlGrupoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idTipoUsuario = int.Parse(ddlGrupoUsuario.SelectedValue);

                rptResultados.DataSource = _servicioHorarios.ObtenerHorarioConsulta(idTipoUsuario);
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
                    LlenaCombos();
                    LlenaHorariosConsulta();
                }
                ucAltaHorario.OnAceptarModal += AltaHorarioOnAceptarModal;
                ucAltaHorario.OnCancelarModal += AltaHorarioOnCancelarModal;
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

        private void AltaHorarioOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaHorario\");", true);
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

        private void AltaHorarioOnAceptarModal()
        {
            try
            {
                LlenaHorariosConsulta();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaHorario\");", true);
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

        protected void ddlGrupoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaHorariosConsulta();
                if (ddlGrupoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    btnNew.Visible = true;
                    btnNew.Text = "Agregar Horario";
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
                _servicioHorarios.Habilitar(Convert.ToInt32(hfId.Value), false);
                LlenaHorariosConsulta();
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
                _servicioHorarios.Habilitar(Convert.ToInt32(hfId.Value), true);
                LlenaHorariosConsulta();
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
                //ucAltaHorario.EsAlta = false;
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaHorario\");", true);
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
                ucAltaHorario.EsAlta = true;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaHorario\");", true);
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