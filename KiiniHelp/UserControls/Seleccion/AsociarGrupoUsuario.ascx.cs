using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceSistemaSubRol;
using KiiniHelp.ServiceSistemaTipoGrupo;
using KiiniHelp.ServiceSubGrupoUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Seleccion
{
    public partial class AsociarGrupoUsuario : UserControl
    {
        readonly ServiceTipoGrupoClient _servicioSistemaTipoGrupo = new ServiceTipoGrupoClient();
        readonly ServiceSubRolClient _servicioSistemaSubRoles = new ServiceSubRolClient();
        readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();
        readonly ServiceSubGrupoUsuarioClient _servicioSubGrupoUsuario = new ServiceSubGrupoUsuarioClient();
        private List<string> _lstError = new List<string>();

        public bool Administrador
        {
            get { return divGrupoAdministrador.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Administrador, value); }
        }

        public bool Acceso
        {
            get { return divGrupoAcceso.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Acceso, value); }
        }

        public bool EspecialConsulta
        {
            get { return divGrupoEspConsulta.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.EspecialDeConsulta, value); }
        }

        public bool Atencion
        {
            get { return divGrupoRespAtencion.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeAtención, value); }
        }

        public bool Mtto
        {
            get { return divGrupoRespMtto.Visible; }
            set
            {
                HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento, value);
            }
        }

        public bool Operacion
        {
            get { return divGrupoRespOperacion.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeOperación, value); }
        }

        public bool Desarrollo
        {
            get { return divGrupoRespDesarrollo.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo, value); }
        }

        public RepeaterItemCollection GruposAsociados
        {
            get { return rptUsuarioGrupo.Items; }
        }

        private List<string> AlertaGrupos
        {
            set
            {
                panelAlertaGrupos.Visible = value.Any();
                if (!panelAlertaGrupos.Visible) return;
                rptErrorGrupos.DataSource = value;
                rptErrorGrupos.DataBind();
            }
        }

        public bool ValidaCapturaGrupos()
        {
            if (rptUsuarioGrupo.Items.Count > 0) return true;
            if (_lstError == null)
            {
                _lstError = new List<string>();
            }
            _lstError.Add("Debe asignar la menos un grupo");
            AlertaGrupos = _lstError;
            return false;
        }

        public void ObtenerGruposHerencia(int idTipoArbolAcceso, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            try
            {
                List<GrupoUsuario> lst;
                if (nivel1 == null)
                    lst = _servicioGrupoUsuario.ObtenerGruposUsuarioSistema();
                else
                    lst = _servicioGrupoUsuario.ObtenerGruposUsuarioNivel(idTipoArbolAcceso, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7);
                foreach (GrupoUsuario grupo in lst)
                {
                    if (grupo.SubGrupoUsuario.Any())
                    {
                        foreach (SubGrupoUsuario subGrupo in grupo.SubGrupoUsuario)
                        {
                            AsignarGrupo(grupo, subGrupo.SubRol.IdRol, subGrupo.Id);
                        }
                    }
                    else
                    {
                        AsignarGrupo(grupo, grupo.TipoGrupo.RolTipoGrupo.First().IdRol, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Limpiar()
        {
            try
            {
                Session["UsuarioGrupo"] = null;
                List<UsuarioGrupo> lst = (List<UsuarioGrupo>)Session["UsuarioGrupo"];
                rptUsuarioGrupo.DataSource = lst;
                rptUsuarioGrupo.DataBind();
                if (Administrador)
                    ddlGrupoAdministrador.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (Acceso)
                    ddlGrupoAcceso.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (EspecialConsulta)
                    ddlGrupoEspecialConsulta.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (Atencion)
                    ddlGrupoResponsableAtencion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (Mtto)
                    ddlGrupoResponsableMantenimiento.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (Operacion)
                    ddlGrupoResponsableOperacion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                if (Desarrollo)
                    ddlGrupoResponsableDesarrollo.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void HabilitaGrupos(int idRol, bool visible)
        {
            switch (idRol)
            {
                case (int)BusinessVariables.EnumRoles.Administrador:
                    divGrupoAdministrador.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoAdministrador, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.Acceso:
                    divGrupoAcceso.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoAcceso, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                    divGrupoEspConsulta.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoEspecialConsulta, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                    divGrupoRespAtencion.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableAtencion, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                    divGrupoRespMtto.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableMantenimiento, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                    divGrupoRespOperacion.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableOperacion, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
                case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                    divGrupoRespDesarrollo.Visible = visible;
                    if (visible)
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableDesarrollo, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _lstError = new List<string>();
        }

        protected void btnCerrarGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaGrupos();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalGrupos\");", true);
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

        #region Grupos
        protected void OnClickAsignarGrupo(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn == null) return;
                int operacion;
                int.TryParse(btn.CommandArgument, out operacion);

                GrupoUsuario grupoUsuario = null;
                Rol rol = null;
                SubRol subRol = null;
                List<HelperSubGurpoUsuario> lstSubRoles = new List<HelperSubGurpoUsuario>();

                int idGrupoUsuario = 0, idRol = 0;

                switch (operacion)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        idRol = (int)BusinessVariables.EnumRoles.Administrador;
                        if (ddlGrupoAdministrador.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoAdministrador.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        idRol = (int)BusinessVariables.EnumRoles.Acceso;
                        if (ddlGrupoAcceso.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoAcceso.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        idRol = (int)BusinessVariables.EnumRoles.EspecialDeConsulta;
                        if (ddlGrupoEspecialConsulta.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoEspecialConsulta.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeAtención;
                        if (ddlGrupoResponsableAtencion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableAtencion.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento;
                        if (ddlGrupoResponsableMantenimiento.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableMantenimiento.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeOperación;
                        if (ddlGrupoResponsableOperacion.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableOperacion.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo;
                        if (ddlGrupoResponsableDesarrollo.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableDesarrollo.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;

                }
                if (grupoUsuario != null)
                {
                    if (lstSubRoles.Count > 0)
                    {
                        hfOperacion.Value = operacion.ToString();
                        lblTitleSubRoles.Text = String.Format("Seleccione Sub Rol de Grupo {0}", grupoUsuario.Descripcion);
                        Metodos.LlenaListBoxCatalogo(chklbxSubRoles, _servicioSistemaSubRoles.ObtenerSubRolesByGrupoUsuarioRol(idGrupoUsuario, idRol, false));
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalSeleccionRol\");", true);
                        return;
                    }
                    AsignarGrupo(grupoUsuario, idRol, null);
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

        private void AsignarGrupo(GrupoUsuario grupoUsuario, int idRol, int? idSubGrupoUsuario)
        {
            try
            {
                List<UsuarioGrupo> lst = (List<UsuarioGrupo>)Session["UsuarioGrupo"];
                if (lst == null)
                    lst = new List<UsuarioGrupo>();

                if (grupoUsuario != null)
                {
                    if (lst.Any(a => a.IdGrupoUsuario == grupoUsuario.Id && a.IdSubGrupoUsuario == idSubGrupoUsuario && a.IdRol == idRol))
                        throw new Exception("Este grupo ya ha sido asignado");
                    grupoUsuario.TipoGrupo = _servicioSistemaTipoGrupo.ObtenerTiposGrupo(false).SingleOrDefault(s => s.Id == grupoUsuario.IdTipoGrupo);
                    lst.Add(new UsuarioGrupo
                    {
                        IdGrupoUsuario = grupoUsuario.Id,
                        IdRol = idRol,
                        IdSubGrupoUsuario = idSubGrupoUsuario,
                        GrupoUsuario = grupoUsuario,
                        SubGrupoUsuario = idSubGrupoUsuario != null ? _servicioSubGrupoUsuario.ObtenerSubGrupoUsuario(grupoUsuario.Id, (int)idSubGrupoUsuario) : null
                    });
                }

                Session["UsuarioGrupo"] = lst;
                rptUsuarioGrupo.DataSource = lst;
                rptUsuarioGrupo.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void OnClickAltaGrupo(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (sender == null) return;
                if (btn.CommandArgument != "0")
                {
                    btnCerrarModalAltaGrupoUsuario.CommandArgument = btn.CommandArgument;
                    ucltaGrupoUsuario.IdTipoGrugo = int.Parse(btn.CommandArgument);
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

        protected void btnCerrarModalAltaGrupoUsuario_OnClick(object sender, EventArgs e)
        {
            try
            {
                int idRol = int.Parse(((Button)sender).CommandArgument);
                switch (idRol)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        divGrupoAdministrador.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoAdministrador,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        divGrupoAcceso.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoAcceso,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        divGrupoEspConsulta.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoEspecialConsulta,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        divGrupoRespAtencion.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableAtencion,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        divGrupoRespMtto.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableMantenimiento,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        divGrupoRespOperacion.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableOperacion,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                        divGrupoRespDesarrollo.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableDesarrollo,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true));
                        break;
                }
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
        #endregion Grupos

        #region SubRoles
        protected void btnAsignarSubRoles_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn == null) return;
                int operacion = Convert.ToInt32(hfOperacion.Value);

                GrupoUsuario grupoUsuario = null;
                Rol rol = null;
                SubRol subRol = null;

                int idGrupoUsuario = 0, idRol = 0;

                switch (operacion)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        idRol = (int)BusinessVariables.EnumRoles.Administrador;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoAdministrador.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        idRol = (int)BusinessVariables.EnumRoles.Acceso;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoAcceso.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        idRol = (int)BusinessVariables.EnumRoles.EspecialDeConsulta;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoEspecialConsulta.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeAtención;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableAtencion.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableMantenimiento.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeOperación;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableOperacion.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo;
                        idGrupoUsuario = Convert.ToInt32(ddlGrupoResponsableDesarrollo.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        break;
                }
                if (grupoUsuario != null)
                {
                    foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => item.Selected))
                    {
                        AsignarGrupo(grupoUsuario, idRol, int.Parse(item.Value));
                    }
                }

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSeleccionRol\");", true);
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

        protected void btnCancelarSubRoles_OnClick(object sender, EventArgs e)
        {
            try
            {
                int operacion = Convert.ToInt32(hfOperacion.Value);
                switch (operacion)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        ddlGrupoAdministrador.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        ddlGrupoAcceso.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        ddlGrupoEspecialConsulta.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        ddlGrupoResponsableAtencion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        ddlGrupoResponsableMantenimiento.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        ddlGrupoResponsableOperacion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                        ddlGrupoResponsableDesarrollo.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                        break;
                }

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSeleccionRol\");", true);
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
        #endregion SubRoles

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
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
                string[] checkedBox = result.Split('$'); ;
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