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
                try
                {
                    List<Usuario> usuarios = _servicioUsuarios.BuscarUsuarios(txtUserName.Text.Trim());
                    rbtnLstUsuarios.DataSource = usuarios;
                    rbtnLstUsuarios.DataTextField = "NombreCompleto";
                    rbtnLstUsuarios.DataValueField = "Id";
                    rbtnLstUsuarios.DataBind();
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
            try
            {
                Response.Redirect(ResolveUrl("~/Default.aspx"));
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

        protected void rbtnLstUsuarios_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (!_servicioUsuarios.ObtenerDetalleUsuario(int.Parse(rbtnLstUsuarios.SelectedValue)).Activo)
                {
                    throw new Exception("Debe primero confirmar su cuenta");
                }
                Response.Redirect("~/FrmRecuperar.aspx?ldata=" + QueryString.Encrypt(rbtnLstUsuarios.SelectedValue));
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