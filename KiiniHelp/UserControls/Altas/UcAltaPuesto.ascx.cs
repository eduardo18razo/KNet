using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using KiiniHelp.Funciones;
using KiiniHelp.ServicePuesto;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcAltaPuesto : UserControl, IControllerModal
    {
        private readonly ServicePuestoClient _servicioPuesto = new ServicePuestoClient();
        private readonly ServiceTipoUsuarioClient _servicioTipoUsuario = new ServiceTipoUsuarioClient();
        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set
            {
                hfEsAlta.Value = value.ToString();
                lblOperacion.Text = value ? "ALTA DE PUESTO" : "EDITAR PUESTO";
            }
        }
        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set
            {
                ddlTipoUsuario.SelectedValue = value.ToString();
            }
        }
        public int IdPuesto
        {
            get { return Convert.ToInt32(hfIdPuesto.Value); }
            set
            {
                Puesto puesto = _servicioPuesto.ObtenerPuestoById(value);
                txtDescripcionPuesto.Text = puesto.Descripcion;
                hfIdPuesto.Value = value.ToString();
            }
        }

        private List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        private string AlertaSucces
        {
            set
            {
                if (value.Trim() != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "SuccsessAlert('Exito','" + value + "');", true);
                }
            }
        }

        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                txtDescripcionPuesto.Text = String.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    LlenaCombos();
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

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionPuesto.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar una descripción");
                Puesto puesto = new Puesto { IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue), Descripcion = txtDescripcionPuesto.Text.Trim(), Habilitado = true };
                if (EsAlta)
                    _servicioPuesto.Guardar(puesto);
                else
                    _servicioPuesto.Actualizar(int.Parse(hfIdPuesto.Value), puesto);
                LimpiarCampos();
                if (!EsAlta)
                    if (OnAceptarModal != null)
                        OnAceptarModal();
                AlertaSucces = "Se guardo correctamente.";
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

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                if (OnLimpiarModal != null)
                    OnLimpiarModal();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                if (OnCancelarModal != null)
                    OnCancelarModal();
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


        protected void btnTerminar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnTerminarModal != null)
                    OnTerminarModal();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}