using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceOrganizacion;
using KiiniHelp.ServiceSistemaTipoGrupo;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaGrupos : UserControl, IControllerModal
    {
        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceTipoGrupoClient _servicioTipoGrupo = new ServiceTipoGrupoClient();
        readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();

        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set { ddlTipoUsuario.SelectedValue = value.ToString(); }
        }

        public List<string> AlertaGrupos
        {
            set
            {
                panelAlertaOrganizacion.Visible = value.Any();
                if (!panelAlertaOrganizacion.Visible) return;
                rptErrorOrganizacion.DataSource = value;
                rptErrorOrganizacion.DataBind();
            }
        }

        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoGrupo, _servicioTipoGrupo.ObtenerTiposGrupo(true));

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private void LlenaGrupos()
        {
            try
            {
                int? idTipoUsuario = null;
                int? idTipoGrupo = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                if (ddlTipoGrupo.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                    idTipoGrupo = int.Parse(ddlTipoGrupo.SelectedValue);

                rptResultados.DataSource = _servicioGrupoUsuario.ObtenerGruposUsuarioAll(idTipoUsuario, idTipoGrupo);
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
            AlertaGrupos = new List<string>();
            if (!IsPostBack)
            {
                LlenaCombos();
                LlenaGrupos();
            }
            ucAltaGrupoUsuario.OnAceptarModal += UcAltaGrupoUsuarioOnOnAceptarModal;
            ucAltaGrupoUsuario.OnCancelarModal += UcAltaGrupoUsuarioOnOnCancelarModal;
        }

        private void UcAltaGrupoUsuarioOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaGrupoUsuarios\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

        private void UcAltaGrupoUsuarioOnOnAceptarModal()
        {
            try
            {
                LlenaGrupos();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaGrupoUsuarios\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaGrupos();
                //Metodos.LimpiarCombo(ddlTipoGrupo);
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index && ddlTipoGrupo.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.CommandName = "Grupo Empleados";
                    btnNew.Text = "Agregar Grupo";
                    btnNew.CommandArgument = "1";
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
                AlertaGrupos = _lstError;
            }
        }

        protected void ddlTipoGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LlenaGrupos();
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index && ddlTipoGrupo.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    btnNew.Visible = true;
                    btnNew.CommandName = "Grupo Empleados";
                    btnNew.Text = "Agregar Grupo";
                    btnNew.CommandArgument = "1";
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
                AlertaGrupos = _lstError;
            }
        }

        protected void btnBaja_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioGrupoUsuario.HabilitarGrupo(Convert.ToInt32(hfId.Value), false);
                LlenaGrupos();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

        protected void btnAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                _servicioGrupoUsuario.HabilitarGrupo(Convert.ToInt32(hfId.Value), true);
                LlenaGrupos();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaGrupoUsuario.GrupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(Convert.ToInt32(hfId.Value));
                ucAltaGrupoUsuario.Alta = false;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaGrupoUsuarios\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }
        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (sender == null) return;
                if (btn.CommandArgument != "0")
                {
                    ucAltaGrupoUsuario.IdTipoUsuario = IdTipoUsuario;
                    ucAltaGrupoUsuario.IdTipoGrupo = int.Parse(ddlTipoGrupo.SelectedValue);
                    ucAltaGrupoUsuario.Alta = true;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaGrupoUsuarios\");", true);
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

        protected void chklbxSubRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                string result = Request.Form["__EVENTTARGET"];
                string[] checkedBox = result.Split('$');
                int index = int.Parse(checkedBox[checkedBox.Length - 1]);
                if (chklbxSubRoles.Items[index].Selected)
                {
                    value = Convert.ToInt32(chklbxSubRoles.Items[index].Value);
                }
                switch (int.Parse(hfOperacion.Value))
                {
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        bool permitir = chklbxSubRoles.Items.Cast<ListItem>().Any(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.Supervisor && item.Selected);
                        if (!permitir)
                            foreach (ListItem item in chklbxSubRoles.Items)
                            {
                                item.Selected = int.Parse(item.Value) == value;
                            }
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        foreach (ListItem item in chklbxSubRoles.Items)
                        {
                            item.Selected = int.Parse(item.Value) == value;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGrupos = _lstError;
            }
        }

    }
}