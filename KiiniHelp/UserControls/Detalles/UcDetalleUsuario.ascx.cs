using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcDetalleUsuario : System.Web.UI.UserControl, IControllerModal
    {
        private List<string> _lstError = new List<string>();

        public int IdUsuario
        {
            get { return Convert.ToInt32(ViewState["IdUsuario"].ToString()); }
            set
            {
                Usuario userDetail = new ServiceUsuariosClient().ObtenerDetalleUsuario(value);
                lblUserName.Text = userDetail.NombreCompleto;
                lblNombre.Text = userDetail.NombreCompleto;
                rptCorreos.DataSource = userDetail.CorreoUsuario;
                rptCorreos.DataBind();
                rptTelefonos.DataSource = userDetail.TelefonoUsuario;
                rptTelefonos.DataBind();
                UcDetalleOrganizacion.IdOrganizacion = userDetail.IdOrganizacion;
                UcDetalleUbicacion.IdUbicacion = userDetail.IdUbicacion;
                UcDetalleGrupoUsuario.IdUsuario = userDetail.Id;
                ViewState["IdUsuario"] = value;
            }
        }
        private List<string> AlertaGeneral
        {
            set
            {
                pnlAlertaGeneral.Visible = value.Any();
                if (!pnlAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _lstError = new List<string>();
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

        public event DelegateCerrarModal OnCerraModal;

        protected void btnCerrarModal_OnClick(object sender, EventArgs e)
        {
            if (OnCerraModal != null)
            {
                OnCerraModal();
            }
            
        }
    }
}