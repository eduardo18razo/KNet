using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string Modal { get; set; }

        public bool AsignacionAutomatica
        {
            get { return Convert.ToBoolean(hfAsignacionAutomatica.Value); }
            set
            { hfAsignacionAutomatica.Value = value.ToString(); }
        }


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

        public bool UbicacionEmpleado
        {
            get { return divUbicacionEmpleado.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Responsablemantenimientoubicacionempleado, value); }
        }


        public bool OrganizacionEmpleado
        {
            get { return divOrganizacionEmpleado.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Responsablemantenimientoorganizacionempleado, value); }
        }

        public bool UsuarioEmpleado
        {
            get { return divUsuarioEmpleado.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioempleado, value); }
        }

        public bool UsuarioCliente
        {
            get { return divUsuarioCliente.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Responsablemantenimientousuariocliente, value); }
        }

        public bool UsuarioProveedor
        {
            get { return divUsuarioProveedor.Visible; }
            set
            { HabilitaGrupos((int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioproveedor, value); }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(hfTipoUsuario.Value); }
            set { hfTipoUsuario.Value = value.ToString(); }
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
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "UpScroll(\"" + Modal + "\");", true);
            }
        }

        public bool ValidaCapturaGrupos()
        {
            StringBuilder sb = new StringBuilder(); 
            try
            {
                if (Administrador)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.Administrador) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Administrador.</li>");
                if (Acceso)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.Acceso) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Acceso.</li>");
                if (EspecialConsulta)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.EspecialDeConsulta) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Especial de consulta.</li>");
                if (Atencion)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.ResponsableDeAtención) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Responsable de Atención.</li>");
                if (Mtto)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Responsable de Mantenimiento.</li>");
                if (Operacion)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.ResponsableDeOperación) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Responsable de Operación.</li>");
                if (Desarrollo)
                    if (GruposAsociados.Cast<RepeaterItem>().Select(item => (Label)item.FindControl("lblIdTipoSubGrupo")).Count(lblIdRol => int.Parse(lblIdRol.Text) == (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo) <= 0)
                        sb.AppendLine("<li>Debe asignar al menos un grupo de Tipo Responsable de Desarrollo.</li>");

                if (sb.ToString() != string.Empty)
                {
                    sb.Append("</ul>");
                    sb.Insert(0, "<ul>");
                    sb.Insert(0, "<h3>Asignación de Grupos</h3>");
                    throw new Exception(sb.ToString());
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
                return false;
            }

            
            return true;
        }

        private void ObtenerGruposHerencia()
        {
            try
            {
                foreach (GrupoUsuario grupo in _servicioGrupoUsuario.ObtenerGruposUsuarioSistema(IdTipoUsuario))
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void HabilitaGrupos(int idRol, bool visible)
        {
            try
            {
                switch (idRol)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        divGrupoAdministrador.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoAdministrador, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        divGrupoAcceso.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoAcceso, _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        divGrupoEspConsulta.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoEspecialConsulta, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        divGrupoRespAtencion.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoResponsableAtencion, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        divGrupoRespMtto.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoResponsableMantenimiento, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        divGrupoRespOperacion.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlGrupoResponsableOperacion, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoubicacionempleado:
                        divUbicacionEmpleado.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlUbicacionEmpleado, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoorganizacionempleado:
                        divOrganizacionEmpleado.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlOrganizacionEmpleado, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuariocliente:
                        divUsuarioCliente.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlUsuarioCliente, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioempleado:
                        divUsuarioEmpleado.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlUsuarioEmpleado, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioproveedor:
                        divUsuarioProveedor.Visible = visible;
                        if (visible)
                            Metodos.LlenaComboCatalogo(ddlUsuarioProveedor, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoorganizacionempleado:
                        idRol = (int)BusinessVariables.EnumRoles.Responsablemantenimientoorganizacionempleado;
                        if (ddlOrganizacionEmpleado.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlOrganizacionEmpleado.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoubicacionempleado:
                        idRol = (int)BusinessVariables.EnumRoles.Responsablemantenimientoubicacionempleado;
                        if (ddlUbicacionEmpleado.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlUbicacionEmpleado.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuariocliente:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeOperación;
                        if (ddlUsuarioCliente.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlUsuarioCliente.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioempleado:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeOperación;
                        if (ddlUsuarioEmpleado.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlUsuarioEmpleado.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;
                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioproveedor:
                        idRol = (int)BusinessVariables.EnumRoles.ResponsableDeOperación;
                        if (ddlUsuarioProveedor.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                            throw new Exception("Seleccione un grupo valido");
                        idGrupoUsuario = Convert.ToInt32(ddlUsuarioProveedor.SelectedItem.Value);
                        grupoUsuario = _servicioGrupoUsuario.ObtenerGrupoUsuarioById(idGrupoUsuario);
                        lstSubRoles = _servicioSubGrupoUsuario.ObtenerSubGruposUsuario(idGrupoUsuario, false);
                        break;

                }
                if (grupoUsuario != null)
                {
                    if (lstSubRoles.Count > 0)
                    {
                        if (AsignacionAutomatica)
                        {
                            foreach (SubRol sbRol in _servicioSistemaSubRoles.ObtenerSubRolesByGrupoUsuarioRol(idGrupoUsuario, idRol, false))
                            {
                                AsignarGrupo(grupoUsuario, idRol, sbRol.Id);
                            }
                        }
                        else
                        {
                            hfOperacion.Value = operacion.ToString();
                            lblTitleSubRoles.Text = String.Format("Seleccione Sub Rol de Grupo {0}", grupoUsuario.Descripcion);
                            Metodos.LlenaListBoxCatalogo(chklbxSubRoles, _servicioSistemaSubRoles.ObtenerSubRolesByGrupoUsuarioRol(idGrupoUsuario, idRol, false));
                            ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "MostrarPopup(\"#modalSeleccionRol\");", true);
                        }
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
                List<UsuarioGrupo> lst = (List<UsuarioGrupo>)Session["UsuarioGrupo"] ?? new List<UsuarioGrupo>();
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
                    ucAltaGrupoUsuario.IdRol = Convert.ToInt32(btn.CommandArgument);
                    ucAltaGrupoUsuario.IdTipoUsuario = IdTipoUsuario;
                    ucAltaGrupoUsuario.IdTipoGrupo = int.Parse(btn.CommandArgument);
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
                int idGrupoUsuario, idRol = 0;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGrupos = new List<string>();
                ucAltaGrupoUsuario.FromOpcion = AsignacionAutomatica;
                ucAltaGrupoUsuario.OnCancelarModal += ucAltaGrupoUsuario_OnCancelarModal;
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

        void ucAltaGrupoUsuario_OnCancelarModal()
        {
            try
            {
                int idRol = ucAltaGrupoUsuario.IdRol;
                switch (idRol)
                {
                    case (int)BusinessVariables.EnumRoles.Administrador:
                        divGrupoAdministrador.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoAdministrador, AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.Acceso:
                        divGrupoAcceso.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoAcceso,
                            _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.EspecialDeConsulta:
                        divGrupoEspConsulta.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoEspecialConsulta,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeAtención:
                        divGrupoRespAtencion.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableAtencion,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeMantenimiento:
                        divGrupoRespMtto.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableMantenimiento,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeOperación:
                        divGrupoRespOperacion.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableOperacion,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;
                    case (int)BusinessVariables.EnumRoles.ResponsableDeDesarrollo:
                        divGrupoRespDesarrollo.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlGrupoResponsableDesarrollo,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoubicacionempleado:
                        divUbicacionEmpleado.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlUbicacionEmpleado,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientoorganizacionempleado:
                        divOrganizacionEmpleado.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlOrganizacionEmpleado,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuariocliente:
                        divUsuarioCliente.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlUsuarioCliente,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioempleado:
                        divUsuarioEmpleado.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlUsuarioEmpleado,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
                        break;

                    case (int)BusinessVariables.EnumRoles.Responsablemantenimientousuarioproveedor:
                        divUsuarioProveedor.Visible = true;
                        Metodos.LlenaComboCatalogo(ddlUsuarioProveedor,
                            AsignacionAutomatica ? _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRol(idRol, true) : _servicioGrupoUsuario.ObtenerGruposUsuarioByIdRolTipoUsuario(idRol, IdTipoUsuario, true));
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

        protected void chkAsignarGruposSistema_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
                if (!chkAsignarGruposSistema.Checked)
                {
                    return;
                }
                ObtenerGruposHerencia();
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