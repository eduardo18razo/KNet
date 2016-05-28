using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceSla;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaSla : UserControl
    {
        readonly ServiceSlaClient _servicioSla = new ServiceSlaClient();
        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                panelAlert.Visible = value.Any();
                if (!panelAlert.Visible) return;
                rptHeaderError.DataSource = value;
                rptHeaderError.DataBind();
            }
        }
        private void LimpiarCampos()
        {
            try
            {
                txtDescripcion.Text = String.Empty;
                txtTiempo.Text = String.Empty;
                chkEstimado.Checked = false;

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

        protected void chkEstimado_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtTiempo.Enabled = !chkEstimado.Checked;
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
                if (txtDescripcion.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar una descripción");
                if (chkEstimado.Checked || txtTiempo.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar un tiempo");
                SLA sla = new SLA();
                sla.Descripcion = txtDescripcion.Text.Trim();
                sla.TiempoHoraProceso = Convert.ToDecimal(txtTiempo.Text.Trim().Replace(":", "."));
                sla.Habilitado = true;
                _servicioSla.Guardar(sla);
                LimpiarCampos();
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