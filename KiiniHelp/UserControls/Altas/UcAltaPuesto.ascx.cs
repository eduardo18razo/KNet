using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServicePuesto;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcAltaPuesto : UserControl, IControllerModal
    {
        private readonly ServicePuestoClient _servicioPuesto = new ServicePuestoClient();
        private List<string> _lstError = new List<string>();

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set { hfEsAlta.Value = value.ToString(); }
        }

        public int IdPuesto
        {
            get { return Convert.ToInt32(hfIdPuesto.Value); }
            set
            {
                Puesto puesto = _servicioPuesto.ObtenerPuestos(false).Single(s => s.Id == value);
                txtDescripcionPuesto.Text = puesto.Descripcion;
                hfIdPuesto.Value = value.ToString();
            }
        }

        private List<string> Alerta
        {
            set
            {
                panelAlerta.Visible = value.Any();
                if (!panelAlerta.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
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
                Alerta = new List<string>();
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
                Puesto puesto = new Puesto { Descripcion = txtDescripcionPuesto.Text.Trim(), Habilitado = true };
                if (EsAlta)
                    _servicioPuesto.Guardar(puesto);
                else
                    _servicioPuesto.Actualizar(int.Parse(hfIdPuesto.Value), puesto);
                LimpiarCampos();
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


    }
}