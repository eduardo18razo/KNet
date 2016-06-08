using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using KiiniHelp.ServiceSeguridad;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp
{
    public partial class Login : Page
    {
        readonly ServiceSecurityClient _servicioSeguridad = new ServiceSecurityClient();
        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                pnlAlerta.Visible = value.Any();
                if (!pnlAlerta.Visible) return;
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }

        private void ValidaCaptura()
        {
            StringBuilder sb = new StringBuilder();

            if (txtUsuario.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Usuario es un campo obligatorio.</li>");
            if (txtpwd.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Contraseña es un campo obligatorio.</li>");
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Acceso</h3>");
                throw new Exception(sb.ToString());
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaCaptura();
                if (!_servicioSeguridad.Autenticate(txtUsuario.Text.Trim(), txtpwd.Text.Trim())) return;
                Usuario user = _servicioSeguridad.GetUserDataAutenticate(txtUsuario.Text.Trim(), txtpwd.Text.Trim());
                Session["UserData"] = user;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.NombreUsuario, DateTime.Now, DateTime.Now.AddMinutes(30), true, Session["UserData"].ToString(), FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                List<int> roles = user.UsuarioRol.Select(s => s.RolTipoUsuario.IdRol).ToList();
                Response.Redirect(roles.Any(a => a == (int)BusinessVariables.EnumRoles.Administrador) ? "~/Administracion/Default.aspx" : "~/General/Default.aspx");
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