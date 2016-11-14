using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp
{
    public partial class Identificar : Page
    {
        private readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        private List<string> _lstError = new List<string>();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                if (Request.Params["confirmacionalta"] != null)
                {
                    string[] values = Request.Params["confirmacionalta"].Split('_');
                    if (!_servicioUsuarios.ValidaConfirmacion(int.Parse(values[0]), values[1]))
                    {
                        AlertaGeneral = new List<string> { "Link Invalido !!!" };
                    }
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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Usuario userData = _servicioUsuarios.BuscarUsuario(txtUserName.Text.Trim());
                if (userData != null)
                    Response.Redirect("~/FrmRecuperar.aspx?ldata=" + QueryString.Encrypt(userData.Id.ToString()));

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