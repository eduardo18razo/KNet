using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceSistemaEstatus;
using KiiniHelp.ServiceTicket;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Operacion
{
    public partial class UcCambiarEstatusAsignacion : UserControl, IControllerModal
    {
        private readonly ServiceEstatusClient _servicioEstatus = new ServiceEstatusClient();
        private readonly ServiceTicketClient _servicioTicketClient = new ServiceTicketClient();
        private readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateCerrarModal OnCerraModal;

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
                LLenaUsuarios();
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

        public bool EsPropietario
        {
            get { return Convert.ToBoolean(lblEsPropietrio.Text); }
            set
            {
                lblEsPropietrio.Text = value.ToString();
                divUsuariosNivel1.Visible = value;
                divUsuariosNivel2.Visible = value;
                divUsuariosNivel3.Visible = value;
                divUsuariosNivel4.Visible = value;
            }
        }

        private void LLenaEstatus()
        {
            try
            {
                List<EstatusAsignacion> lstEstatus = new List<EstatusAsignacion>();
                foreach (SubGrupoUsuario subRol in ((Usuario)Session["UserData"]).UsuarioGrupo.Select(s => s.SubGrupoUsuario))
                {
                    lstEstatus.AddRange(_servicioEstatus.ObtenerEstatusAsignacionUsuario(IdUsuario, subRol.IdSubRol, EsPropietario, true));
                }
                lstEstatus = lstEstatus.Distinct().ToList();
                ddlEstatus.DataSource = lstEstatus;
                ddlEstatus.DataTextField = "Descripcion";
                ddlEstatus.DataValueField = "Id";
                ddlEstatus.DataBind();
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
                bool supervisor = false;
                foreach (SubGrupoUsuario subRol in ((Usuario)Session["UserData"]).UsuarioGrupo.Select(s => s.SubGrupoUsuario))
                {
                    supervisor = subRol.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor;
                    if (supervisor) break;
                }
                if (!EsPropietario && !supervisor) return;
                rbtnlUsuariosGrupoNivel1.DataSource = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo, (int)BusinessVariables.EnumSubRoles.PrimererNivel);
                rbtnlUsuariosGrupoNivel1.DataTextField = "NombreCompleto";
                rbtnlUsuariosGrupoNivel1.DataValueField = "Id";
                rbtnlUsuariosGrupoNivel1.DataBind();
                divUsuariosNivel1.Visible = rbtnlUsuariosGrupoNivel1.Items.Count > 0;

                rbtnlUsuariosGrupoNivel2.DataSource = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo, (int)BusinessVariables.EnumSubRoles.SegundoNivel);
                rbtnlUsuariosGrupoNivel2.DataTextField = "NombreCompleto";
                rbtnlUsuariosGrupoNivel2.DataValueField = "Id";
                rbtnlUsuariosGrupoNivel2.DataBind();
                divUsuariosNivel2.Visible = rbtnlUsuariosGrupoNivel1.Items.Count > 0;

                rbtnlUsuariosGrupoNivel3.DataSource = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo, (int)BusinessVariables.EnumSubRoles.TercerNivel);
                rbtnlUsuariosGrupoNivel3.DataTextField = "NombreCompleto";
                rbtnlUsuariosGrupoNivel3.DataValueField = "Id";
                rbtnlUsuariosGrupoNivel3.DataBind();
                divUsuariosNivel3.Visible = rbtnlUsuariosGrupoNivel1.Items.Count > 0;

                rbtnlUsuariosGrupoNivel4.DataSource = _servicioUsuarios.ObtenerUsuariosByGrupo(IdGrupo, (int)BusinessVariables.EnumSubRoles.CuartoNivel);
                rbtnlUsuariosGrupoNivel4.DataTextField = "NombreCompleto";
                rbtnlUsuariosGrupoNivel4.DataValueField = "Id";
                rbtnlUsuariosGrupoNivel4.DataBind();
                divUsuariosNivel4.Visible = rbtnlUsuariosGrupoNivel1.Items.Count > 0;
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
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }

        private int IdUsuarioSeleccionado()
        {
            int result = 0;
            try
            {
                if (rbtnlUsuariosGrupoNivel1.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel1.SelectedValue);
                if (rbtnlUsuariosGrupoNivel2.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel2.SelectedValue);
                if (rbtnlUsuariosGrupoNivel3.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel3.SelectedValue);
                if (rbtnlUsuariosGrupoNivel4.SelectedItem != null)
                    result = Convert.ToInt32(rbtnlUsuariosGrupoNivel4.SelectedValue);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return result;
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnAceptarModal != null)
                    OnAceptarModal();
                if (ddlEstatus.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    _servicioTicketClient.CambiarAsignacionTicket(IdTicket, Convert.ToInt32(ddlEstatus.SelectedValue), IdUsuarioSeleccionado(), ((Usuario)Session["UserData"]).Id);
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