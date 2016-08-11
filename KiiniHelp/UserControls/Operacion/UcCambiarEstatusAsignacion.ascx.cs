using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceSistemaEstatus;
using KiiniHelp.ServiceSistemaSubRol;
using KiiniHelp.ServiceTicket;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Operacion
{
    public partial class UcCambiarEstatusAsignacion : UserControl, IControllerModal
    {
        private readonly ServiceEstatusClient _servicioEstatus = new ServiceEstatusClient();
        private readonly ServiceTicketClient _servicioTicketClient = new ServiceTicketClient();
        private readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        private readonly ServiceSubRolClient _servicioSubRol = new ServiceSubRolClient();
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private List<string> _lstError = new List<string>();

        public int IdTicket
        {
            get { return Convert.ToInt32(lblIdticket.Text); }
            set { lblIdticket.Text = value.ToString(); }
        }

        public int IdGrupo
        {
            get { return Convert.ToInt32(ViewState["IdGrupoTicket"]); }
            set
            {
                ViewState["IdGrupoTicket"] = value;
            }
        }
        public int IdUsuario
        {
            get { return Convert.ToInt32(ViewState["IdUsuarioTicket"]); }
            set
            {
                ViewState["IdUsuarioTicket"] = value;
                LLenaEstatus();
            }
        }

        public int IdEstatusAsignacionActual
        {
            get { return Convert.ToInt32(ViewState["IdEstatusAsignacionActual"]); }
            set
            {
                ViewState["IdEstatusAsignacionActual"] = value;
            }
        }

        public bool EsPropietario
        {
            get { return Convert.ToBoolean(lblEsPropietrio.Text); }
            set { lblEsPropietrio.Text = value.ToString(); }
        }

        private void LLenaEstatus()
        {
            try
            {
                List<EstatusAsignacion> lstEstatus = new List<EstatusAsignacion>();
                foreach (SubGrupoUsuario subRol in ((Usuario)Session["UserData"]).UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Select(s => s.SubGrupoUsuario))
                {
                    lstEstatus.AddRange(_servicioEstatus.ObtenerEstatusAsignacionUsuario(IdUsuario, subRol.IdSubRol, IdEstatusAsignacionActual, EsPropietario, true));
                }
                lstEstatus = lstEstatus.Distinct().ToList();
                ddlEstatus.DataSource = lstEstatus;
                ddlEstatus.DataTextField = "Descripcion";
                ddlEstatus.DataValueField = "Id";
                ddlEstatus.DataBind();
                divUsuariosNivel1.Visible = false;
                divUsuariosNivel2.Visible = false;
                divUsuariosNivel3.Visible = false;
                divUsuariosNivel4.Visible = false;
                divUsuariosSupervisor.Visible = false;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LLenaUsuarios()
        {
            try
            {
                divUsuariosNivel1.Visible = false;
                divUsuariosNivel2.Visible = false;
                divUsuariosNivel3.Visible = false;
                divUsuariosNivel4.Visible = false;
                divUsuariosSupervisor.Visible = false;
                List<int> lstSubRoles = ((Usuario)Session["UserData"]).UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Select(s => s.SubGrupoUsuario).Select(subRol => subRol.IdSubRol).ToList();
                var supervisor = lstSubRoles.Contains((int)BusinessVariables.EnumSubRoles.Supervisor);
                if (!EsPropietario && !supervisor) return;
                List<Usuario> lstUsuarios;
                List<SubRolEscalacionPermitida> lstAsignacionesPermitidas = new List<SubRolEscalacionPermitida>();
                foreach (int subRol in lstSubRoles)
                {
                    lstAsignacionesPermitidas.AddRange(_servicioSubRol.ObtenerEscalacion(subRol));
                }
                if (lstAsignacionesPermitidas.Any(a => a.IdSubRolPermitido == (int)BusinessVariables.EnumSubRoles.Supervisor))
                {
                    lstUsuarios = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo, (int)BusinessVariables.EnumSubRoles.Supervisor);
                    rbtnlSupervisor.DataSource = lstUsuarios;
                    rbtnlSupervisor.DataTextField = "NombreCompleto";
                    rbtnlSupervisor.DataValueField = "Id";
                    rbtnlSupervisor.DataBind();
                    divUsuariosSupervisor.Visible = lstUsuarios.Count > 0;
                }
                if (lstAsignacionesPermitidas.Any(a => a.IdSubRolPermitido == (int)BusinessVariables.EnumSubRoles.PrimererNivel))
                {
                    lstUsuarios = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo,
                        (int)BusinessVariables.EnumSubRoles.PrimererNivel);
                    rbtnlUsuariosGrupoNivel1.DataSource = lstUsuarios;
                    rbtnlUsuariosGrupoNivel1.DataTextField = "NombreCompleto";
                    rbtnlUsuariosGrupoNivel1.DataValueField = "Id";
                    rbtnlUsuariosGrupoNivel1.DataBind();
                    divUsuariosNivel1.Visible = lstUsuarios.Count > 0;
                }
                if (lstAsignacionesPermitidas.Any(a => a.IdSubRolPermitido == (int)BusinessVariables.EnumSubRoles.SegundoNivel))
                {
                    lstUsuarios = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo,
                        (int)BusinessVariables.EnumSubRoles.SegundoNivel);
                    rbtnlUsuariosGrupoNivel2.DataSource = lstUsuarios;
                    rbtnlUsuariosGrupoNivel2.DataTextField = "NombreCompleto";
                    rbtnlUsuariosGrupoNivel2.DataValueField = "Id";
                    rbtnlUsuariosGrupoNivel2.DataBind();
                    divUsuariosNivel2.Visible = lstUsuarios.Count > 0;
                }
                if (lstAsignacionesPermitidas.Any(a => a.IdSubRolPermitido == (int)BusinessVariables.EnumSubRoles.TercerNivel))
                {
                    lstUsuarios = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo,
                        (int)BusinessVariables.EnumSubRoles.TercerNivel);
                    rbtnlUsuariosGrupoNivel3.DataSource = lstUsuarios;
                    rbtnlUsuariosGrupoNivel3.DataTextField = "NombreCompleto";
                    rbtnlUsuariosGrupoNivel3.DataValueField = "Id";
                    rbtnlUsuariosGrupoNivel3.DataBind();
                    divUsuariosNivel3.Visible = lstUsuarios.Count > 0;
                }
                if (lstAsignacionesPermitidas.Any(a => a.IdSubRolPermitido == (int)BusinessVariables.EnumSubRoles.CuartoNivel))
                {
                    lstUsuarios = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo,
                        (int)BusinessVariables.EnumSubRoles.CuartoNivel);
                    rbtnlUsuariosGrupoNivel4.DataSource = lstUsuarios;
                    rbtnlUsuariosGrupoNivel4.DataTextField = "NombreCompleto";
                    rbtnlUsuariosGrupoNivel4.DataValueField = "Id";
                    rbtnlUsuariosGrupoNivel4.DataBind();
                    divUsuariosNivel4.Visible = lstUsuarios.Count > 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }

        private int IdUsuarioSeleccionado()
        {
            int result = 0;
            try
            {
                if (rbtnlSupervisor.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlSupervisor.SelectedValue);
                if (rbtnlUsuariosGrupoNivel1.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel1.SelectedValue);
                if (rbtnlUsuariosGrupoNivel2.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel2.SelectedValue);
                if (rbtnlUsuariosGrupoNivel3.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel3.SelectedValue);
                if (rbtnlUsuariosGrupoNivel4.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel4.SelectedValue);
                if (result == 0 && ddlEstatus.SelectedValue == ((int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado).ToString())
                    result = ((Usuario)Session["UserData"]).Id;
                else if (result == 0 && ddlEstatus.SelectedValue != ((int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado).ToString())
                    throw new Exception("Seleccione un usuario");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return result;
        }

        private void LimpiarPantalla()
        {
            try
            {
                _lstError = new List<string>();
                ddlEstatus.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                txtComentarios.Text = string.Empty;
                rbtnlSupervisor.ClearSelection();
                rbtnlUsuariosGrupoNivel1.ClearSelection();
                rbtnlUsuariosGrupoNivel2.ClearSelection();
                rbtnlUsuariosGrupoNivel3.ClearSelection();
                rbtnlUsuariosGrupoNivel4.ClearSelection();
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
                if (!IsPostBack)
                {

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

        protected void rbtnlUsuariosGrupoNivel1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rbtnlUsuariosGrupoNivel2.ClearSelection();
                rbtnlUsuariosGrupoNivel3.ClearSelection();
                rbtnlUsuariosGrupoNivel4.ClearSelection();
                rbtnlSupervisor.ClearSelection();
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

        protected void rbtnlUsuariosGrupoNivel2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rbtnlUsuariosGrupoNivel1.ClearSelection();
                rbtnlUsuariosGrupoNivel3.ClearSelection();
                rbtnlUsuariosGrupoNivel4.ClearSelection();
                rbtnlSupervisor.ClearSelection();
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

        protected void rbtnlUsuariosGrupoNivel3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rbtnlUsuariosGrupoNivel1.ClearSelection();
                rbtnlUsuariosGrupoNivel2.ClearSelection();
                rbtnlUsuariosGrupoNivel4.ClearSelection();
                rbtnlSupervisor.ClearSelection();
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

        protected void rbtnlUsuariosGrupoNivel4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rbtnlUsuariosGrupoNivel1.ClearSelection();
                rbtnlUsuariosGrupoNivel2.ClearSelection();
                rbtnlUsuariosGrupoNivel3.ClearSelection();
                rbtnlSupervisor.ClearSelection();
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

        protected void ddlEstatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlEstatus.SelectedValue) != (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.Autoasignado)
                    LLenaUsuarios();
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

        protected void rbtnlSupervisor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rbtnlUsuariosGrupoNivel1.ClearSelection();
                rbtnlUsuariosGrupoNivel2.ClearSelection();
                rbtnlUsuariosGrupoNivel3.ClearSelection();
                rbtnlUsuariosGrupoNivel4.ClearSelection();
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlEstatus.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.Index)
                    throw new Exception("Debe seleccionar un estatus");
                if (((Usuario)Session["UserData"]).UsuarioGrupo.Where(w => w.SubGrupoUsuario != null).Select(s => s.SubGrupoUsuario).Where(subRol => _servicioEstatus.HasComentarioObligatorio(((Usuario) Session["UserData"]).Id, subRol.IdSubRol,IdEstatusAsignacionActual, Convert.ToInt32(ddlEstatus.SelectedValue), EsPropietario)).Any(subRol => txtComentarios.Text.Trim() == string.Empty))
                {
                    throw new Exception("Debe agregar un comentario");
                }
                if (ddlEstatus.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    _servicioTicketClient.CambiarAsignacionTicket(IdTicket, Convert.ToInt32(ddlEstatus.SelectedValue), IdUsuarioSeleccionado(), ((Usuario)Session["UserData"]).Id, txtComentarios.Text);
                }
                LimpiarPantalla();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            LimpiarPantalla();
            if (OnAceptarModal != null)
                OnAceptarModal();
        }
    }
}