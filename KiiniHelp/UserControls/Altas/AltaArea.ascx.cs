using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArea;
using KiiniNet.Entities.Operacion;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaArea : System.Web.UI.UserControl
    {
        readonly ServiceAreaClient _servicioArea = new ServiceAreaClient();
        private List<string> _lstError = new List<string>();
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
                txtDescripcionAreas.Text = String.Empty;

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
                if (txtDescripcionAreas.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar una descripción");
                Area sla = new Area();
                sla.Descripcion = txtDescripcionAreas.Text.Trim();
                //TODO: Cambiar propiedad por valor de control
                sla.Habilitado = true;
                _servicioArea.Guardar(sla);
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